namespace PixelizeFaces.Domain.Entities
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string Name { get; set; }

        public byte[] BeforePicture { get; set; }
        public byte[] AfterPicture { get; set; }
        public string ImageMimeType { get; set; }
    }
}