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
            return Directory.GetFiles(TessdataPath, "*.traineddata").Select(lang => lang.Substring(TessdataPath.Length, lang.Length - TessdataPath.Length - ".traineddata".Length)).Where(c => _code2lang.ContainsKey(c)).ToArray();
        }

        public static string LanguagesToString(string[] Languages)
        {
            return string.Join("+", Languages.Distinct() );
        }

        private static Dictionary<string, string> _code2lang = new Dictionary<string, string>()
        {
            {"afr","Afrikaans"},
            {"amh","Amharic"},
            {"ara","Arabic"},
            {"asm","Assamese"},
            {"aze","Azerbaijani"},
            {"aze_cyrl","Azerbaijani - Cyrilic"},
            {"bel","Belarusian"},
            {"ben","Bengali"},
            {"bod","Tibetan"},
            {"bos","Bosnian"},
            {"bre","Breton"},
            {"bul","Bulgarian"},
            {"cat","Catalan; Valencian"},
            {"ceb","Cebuano"},
            {"ces","Czech"},
            {"chi_sim","Chinese - Simplified"},
            {"chi_sim_vert","Chinese - Simplified (Vertical)"},
            {"chi_tra","Chinese - Traditional"},
            {"chi_tra_vert","Chinese - Traditional (Vertical)"},
            {"chr","Cherokee"},
            {"cos","Corsican"},
            {"cym","Welsh"},
            {"dan","Danish"},
            {"dan_frak","Danish - Fraktur (contrib)"},
            {"div","Dhivehi"},
            {"deu","German"},
            {"deu_frak","German - Fraktur (contrib)"},
            {"dzo","Dzongkha"},
            {"ell","Greek, Modern (1453-)"},
            {"eng","English"},
            {"enm","English, Middle (1100-1500)"},
            {"epo","Esperanto"},
            {"equ","Math / equation detection module"},
            {"est","Estonian"},
            {"eus","Basque"},
            {"fao","Faroese"},
            {"fas","Persian"},
            {"fil","Filipino (old - Tagalog)"},
            {"fin","Finnish"},
            {"fra","French"},
            {"frk","German - Fraktur"},
            {"frm","French, Middle (ca.1400-1600)"},
            {"fry","Western Frisian"},
            {"gla","Scottish Gaelic"},
            {"gle","Irish"},
            {"glg","Galician"},
            {"grc","Greek, Ancient (to 1453) (contrib)"},
            {"guj","Gujarati"},
            {"hat","Haitian; Haitian Creole"},
            {"heb","Hebrew"},
            {"hin","Hindi"},
            {"hrv","Croatian"},
            {"hun","Hungarian"},
            {"hye","Armenian"},
            {"iku","Inuktitut"},
            {"ind","Indonesian"},
            {"isl","Icelandic"},
            {"ita","Italian"},
            {"ita_old","Italian - Old"},
            {"jav","Javanese"},
            {"jpn","Japanese"},
            {"jpn_vert","Japanese (Vertical)"},
            {"kan","Kannada"},
            {"kat","Georgian"},
            {"kat_old","Georgian - Old"},
            {"kaz","Kazakh"},
            {"khm","Central Khmer"},
            {"kir","Kirghiz; Kyrgyz"},
            {"kmr","Kurmanji (Kurdish - Latin Script)"},
            {"kor","Korean"},
            {"kor_vert","Korean (vertical)"},
            {"kur","Kurdish (Arabic Script)"},
            {"lao","Lao"},
            {"lat","Latin"},
            {"lav","Latvian"},
            {"lit","Lithuanian"},
            {"ltz","Luxembourgish"},
            {"mal","Malayalam"},
            {"mar","Marathi"},
            {"mkd","Macedonian"},
            {"mlt","Maltese"},
            {"mon","Mongolian"},
            {"mri","Maori"},
            {"msa","Malay"},
            {"mya","Burmese"},
            {"nep","Nepali"},
            {"nld","Dutch; Flemish"},
            {"nor","Norwegian"},
            {"oci","Occitan (post 1500)"},
            {"ori","Oriya"},
            {"osd","Orientation and script detection module"},
            {"pan","Panjabi; Punjabi"},
            {"pol","Polish"},
            {"por","Portuguese"},
            {"pus","Pushto; Pashto"},
            {"que","Quechua"},
            {"ron","Romanian; Moldavian; Moldovan"},
            {"rus","Russian"},
            {"san","Sanskrit"},
            {"sin","Sinhala; Sinhalese"},
            {"slk","Slovak"},
            {"slk_frak","Slovak - Fraktur (contrib)"},
            {"slv","Slovenian"},
            {"snd","Sindhi"},
            {"spa","Spanish; Castilian"},
            {"spa_old","Spanish; Castilian - Old"},
            {"sqi","Albanian"},
            {"srp","Serbian"},
            {"srp_latn","Serbian - Latin"},
            {"sun","Sundanese"},
            {"swa","Swahili"},
            {"swe","Swedish"},
            {"syr","Syriac"},
            {"tam","Tamil"},
            {"tat","Tatar"},
            {"tel","Telugu"},
            {"tgk","Tajik"},
            {"tgl","Tagalog (new - Filipino)"},
            {"tha","Thai"},
            {"tir","Tigrinya"},
            {"ton","Tonga"},
            {"tur","Turkish"},
            {"uig","Uighur; Uyghur"},
            {"ukr","Ukrainian"},
            {"urd","Urdu"},
            {"uzb","Uzbek"},
            {"uzb_cyrl","Uzbek - Cyrilic"},
            {"vie","Vietnamese"},
            {"yid","Yiddish"},
            {"yor","Yoruba"}
        };

        public static string Code2Lang(string Code)
        {
            return _code2lang.GetValueOrDefault(Code, "");
        }

        public static string Lang2Code(string Lang)
        {
            foreach( KeyValuePair<string, string> codeLang in _code2lang)
            {
                if( codeLang.Value == Lang)
                {
                    return codeLang.Key;
                }
            }
            return "";
        }

        public static OCRTesseract CreateOpenCvEngine(string[] Languages)
        {
            return OCRTesseract.Create(TessdataPath, LanguagesToString(Languages), ""); // Reset empty whitelist
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

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("Plain")]
        public string Strategy
        {
            get { return (string)this["Strategy"]; }
            set { this["Strategy"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("false")]
        public bool DebugPDF
        {
            get { return (bool)this["DebugPDF"]; }
            set { this["DebugPDF"] = value; }
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
                Quality >= 0 && Quality <= 100 &&
                Language.Length > 0;
        }
    }
}
