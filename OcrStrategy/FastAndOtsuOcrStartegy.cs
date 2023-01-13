using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class FastAndOtsuOcrStartegy : AOcrStrategy
    {
        public static readonly string StrategyName = "Fast & Otsu";
        private readonly OCRTesseract OpenCvEngineInstance;
        public FastAndOtsuOcrStartegy(string[] Languages) : base(Languages)
        {
            OpenCvEngineInstance = TessdataUtil.CreateOpenCvEngine(Languages);
        }


        public new void Dispose()
        {
            OpenCvEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput FastOut = new("Fast");
            OCROutput OtsuOut = new("Otsu");

            System.Diagnostics.Stopwatch watch = new();
            using ResourcesTracker t = new();
            watch.Start();
            Mat FullMat = t.T(Cv2.ImRead(TiffPage));
            Mat TiffMat = t.T(FullMat.Resize(OpenCvSharp.Size.Zero, 0.5, 0.5));

            OpenCvEngineInstance.Run(TiffMat, out _, out FastOut.Rects, out FastOut.Components, out FastOut.Confidences, ComponentLevels.Word);
            Mat Gray = TiffMat.Channels() switch
            {
                1 => t.T(TiffMat.Clone()),
                3 => t.T(TiffMat.CvtColor(ColorConversionCodes.BGR2GRAY)),
                4 => t.T(TiffMat.CvtColor(ColorConversionCodes.BGRA2GRAY)),
                _ => throw new Exception($"Cannot handle number of channels specified ({TiffMat.Channels()})"),
            };
            Mat redn = t.T(Gray.MedianBlur(5));
            Mat thre = t.T(redn.Threshold(0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu));
            Mat strcDilate = t.T(Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(5, 5)));
            Mat dilated = t.T(thre.Dilate(strcDilate)); // Open = Dilate + Erude; Close = Erude + Dilate
            OpenCvEngineInstance.Run(dilated, out _, out OtsuOut.Rects, out OtsuOut.Components, out OtsuOut.Confidences, ComponentLevels.Word);
            OCROutput OcrOut = OCROutput.MergeBest(FastOut, OtsuOut);
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
