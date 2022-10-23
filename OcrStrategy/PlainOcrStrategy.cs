using Tesseract;

namespace Tesseract_UI_Tools.OcrStrategy
{
    public class PlainOcrStartegy : AOcrStrategy
    {
        TesseractEngine TesseractEngineInstance;
        public static string StrategyName = "Plain";
        public PlainOcrStartegy(string[] Languages) : base(Languages)
        {
            TesseractEngineInstance = TessdataUtil.CreateTesseractEngine(LanguagesArray);
        }

        public override void Dispose()
        {
            TesseractEngineInstance.Dispose();
        }

        public override void GenerateTsv(string TiffPage, string TsvPage)
        {
            OCROutput PlainOcrOutput = new OCROutput(StrategyName);
            List<OpenCvSharp.Rect> Rects = new List<OpenCvSharp.Rect>();
            List<float> Confs = new List<float>();
            List<string> Comps = new List<string>();

            using( Pix TiffPix = Pix.LoadFromFile(TiffPage))
            {
                using (Page P = TesseractEngineInstance.Process(TiffPix, PageSegMode.AutoOsd))
                {
                    ResultIterator Iter = P.GetIterator();
                    while( Iter.Next(PageIteratorLevel.Word))
                    {
                        Rect TesseractCurrRect;
                        if( Iter.TryGetBoundingBox(PageIteratorLevel.Word, out TesseractCurrRect))
                        {
                            OpenCvSharp.Rect CurrRect = new OpenCvSharp.Rect(TesseractCurrRect.X1, TesseractCurrRect.Y1, TesseractCurrRect.Width, TesseractCurrRect.Height);
                            Rects.Add(CurrRect);
                            Confs.Add(Iter.GetConfidence(PageIteratorLevel.Word));
                            Comps.Add(Iter.GetText(PageIteratorLevel.Word));
                        }
                    }
                    PlainOcrOutput.Rects = Rects.ToArray();
                    PlainOcrOutput.Confidences = Confs.ToArray();
                    PlainOcrOutput.Components = Comps.ToArray();
                }
            }

            PlainOcrOutput.Save(TsvPage);
        }
    }
}
