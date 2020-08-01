using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace NorthWindApp.Helpers
{
    public static class ImageHelper
    {
        public static MemoryStream ConvertImage(this Stream originalStream, ImageFormat format)
        {
            Image image;
            try
            {
                image = Image.FromStream(originalStream);
            }
            catch (System.Exception)
            {
                image = Image.FromStream(new MemoryStream());
            }

            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
