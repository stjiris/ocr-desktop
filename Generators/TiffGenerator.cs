using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Tesseract_UI_Tools.Generators
{
    internal class TiffGenerator : ATiffPagesGenerator
    {
        public static new string[] FORMATS = new string[] { "tiff", "tif" };
        public TiffGenerator(string FilePath) : base(FilePath) { }

        private static FrameDimension PAGE = FrameDimension.Page;

        public override string[] GenerateTIFFs(string FolderPath, bool Overwrite = false, IProgress<float>? Progress = null)
        {
            string[] Pages;
            using (Bitmap Tiff = (Bitmap)Image.FromStream(File.OpenText(FilePath).BaseStream))
            {
                int PagesNumber = Tiff.GetFrameCount(PAGE);
                Pages = new string[PagesNumber];
                for (int i = 0; i < PagesNumber; i++)
                {
                    if (Progress != null) Progress.Report((float)i / PagesNumber);

                    string FullName = Path.Combine(FolderPath, TiffPage(i));
                    Pages[i] = FullName;
                    if (File.Exists(FullName) && !Overwrite) continue;

                    Tiff.SelectActiveFrame(PAGE, i);
                    Tiff.Save(FullName, ImageFormat.Tiff);
                    Pages[i] = FullName;
                }
            }
            return Pages;
        }
    }
}
