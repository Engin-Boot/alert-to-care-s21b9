using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedProjects.Models
{
    public class VitalsLogs
    {
        [Key]
        public int VitalsLogId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientId { get; set; }
        public double BpmRate { get; set; }
        public double Spo2Rate { get; set; }
        public double RespRate { get; set; }
    }
}
