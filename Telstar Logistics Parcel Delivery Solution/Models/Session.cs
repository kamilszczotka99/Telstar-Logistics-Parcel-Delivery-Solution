using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    public class Session
    {
        public string Email { get; set; }
        [Key]
        public string Token { get; set; }
    }
}
