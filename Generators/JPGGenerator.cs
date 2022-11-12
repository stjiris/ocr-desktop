using System.ComponentModel;

namespace Tesseract_UI_Tools.Generators
{
    public class JPGGenerator : ATiffPagesGenerator
    {
        public static new string[] FORMATS = new string[] { "jpg", "jpeg" };

        public JPGGenerator(string FilePath) : base(FilePath) { }

        public override string[] GenerateTIFFs(string FolderPath, bool Overwrite = false, IProgress<float>? Progress = null, BackgroundWorker? worker = null)
        {
            string Out = Path.Combine(FolderPath, $"1.tiff");
            using (Image Curr = Image.FromFile(FilePath))
            {
                Curr.Save(Out, System.Drawing.Imaging.ImageFormat.Tiff);
            }
            return new string[] { Out };

        }
    }
}
