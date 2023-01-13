namespace Telstar_Logistics_Parcel_Delivery_Solution.Models;

public class Parcel
{
    public Parcel(int height, int width, int length, Category category, bool signature)
    {
        Height = height;
        Width = width;
        Length = length;
        Category = category;
        Signature = signature;
    }

    public  int Height { get; set; }

    public     int Width { get; set; }

    public  int Length { get; set; }
    
    public Category Category { get; set; }

    public Boolean Signature { get; set; }
}