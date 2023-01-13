using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    [Keyless]
    public class Account
    {
        
        
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
    }
}
