using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlertToCareApi.Models
{
    
    public class Beds
    {
        [Key] private protected int BedId { get; set; }
        private protected int IcuNo{ get; set; }
        [ForeignKey("IcuNo")] private protected bool OccupancyStatus { get; set; }

        private protected  int BedSerialNo { get; set; }

    }
    
}
