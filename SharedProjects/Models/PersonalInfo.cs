using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProjects.Models
{
    public class PersonalInfo
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public int ContactNumber { get; set; }
        public string Status { get; set; }
        public int BedNo { get; set; }
    }
}
