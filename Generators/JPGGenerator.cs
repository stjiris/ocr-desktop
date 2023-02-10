﻿using IRIS_OCR_Desktop;
using System.ComponentModel;

namespace IRIS_OCR_Desktop.Generators
{
    public class JPGGenerator : ATiffPagesGenerator
    {
        public static new readonly string[] FORMATS = new string[] { "jpg", "jpeg" };

        public JPGGenerator(string FilePath) : base(FilePath) { }

        public override string[] GenerateTIFFs(string FolderPath, bool Overwrite = false)
        {
            string Out = Path.Combine(FolderPath, $"1.tiff");
            using Image Curr = Image.FromFile(FilePath);
            Curr.Save(Out, System.Drawing.Imaging.ImageFormat.Tiff);
            return new string[] { Out };

        }
    }
}
