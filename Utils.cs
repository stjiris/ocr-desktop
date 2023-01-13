using PdfSharp.Drawing;
using System.Xml;
using System.Drawing.Imaging;

namespace Tesseract_UI_Tools
{
    public static class PdfUtil
    {
        public static void AddTextLayer(XGraphics g, string TsvPath, string Jpeg, string OriginalTiff, float MinConf = 25, bool DebugPDF = false)
        {
            float FinalRes = 1;
            using(Image JpegImage = Image.FromFile(Jpeg))
            {
                FinalRes = JpegImage.HorizontalResolution;
            }
            float InitialRes = 1;
            using(Image TiffImage = Image.FromFile(OriginalTiff))
            {
                InitialRes = TiffImage.HorizontalResolution;
            }
            float Scale = FinalRes / InitialRes;
            XBrush brush = !DebugPDF ? new XSolidBrush(XColor.FromArgb(0, 0, 0, 0)) : XBrushes.Black;

            OCROutput OcrObject = OCROutput.Load(TsvPath);
            for( int i= 0; i < OcrObject.Rects.Length; i++)
            {
                if (OcrObject.Confidences[i] < MinConf && !DebugPDF) continue;
                float X1 = OcrObject.Rects[i].TopLeft.X * Scale;
                float Y1 = OcrObject.Rects[i].TopLeft.Y * Scale;
                float X2 = OcrObject.Rects[i].BottomRight.X * Scale;
                float Y2 = OcrObject.Rects[i].BottomRight.Y * Scale;
                string text = OcrObject.Components[i] ?? "";
                if ( DebugPDF)
                {
                    float conf = OcrObject.Confidences[i];
                    if( conf < 0.5)
                    {
                        g.DrawRectangle(new XSolidBrush(XColor.FromArgb(255, 255, 0, 0)), X1,Y1, X2-X1, Y2-Y1);
                    }
                    else if( conf < 0.75)
                    {
                        g.DrawRectangle(new XSolidBrush(XColor.FromArgb(255, 255, 255, 0)), X1, Y1, X2 - X1, Y2 - Y1);
                    }
                    else
                    {
                        g.DrawRectangle(new XSolidBrush(XColor.FromArgb(255, 0, 255, 0)), X1, Y1, X2 - X1, Y2 - Y1);
                    }
                }
                if( text.Trim() != "")
                {
                    XFont font = BestFont(g, text, X2 - X1, Y2 - Y1);
                    g.DrawString(text, font, brush, X1, Y1, XStringFormats.TopLeft);
                }
            }
        }
        public static XFont BestFont(XGraphics g, string text, double width, double height)
        {
            XFont font = new(FontFamily.GenericSansSerif, 120, XFontStyle.Regular);
            while (g.MeasureString(text, font).Width > width)
            {
                font = new XFont(font.FontFamily.Name, font.Size - 1, XFontStyle.Regular);
            }
            while (g.MeasureString(text, font).Height > height)
            {
                font = new XFont(font.FontFamily.Name, font.Size - 1, XFontStyle.Regular);
            }
            return font;
        }
    }

    public static class HocrUtil
    {
        internal static void FloatArrayFromProp(Dictionary<string,string> props, string key, float[] result, float scale = 1)
        {
            if (props.ContainsKey(key))
            {
                string value = props[key];
                string[] values = value.Split(' ');
                for (int i = 0; i < values.Length; i++)
                {
                    result[i] = float.Parse(values[i]) * scale;
                }
            }
        }

        internal static Dictionary<string,string> ReadTitleProperties(string title)
        {
            string[] props = title.Split(';');
            Dictionary<string, string> result = new();
            foreach (string tprop in props)
            {
                string prop = tprop.Trim();
                int sepInd = prop.IndexOf(' ');
                string key = prop[..sepInd];
                string value = prop[(sepInd + 1)..];
                result.Add(key, value);
            }
            return result;
        }
    }
}
