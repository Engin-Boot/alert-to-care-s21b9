using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedProjects.Models
{
    public class Patients
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string ContactNo { get; set; }
        [ForeignKey("BedId")]
        public int BedId { get; set; }

        public int MonitoringStatus { get; set; }
    }
}
