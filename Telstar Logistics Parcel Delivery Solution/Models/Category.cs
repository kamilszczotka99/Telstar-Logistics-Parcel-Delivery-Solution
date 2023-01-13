using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    [Keyless]
    public class Category
    {  
        [MaxLength (255)]
        public string CategoryType { get; set; }
        public decimal FeePercent { get; set; }
        [MaxLength(255)]
        public decimal AddedFee { get; set; }


    }
}
