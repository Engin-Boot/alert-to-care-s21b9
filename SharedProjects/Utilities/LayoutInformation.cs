using System.Collections.Generic;
using System.Linq;
using SharedProjects.Models;

namespace SharedProjects.Utilities
{
    public class LayoutInformation
    {
        public static readonly List<Layouts> Layouts = new List<Layouts>
        {
            new Layouts{ LayoutId = 1 , LayoutType = "CLUSTER", Capacity = 24},
            new Layouts{ LayoutId = 2 , LayoutType = "TRIANGULAR", Capacity = 22 },
            new Layouts{ LayoutId = 3 , LayoutType = "U-SHAPED", Capacity = 12 },
            new Layouts{ LayoutId = 4 , LayoutType = "RADIAL", Capacity = 8 }
        };

        readonly ConfigDbContext _context = new ConfigDbContext();
        public int FindNoOfIcus(int layoutId)
        {
            var icuStore = _context.Icu.ToList();
            var icuInEachlayout = icuStore.Count(item => item.LayoutId == layoutId);
            return icuInEachlayout;
        }

        public List<Icu> FindListOfIcus(int layoutId)
        {
            var icuStore = _context.Icu.ToList();
            var listOfIcuInEachlayout = icuStore.Where(item => item.LayoutId == layoutId).ToList();
            return listOfIcuInEachlayout;
        }
    }
}
