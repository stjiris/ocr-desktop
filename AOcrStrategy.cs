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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public abstract void GenerateTsv(string TiffPage, string TsvPage);

        public static AOcrStrategy? GetStrategy(string Strategy, string[] Languages)
        {
            Type? mType = StrategiesTypes().FirstOrDefault(mType => (mType.GetField("StrategyName")?.GetValue(null) as string) == Strategy);
            if (mType == null) return null;
            try
            {
                ConstructorInfo? cinfo = mType.GetConstructor(new Type[] { typeof(string[]) });
                if (cinfo == null) throw new ArgumentException("ConstructorInfo not available");
                return cinfo.Invoke(new object[] { Languages }) as AOcrStrategy;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public static string[] Strategies() => StrategiesTypes().Select(mType => mType.GetField("StrategyName")?.GetValue(null) as string ?? "").Where(o => o != "").ToArray<string>();

        private static IEnumerable<Type> StrategiesTypes()
        {
            return Assembly.GetAssembly(typeof(AOcrStrategy))?.GetTypes()?.Where(mType => mType.IsClass && !mType.IsAbstract && mType.IsSubclassOf(typeof(AOcrStrategy)) && mType.GetField("StrategyName") != null) ?? Array.Empty<Type>();
        }
    }
}