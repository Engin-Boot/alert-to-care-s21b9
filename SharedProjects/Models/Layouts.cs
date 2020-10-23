using System.Collections.Generic;

namespace SharedProjects.Models
{
    public class Layouts
    {
        public int LayoutId { get; set; }
        public string LayoutType { get; set; }
        public int Capacity { get; set; }
        public int NoOfIcus { get; set; }
        public List<Icu> ListOfIcus { get; set; }
    }
}
