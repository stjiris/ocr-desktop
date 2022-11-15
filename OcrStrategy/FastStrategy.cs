using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class FastStrategy : AOcrStrategy
    {
        public static string StrategyName = "Fast";
        private OCRTesseract OpenCvEngineInstance;
        public FastStrategy(string[] Languages) : base(Languages)
        {
            OpenCvEngineInstance = TessdataUtil.CreateOpenCvEngine(Languages);
        }


        public override void Dispose()
        {
            OpenCvEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput PlainOcrOutput = new OCROutput(StrategyName);

            var watch = new System.Diagnostics.Stopwatch();
            using (ResourcesTracker t = new ResourcesTracker())
            {
                watch.Start();
                Mat TiffMat = t.T(Cv2.ImRead(TiffPage));
                Mat RedMat = t.T(TiffMat.Resize(new OpenCvSharp.Size(0, 0), 0.5, 0.5));
                string Text;
                OpenCvEngineInstance.Run(RedMat, out Text, out PlainOcrOutput.Rects, out PlainOcrOutput.Components, out PlainOcrOutput.Confidences, ComponentLevels.Word);
            }
            for( int i = 0; i < PlainOcrOutput.Rects.Length; i++)
            {
                Rect Curr = PlainOcrOutput.Rects[i];
                PlainOcrOutput.Rects[i] = new Rect(Curr.X * 2, Curr.Y * 2, Curr.Width * 2, Curr.Height * 2);
            }
            watch.Stop();
            PlainOcrOutput.Save(TsvPage, $"{watch.ElapsedMilliseconds}");
        }
    }
}
