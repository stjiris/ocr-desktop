using System.IO.Compression;
using OpenCvSharp.Text;
using System.Configuration;

namespace Tesseract_UI_Tools
{
    public class TessdataUtil
    {
        public static readonly string TessdataURL = "https://github.com/tesseract-ocr/tessdata_fast/archive/refs/heads/main.zip";
        public static readonly string TessdataPath = Path.Combine(Application.CommonAppDataPath, "tessdata_fast-main/");

        public static async Task<string[]> Setup()
        {
            if( Directory.Exists(TessdataPath))
            {
                return GetLanguages();
            }
            
            using (HttpClient client = new HttpClient())
            {
                Task<Stream> request = client.GetStreamAsync(TessdataURL);
                string TmpFile = Path.GetTempFileName();
                using (FileStream TmpFileStream = new FileStream(TmpFile, FileMode.Open, FileAccess.Write))
                {
                    (await request).CopyTo(TmpFileStream);
                }
                ZipFile.ExtractToDirectory(TmpFile, Application.CommonAppDataPath);
                File.Delete(TmpFile);
            }

            return GetLanguages();
        }

        public static string[] GetLanguages()
        {
            return Directory.GetFiles(TessdataPath, "*.traineddata", SearchOption.AllDirectories).Select(lang => lang.Substring(TessdataPath.Length, lang.Length - TessdataPath.Length - ".traineddata".Length)).ToArray();
        }

        public static string LanguagesToString(string[] Languages)
        {
            return string.Join("+", Languages);
        }

        public static OCRTesseract CreateEngine(string[] Languages)
        {
            return OCRTesseract.Create(TessdataPath, LanguagesToString(Languages));
        }
    }

    public class TesseractUIParameters : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string InputFolder
        {
            get { return (string)this["InputFolder"]; }
            set { this["InputFolder"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string OutputFolder
        {
            get { return (string)this["OutputFolder"]; }
            set { this["OutputFolder"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("eng")]
        public string Language
        {
            get { return (string)this["Language"]; }
            set { this["Language"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("100")]
        public int Dpi
        {
            get { return (int)this["Dpi"]; }
            set { this["Dpi"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("100")]
        public int Quality
        {
            get { return (int)this["Quality"]; }
            set { this["Quality"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("false")]
        public bool Overwrite
        {
            get { return (bool)this["Overwrite"]; }
            set { this["Overwrite"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("false")]
        public bool Clear
        {
            get { return (bool)this["Clear"]; }
            set { this["Clear"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("25")]
        public float MinimumConfidence
        {
            get { return (float)this["MinimumConfidence"]; }
            set { this["MinimumConfidence"] = value; }
        }

        public void SetLanguage(string[] Languages)
        {
            Language = TessdataUtil.LanguagesToString(Languages);
        }

        public string[] GetLanguage()
        {
            return Language.Split("+");
        }

        public bool Validate()
        {
            return
                Directory.Exists(InputFolder) &&
                Directory.Exists(OutputFolder) &&
                Language != string.Empty &&
                Dpi >= 70 && Dpi <= 300 &&
                Quality >= 0 && Quality <= 100;
        }
    }
}
