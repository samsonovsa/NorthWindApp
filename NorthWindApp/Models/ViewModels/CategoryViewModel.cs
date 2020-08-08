using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NorthWindApp.Models.ViewModels
{
    public class CategoryViewModel
    {
        private int garbageBytesInDbImage = 78;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public byte[] Image 
        {
            get 
            {
                if(Picture !=null)
                {
                    using (var stream = new MemoryStream(Picture.Skip(garbageBytesInDbImage).ToArray()))
                    {
                        Picture = stream.ToArray();
                    }
                }

                return Picture;
            }
        }

        private IFormFile _imageUpload;
        public IFormFile ImageUpload
        {
            get { return _imageUpload; }
            set
            {
                if (value != null)
                {
                    byte[] imageData = null;
                    List<byte> garbage = Enumerable.Repeat((byte)0x20, garbageBytesInDbImage).ToList();
                    using (var binaryReader = new BinaryReader(value.OpenReadStream()))
                    {
                        garbage.AddRange(binaryReader.ReadBytes((int)value.Length));
                        imageData = garbage.ToArray();
                    }
                    Picture = imageData;
                    _imageUpload = value;
                }
            }
        }
    }
}
