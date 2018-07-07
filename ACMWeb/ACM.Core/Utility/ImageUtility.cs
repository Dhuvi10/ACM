using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.DrawingCore;
using System.IO;
using System.DrawingCore.Imaging;
using FreeImageAPI;

namespace ACM.Core.Utility
{
   public static class ImageUtility
    {
        public static string ErrMessage;
        
        public static bool ThumbnailCallback()
        {
            return false;
        }
        public static void Thumbnail(string path,string outpath)
        {
            const int size = 150;

            using (var original = FreeImageBitmap.FromFile(path))
            {
                int width, height;
                if (original.Width > original.Height)
                {
                    width = size;
                    height = original.Height * size / original.Width;
                }
                else
                {
                    width = original.Width * size / original.Height;
                    height = size;
                }
                var resized = new FreeImageBitmap(original, width, height);
                // JPEG_QUALITYGOOD is 75 JPEG.
                // JPEG_BASELINE strips metadata (EXIF, etc.)
                //resized.Save()
                resized.Save(outpath, FREE_IMAGE_FORMAT.FIF_JPEG,
                    FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD |
                    FREE_IMAGE_SAVE_FLAGS.JPEG_BASELINE);
            }
        }
    }
}
