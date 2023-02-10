using OpenCvSharp;
using OpenCvSharp.Text;

namespace IRIS_OCR_Desktop.OcrStrategy
{
    public class FastStrategy : AOcrStrategy
    {
        public static readonly string StrategyName = "Fast";
        private readonly OCRTesseract OpenCvEngineInstance;
        public FastStrategy(string[] Languages) : base(Languages)
        {
            OpenCvEngineInstance = TessdataUtil.CreateOpenCvEngine(Languages);
        }


        public new void Dispose()
        {
            OpenCvEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput PlainOcrOutput = new(StrategyName);

            var watch = new System.Diagnostics.Stopwatch();
            using ResourcesTracker t = new();

            watch.Start();
            Mat TiffMat = t.T(Cv2.ImRead(TiffPage));
            Mat RedMat = t.T(TiffMat.Resize(new OpenCvSharp.Size(0, 0), 0.5, 0.5));
            OpenCvEngineInstance.Run(RedMat, out string Text, out PlainOcrOutput.Rects, out PlainOcrOutput.Components, out PlainOcrOutput.Confidences, ComponentLevels.Word);

            for (int i = 0; i < PlainOcrOutput.Rects.Length; i++)
            {
                Rect Curr = PlainOcrOutput.Rects[i];
                PlainOcrOutput.Rects[i] = new Rect(Curr.X * 2, Curr.Y * 2, Curr.Width * 2, Curr.Height * 2);
            }
            watch.Stop();
            PlainOcrOutput.Save(TsvPage, $"{watch.ElapsedMilliseconds}");
        }
    }
}
