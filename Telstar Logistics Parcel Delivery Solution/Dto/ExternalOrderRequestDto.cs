namespace Models
{
    public class ExternalOrderRequestDto
    {
        public string start_destination { get; set; }
        public string stop_destination { get; set; }
        public string start_destination_arrival { get; set; }
        public DimensionsDto dimensions { get; set; }
        public string weight { get; set; }
        public string categories { get; set; }

        public ExternalOrderRequestDto(string start_destination, string stop_destination,
                                       string start_destination_arrival, DimensionsDto dimensions,
                                       string weight, string categories)
        {
            this.start_destination = start_destination;
            this.stop_destination = stop_destination;
            this.start_destination_arrival = start_destination_arrival;
            this.dimensions = dimensions;
            this.weight = weight;
            this.categories = categories;
        }
    }
}