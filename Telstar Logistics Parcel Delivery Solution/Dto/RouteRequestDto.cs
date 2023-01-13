using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Models;

public class RouteRequestDto
{
    public ParcelRequestDTO parcel { get; set; }
    public String startLocation{ get; set; }
    public String endDestination{ get; set; }
    public String? transitLocation{ get; set; }
}