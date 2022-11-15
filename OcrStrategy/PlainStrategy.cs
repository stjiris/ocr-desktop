using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class PlainStrategy : AOcrStrategy
    {
        public static string StrategyName = "Plain";
        private OCRTesseract OpenCvEngineInstance;
        public PlainStrategy(string[] Languages) : base(Languages)
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
            using(ResourcesTracker t = new ResourcesTracker())
            {
                watch.Start();
                Mat TiffMat = t.T(Cv2.ImRead(TiffPage));
                string Text;
                OpenCvEngineInstance.Run(TiffMat, out Text, out PlainOcrOutput.Rects, out PlainOcrOutput.Components, out PlainOcrOutput.Confidences, ComponentLevels.Word);
                watch.Stop();
            }
            PlainOcrOutput.Save(TsvPage, $"{watch.ElapsedMilliseconds}");
        }
    }
}
