using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Common;


namespace YixiaoAdmin.Common
{
    public class BarCodeHelper
    {
        /// <summary>
        /// 生成条形码,保存成图片，使用了ZXing
        /// </summary>
        public static Image GenerateQRimage(string content)
        {
            //BarcodeWriter writer = new BarcodeWriter()
            //{
            //    Format = BarcodeFormat.CODE_128,
            //    Options = new EncodingOptions
            //    {
            //        Height = 400,
            //        Width = 800,
            //        PureBarcode = false,
            //        Margin = 10,
            //    },
            //};
            //初始化条形码格式，宽高，以及PureBarcode = true则不会留白框
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions { Height = 100, Width = 200 }
            };
            var pixelData = writer.Write(content);

            var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //return bitmap;
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                   System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                       pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                // save to stream as PNG
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Image outputImg = Image.FromStream(ms);
               
                return outputImg;
            }
            
        }



    }
}
