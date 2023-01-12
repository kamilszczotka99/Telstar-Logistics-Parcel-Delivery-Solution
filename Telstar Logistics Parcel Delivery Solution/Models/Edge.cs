using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Models
{
    [Keyless]
    public class Edge
    {
        
        public int IdSource { get; set; }
        public int IdTarget { get; set; }  
        public int Distance { get; set; }

    }
}
