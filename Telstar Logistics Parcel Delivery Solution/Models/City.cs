using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    [Keyless]
    public class City
    {
        
        public int ID { get; set; }
        [MaxLength(255)]
        public string CityName { get; set; }
        [MaxLength(255)]
        public string NormalizedName { get; set; }

    }
}
