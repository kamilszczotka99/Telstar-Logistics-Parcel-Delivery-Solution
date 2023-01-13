using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    [Keyless]
    public class Booking
    {
        public string CityName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }

        public bool Active { get; set; }

        public string NormalizedName { get; set; }

        public decimal FeePercent { get; set; }

        public decimal AddedFee { get; set; }
        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public string CategoryName { get; set; }

        public Boolean Signature { get; set; }
    }
}
