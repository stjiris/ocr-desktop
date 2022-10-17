using PdfSharp.Drawing;
using System.Xml;
using System.Drawing.Imaging;

namespace Tesseract_UI_Tools
{
    public static class PdfUtil
    {
        public static void AddTextLayer(XGraphics g, string HocrPath, string Jpeg, string OriginalTiff)
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
            XmlDocument hocrXml = new XmlDocument();
            hocrXml.Load(HocrPath);
            if (hocrXml.DocumentElement == null){ return; }
            XmlElement root = hocrXml.DocumentElement;


            foreach (XmlNode line in root.SelectNodes("//*[@class='ocr_line']"))
            {
                var properties = HocrUtil.ReadTitleProperties(line.Attributes["title"].Value);
                float[] linebox = { 0, 0, 0, 0 };
                HocrUtil.FloatArrayFromProp(properties, "bbox", linebox, Scale);
                float[] baseline = { 0, 0 };
                HocrUtil.FloatArrayFromProp(properties, "baseline", baseline);
                float[] size = { 0 };
                HocrUtil.FloatArrayFromProp(properties, "x_size", size, Scale);
                baseline[1] = baseline[1] * Scale;
                string xpath_elements = ".//*[@class='ocrx_word']";
                if (line.SelectNodes(xpath_elements).Count == 0)
                {
                    xpath_elements = ".";
                }
                foreach (XmlNode word in line.SelectNodes(xpath_elements))
                {
                    string rawtext = word.InnerText;
                    if (rawtext == "") continue;
                    var properties2 = HocrUtil.ReadTitleProperties(word.Attributes["title"].Value);
                    float[] box = { 0, 0, 0, 0 };
                    HocrUtil.FloatArrayFromProp(properties2, "bbox", box, Scale);
                    XFont font;
                    if (size[0] > 0)
                    {
                        font = new XFont(FontFamily.GenericSansSerif, size[0], XFontStyle.Regular);
                    }
                    else
                    {
                        font = BestFont(g, rawtext, box[2] - box[0], box[3] - box[1]);
                    }
                    g.DrawString(rawtext, font, brush, box[0], box[1], XStringFormats.TopLeft);
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
