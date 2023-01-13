using Models;

namespace Models
{
    public class ExternalOrderRequestDto
    {
        public string cityTo { get; set; }
        public string cityFrom { get; set; }
        public string deliveryTime { get; set; }
        public DimensionsDto dimensions { get; set; }
        public string weight { get; set; }
        public List<String> categories { get; set; }
        public ExternalOrderRequestDto(string cityTo, string cityFrom, string deliveryTime, DimensionsDto dimensions, string weight, List<String> categories)
        {
            this.cityTo = cityTo;
            this.cityFrom = cityFrom;
            this.deliveryTime = deliveryTime;
            this.dimensions = dimensions;
            this.weight = weight;
            this.categories = categories;
        }
    }
}