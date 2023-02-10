﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.ComponentModel;
using IRIS_OCR_Desktop;

namespace IRIS_OCR_Desktop.Generators
{
    internal class TiffGenerator : ATiffPagesGenerator
    {
        public static readonly new string[] FORMATS = new string[] { "tiff", "tif" };
        public TiffGenerator(string FilePath) : base(FilePath) { }

        private static readonly FrameDimension PAGE = FrameDimension.Page;

        public override string[] GenerateTIFFs(string FolderPath, bool Overwrite = false)
        {
            string[] Pages;
            using Bitmap Tiff = (Bitmap)Image.FromStream(File.OpenText(FilePath).BaseStream);
            int PagesNumber = Tiff.GetFrameCount(PAGE);
            Pages = new string[PagesNumber];
            for (int i = 0; i < PagesNumber && (worker == null || !worker.CancellationPending); i++)
            {
                if (Progress != null) Progress.Report((float)i / PagesNumber);

                string FullName = Path.Combine(FolderPath, TiffPage(i));
                Pages[i] = FullName;
                if (File.Exists(FullName) && !Overwrite) continue;

                Tiff.SelectActiveFrame(PAGE, i);
                Tiff.Save(FullName, ImageFormat.Tiff);
                Pages[i] = FullName;
            }
            return Pages;
        }
    }
}
