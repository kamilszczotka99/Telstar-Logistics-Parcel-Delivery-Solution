namespace Models;

public class RouteResponseDTO
{
    public RouteResponseDTO(List<string> cities, double cost, double duration)
    {
        Cities = cities;
        Cost = cost;
        Duration = duration;
    }

    public List<String> Cities { get; set; }
    public double Cost { get; set; }
    public double Duration { get; set; }
}