using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class PreprocessCombineOcrStartegy : AOcrStrategy
    {
        public static string StrategyName = "Merge Plain & Preprocess";
        private OCRTesseract OpenCvEngineInstance;
        public PreprocessCombineOcrStartegy(string[] Languages) : base(Languages)
        {
            OpenCvEngineInstance = TessdataUtil.CreateOpenCvEngine(Languages);
        }


        public override void Dispose()
        {
            OpenCvEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput OcrOutput;

            using (ResourcesTracker t = new ResourcesTracker())
            {
                Mat TiffMat = t.T(Cv2.ImRead(TiffPage));
                Mat Gray;
                switch (TiffMat.Channels())
                {
                    case 1:
                        Gray = t.T(TiffMat.Clone());
                        break;
                    case 3:
                        Gray = t.T(TiffMat.CvtColor(ColorConversionCodes.BGR2GRAY));
                        break;
                    case 4:
                        Gray = t.T(TiffMat.CvtColor(ColorConversionCodes.BGRA2GRAY));
                        break;
                    default:
                        throw new Exception($"Cannot handle number of channels specified ({TiffMat.Channels()})");
                }
                OCROutput Plain = new OCROutput("Plain");
                OpenCvEngineInstance.Run(Gray, out _, out Plain.Rects, out Plain.Components, out Plain.Confidences, ComponentLevels.Word);
                Mat redn = t.T(Gray.GaussianBlur(new OpenCvSharp.Size(3, 3), 0));
                Mat thre = t.T(redn.AdaptiveThreshold(255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 17, 27));
                Mat strcDilate = t.T(new Mat(3, 3, MatType.CV_8UC1, new int[] {
                    1,1,1,
                    1,0,1,
                    1,1,1
                }));
                Mat strcErode = t.T(Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3)));
                Mat dilated = t.T(thre.Dilate(strcDilate)); // Open = Dilate + Erude; Close = Erude + Dilate
                Mat eroded = t.T(dilated.Erode(strcErode));
                OCROutput Prep = new OCROutput("Prep");
                OpenCvEngineInstance.Run(eroded, out _, out Prep.Rects, out Prep.Components, out Prep.Confidences, ComponentLevels.Word);
                OcrOutput = OCROutput.MergeBest(Plain, Prep);
            }
            OcrOutput.Save(TsvPage);
        }
    }
}
