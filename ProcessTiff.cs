using OpenCvSharp.Text;
using OpenCvSharp;

namespace Tesseract_UI_Tools
{
    public class ProcessTiff
    {
        public static OCROutput BestOCRProcess(string TiffFile, OCRTesseract engine)
        {
            using ResourcesTracker t = new();
            
            Mat TiffMat = t.T(Cv2.ImRead(TiffFile));
            OCROutput PlainOutputObj = new("Plain");
            engine.Run(TiffMat, out string Text, out PlainOutputObj.Rects, out PlainOutputObj.Components, out PlainOutputObj.Confidences, ComponentLevels.Word);
            Mat gray = TiffMat.Channels() switch
            {
                1 => t.T(TiffMat.Clone()),
                3 => t.T(TiffMat.CvtColor(ColorConversionCodes.BGR2GRAY)),
                4 => t.T(TiffMat.CvtColor(ColorConversionCodes.BGRA2GRAY)),
                _ => throw new Exception($"Cannot handle number of channels specified ({TiffMat.Channels()})"),
            };
            Mat redn = t.T(gray.GaussianBlur(new OpenCvSharp.Size(3, 3), 0));
            Mat thre = t.T(redn.AdaptiveThreshold(255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 17, 27));
            Mat strcDilate = t.T(new Mat(3, 3, MatType.CV_8UC1, new int[] {
                                1,1,1,
                                1,0,1,
                                1,1,1
                        }));
            Mat strcErode = t.T(Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3)));
            Mat dilated = t.T(thre.Dilate(strcDilate)); // Open = Dilate + Erude; Close = Erude + Dilate
            Mat eroded = t.T(dilated.Erode(strcErode));

            OCROutput PreprocessedOutputObj = new("Preprocessed");
            engine.Run(eroded, out Text, out PreprocessedOutputObj.Rects, out PreprocessedOutputObj.Components, out PreprocessedOutputObj.Confidences);

            return OCROutput.MergeBest(PlainOutputObj, PreprocessedOutputObj);
        }
    }
}