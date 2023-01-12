namespace Models
{
    public class DimensionsDto
    {
        public String height { get; set; }
        public String width { get; set; }
        public String length { get; set; }

        public DimensionsDto(string height, string width, string length)
        {
            this.height = height;
            this.width = width;
            this.length = length;
        }
    }
}
