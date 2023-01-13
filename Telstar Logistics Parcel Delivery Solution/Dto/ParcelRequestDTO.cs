namespace Models;

public class ParcelRequestDTO
{
    public ParcelRequestDTO(int height, int width, int length, string category, bool signature, double weight)
    {
        Height = height;
        Weight = weight;
        Width = width;
        Length = length;
        Category = category;
        Signature = signature;
    }

    public  int Height { get; set; }
    
    public double Weight { get; set; }

    public     int Width { get; set; }

    public  int Length { get; set; }
    
    public string Category { get; set; }

    public Boolean Signature { get; set; }
}