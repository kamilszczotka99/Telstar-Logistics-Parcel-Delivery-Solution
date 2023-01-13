namespace Telstar_Logistics_Parcel_Delivery_Solution.Calculations;

public interface RouteMapperService
{
    bool GetDuration();

    List<int> Execute(List<(int, int, int)> cityMap, int start, int end);
}