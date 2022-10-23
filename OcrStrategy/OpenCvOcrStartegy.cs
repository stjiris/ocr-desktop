using OpenCvSharp;
using OpenCvSharp.Text;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class OpenCvOcrStartegy : AOcrStrategy
    {
        public static string StrategyName = "OpenCv Plain WS";
        private OCRTesseract OpenCvEngineInstance;
        public OpenCvOcrStartegy(string[] Languages) : base(Languages)
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

            using(ResourcesTracker t = new ResourcesTracker())
            {
                Mat TiffMat = t.T(Cv2.ImRead(TiffPage));
                string Text;
                OpenCvEngineInstance.Run(TiffMat, out Text, out PlainOcrOutput.Rects, out PlainOcrOutput.Components, out PlainOcrOutput.Confidences, ComponentLevels.Word);
            }
            PlainOcrOutput.Save(TsvPage);
        }
    }
}
