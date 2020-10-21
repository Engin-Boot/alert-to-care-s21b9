using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlertToCareApi.Models
{
    public class Beds
    {
        [Key]
        public int BedId { get; set; }
        public int IcuNo{ get; set; }
        [ForeignKey("IcuNo")]
        public bool OccupancyStatus { get; set; }
        public int BedSerialNo { get; set; }
    }
}
