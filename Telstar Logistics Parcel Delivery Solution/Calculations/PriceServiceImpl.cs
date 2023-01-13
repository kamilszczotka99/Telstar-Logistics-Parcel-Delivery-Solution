using Microsoft.EntityFrameworkCore;
using Models;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Calculations;

public class PriceServiceImpl: PriceService
{ 
    public Random rnd = new Random();
    private ApplicationDbContext _dbContext;
    public PriceServiceImpl(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public int CalculateDuration(List<int> route)
    {
        int distance = 0;
        var edges = _dbContext.Edges.ToList();
        if (route.Count < 2)
        {
            return -1;
        }

        int i = 0;
        while (i < route.Count - 1)
        {                        
            foreach (var e in edges)
            {
                if (e.IdSource == route[i] && e.IdTarget == route[i + 1])
                {
                    if (e.Distance > 0)
                    {
                        distance += (e.Distance * 4);
                    }
                    else if (e.Distance == -1)
                    {
                        distance += 3;
                    }
                    else
                    {
                        distance = +(rnd.Next(3, 8) * 8);
                    }
                    i++;
                    break;
                }
            }
        }
        return distance;
    }

    public double CalculatePrice(ParcelRequestDTO parcel, List<int> route)
    {
        
        // returns random integers >= 10 and < 20
       
        // Tell alle edges i ruten.
        var edges = _dbContext.Edges.ToList();
        double price = 0;
        int segmentCount = 0;
        
        int i = 0;
        while (i < route.Count - 1)
        {                        
            foreach (var e in edges)
            {
                if (e.IdSource == route[i] && e.IdTarget == route[i + 1])
                {
                    if (e.Distance > 0)
                    {
                        price += (e.Distance * 3);
                    }
                    else if (e.Distance == -1)
                    {
                        price += 3;
                    }
                    else
                    {
                        price= +(rnd.Next(3, 8) * 8);
                    }
                    i++;
                    break;
                }
            }
        }

        if (parcel.Category.Equals("Refrigerated goods"))
        {
            price *= 1.1;
        }
        
        if (parcel.Category.Equals("Cautious parcels"))
        {
            price *= 1.75;
        }

        
        if (parcel.Category.Equals("Live animals"))
        {
            price *= 1.5;
        }

        
        if (parcel.Signature)
        {
            price += 10;
        }
        
        

        //price += _dbContext.Category.Where(x => x.CategoryType.Equals(parcel.Category)).FirstOrDefault();
        double multiplier;
        
        
        //price *= _dbContext.Category.Find(x=> x.CategoryType.Equals(parcel.Category));

        return price;
    }
}