using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class GaussianStrategy : AOcrStrategy
    {
        public static string StrategyName = "Gaussian";
        private OCRTesseract OpenCvEngineInstance;
        public GaussianStrategy(string[] Languages) : base(Languages)
        {
            OpenCvEngineInstance = TessdataUtil.CreateOpenCvEngine(Languages);
        }


        public override void Dispose()
        {
            OpenCvEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput OcrOut = new OCROutput(StrategyName);
            var watch = new System.Diagnostics.Stopwatch();
            using (ResourcesTracker t = new ResourcesTracker())
            {
                watch.Start();
                Mat FullMat = t.T(Cv2.ImRead(TiffPage));
                Mat TiffMat = t.T(FullMat.Resize(OpenCvSharp.Size.Zero, 0.5, 0.5));
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
                Mat redn = t.T(Gray.GaussianBlur(new OpenCvSharp.Size(3, 3), 0));
                Mat thre = t.T(redn.AdaptiveThreshold(255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 13, 10));
                Mat strcDilate = t.T(new Mat(3, 3, MatType.CV_8UC1, new int[] {
                    1,1,1,
                    1,0,1,
                    1,1,1
                }));
                Mat strcErode = t.T(Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3)));
                Mat dilated = t.T(thre.Dilate(strcDilate)); // Open = Dilate + Erude; Close = Erude + Dilate
                Mat eroded = t.T(dilated.Erode(strcErode));
                OpenCvEngineInstance.Run(eroded, out _, out OcrOut.Rects, out OcrOut.Components, out OcrOut.Confidences, ComponentLevels.Word);
            }

            for (int i = 0; i < OcrOut.Rects.Length; i++)
            {
                Rect Curr = OcrOut.Rects[i];
                OcrOut.Rects[i] = new Rect(Curr.X * 2, Curr.Y * 2, Curr.Width * 2, Curr.Height * 2);
            }
            watch.Stop();
            OcrOut.Save(TsvPage, $"{watch.ElapsedMilliseconds}");
        }
    }
}
