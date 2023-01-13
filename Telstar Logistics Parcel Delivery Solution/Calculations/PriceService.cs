using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Calculations;

public interface PriceService
{
    public double CalculatePrice(Parcel parcel, List<int> route);
    public int CalculateDuration(List<int> route);
}