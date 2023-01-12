
namespace Models
{
    public class ExternalOrderResponseDto
    {
        public String delivery_cost { get; set; }
        public String delivery_time { get; set; }

        public ExternalOrderResponseDto(string delivery_cost, string delivery_time)
        {
            this.delivery_cost = delivery_cost;
            this.delivery_time = delivery_time;
        }

        public override string ToString()
        {
            return $"delivery_cost: {delivery_cost}, delivery_time: {delivery_time}";
        }
    }
}