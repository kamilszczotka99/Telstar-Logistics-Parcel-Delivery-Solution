
namespace Models
{
    public class ExternalOrderResponseDto
    {
        public String cost { get; set; }
        public String duration { get; set; }

        public ExternalOrderResponseDto(string cost, string duration)
        {
            this.cost = cost;
            this.duration = duration;
        }
    }
}