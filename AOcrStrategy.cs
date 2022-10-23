using System.Reflection;
namespace Tesseract_UI_Tools
{
    public abstract class AOcrStrategy : IDisposable
    {
        protected string[] LanguagesArray;
        protected string LanguagesString;
        public AOcrStrategy(string[] Languages)
        {
            LanguagesArray = Languages;
            LanguagesString = TessdataUtil.LanguagesToString(Languages);
        }

        public abstract void Dispose();

        public abstract void GenerateTsv(string TiffPage, string TsvPage);

        public static AOcrStrategy GetStrategy(string Strategy, string[] Languages)
        {
            Type? mType = _StrategiesTypes().FirstOrDefault(mType => ((string)mType.GetField("StrategyName").GetValue(null)) == Strategy);
            if (mType == null) throw new Exception("Invalid Strategy Selected");

            return (AOcrStrategy)mType.GetConstructor(new Type[] { typeof(string[]) }).Invoke(new object[] { Languages });
        }

        public static string[] Strategies()
        {
            return _StrategiesTypes().Select(mType => (string)mType.GetField("StrategyName").GetValue(null)).ToArray<string>();
        }

        private static IEnumerable<Type> _StrategiesTypes()
        {
            return Assembly.GetAssembly(typeof(AOcrStrategy)).GetTypes()
                .Where(mType => mType.IsClass && !mType.IsAbstract && mType.IsSubclassOf(typeof(AOcrStrategy)) && mType.GetField("StrategyName") != null);
        }
    }
}