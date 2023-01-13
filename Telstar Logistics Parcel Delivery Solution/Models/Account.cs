using System.ComponentModel.DataAnnotations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
    }
}
