using PdfSharp.Drawing;
using System.Xml;
using System.Drawing.Imaging;

namespace Tesseract_UI_Tools
{
    public static class PdfUtil
    {
        public static void AddTextLayer(XGraphics g, string TsvPath, string Jpeg, string OriginalTiff, float MinConf = 25)
        {
            float FinalRes = 1;
            float InitialRes = 1;
            using(Image JpegImage = Image.FromFile(Jpeg))
            {
                FinalRes = JpegImage.HorizontalResolution;
            }
            using(Image TiffImage = Image.FromFile(OriginalTiff))
            {
                InitialRes = TiffImage.HorizontalResolution;
            }
            float Scale = FinalRes / InitialRes;
            XBrush brush = new XSolidBrush(XColor.FromArgb(100, 0, 0, 0));

            using (StreamReader reader = new StreamReader(TsvPath))
            {
                reader.ReadLine(); // Drop header
                while (!reader.EndOfStream)
                {
                    string Line = reader.ReadLine();
                    string[] ValueString = Line.Split('\t');
                    string DebugInfo = ValueString[0];
                    float X1 = int.Parse(ValueString[1]) * Scale;
                    float Y1 = int.Parse(ValueString[2]) * Scale;
                    float X2 = int.Parse(ValueString[3]) * Scale;
                    float Y2 = int.Parse(ValueString[4]) * Scale;
                    float Confidence = float.Parse(ValueString[5]);
                    string Text = ValueString[6];
                    if (Text == "") continue;
                    if (Confidence < MinConf) continue;
                    XFont font = BestFont(g, Text, X2-X1, Y2-Y1);
                    g.DrawString(Text, font, brush, X1, Y1, XStringFormats.TopLeft);
                }
            }
        }
        public static XFont BestFont(XGraphics g, string text, double width, double height)
        {
            XFont font = new XFont(FontFamily.GenericSansSerif, 120, XFontStyle.Regular);
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
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string tprop in props)
            {
                string prop = tprop.Trim();
                int sepInd = prop.IndexOf(' ');
                string key = prop.Substring(0, sepInd);
                string value = prop.Substring(sepInd + 1);
                result.Add(key, value);
            }
            return result;
        }
    }
}
