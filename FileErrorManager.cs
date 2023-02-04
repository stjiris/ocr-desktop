namespace Tesseract_UI_Tools
{
    internal class FileErrorManager
    {
        string errorFilePath;
        List<string> files = new();

        public FileErrorManager(string outputFolder)
        {
            errorFilePath = Path.Combine(outputFolder, "errors.txt");
            if (!File.Exists(errorFilePath))
                return;
            using StreamReader sr = new(errorFilePath);
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line != null)
                    files.Add(line);
            }
        }

        public bool Contains(string file)
        {
            return files.Contains(file);
        }

        public void Add(string file)
        {
            files.Add(file);
        }

        public void Save()
        {
            if (files.Count == 0) return;

            using StreamWriter sw = new(errorFilePath);
            files.ForEach(f => sw.WriteLine(f));
        }
    }
}