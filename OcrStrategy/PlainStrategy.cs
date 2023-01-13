using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class PlainStrategy : AOcrStrategy
    {
        public static readonly string StrategyName = "Plain";
        private readonly OCRTesseract OpenCvEngineInstance;
        public PlainStrategy(string[] Languages) : base(Languages)
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
            OpenCvEngineInstance.Run(TiffMat, out string Text, out PlainOcrOutput.Rects, out PlainOcrOutput.Components, out PlainOcrOutput.Confidences, ComponentLevels.Word);
            watch.Stop();
            
            PlainOcrOutput.Save(TsvPage, $"{watch.ElapsedMilliseconds}");
        }
    }
}
