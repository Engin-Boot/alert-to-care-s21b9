using System.Linq;

namespace SharedProjects.Utilities
{
    public class BedIdentification
    {
        readonly ConfigDbContext _context = new ConfigDbContext();

        private int FindLayoutIdForBed(int icuNo)
        {
            var icuStore = _context.Icu.ToList();
            var icu = icuStore.FirstOrDefault(item => item.IcuNo == icuNo);
            if (icu == null)
                return 0;
            var layoutId = icu.LayoutId;
            return layoutId;
        }
        private int FindCapacityOfLayout(int icuNo)
        {
            int layoutId = FindLayoutIdForBed(icuNo);
            var layoutStore = LayoutInformation.Layouts;
            var layout = layoutStore.FirstOrDefault(item => item.LayoutId == layoutId);
            if (layout == null)
                return 0;
            var capacity = layout.Capacity;
            return capacity;
        }

        public int FindCountOfBeds(int icuNo)
        {
            var bedStore = _context.Beds.ToList();
            var bedsInEachIcu = bedStore.Count(item => item.IcuNo == icuNo);
            return bedsInEachIcu;
        }

        public int FindBedSerialNo(int icuNo)
        {
            int capacity = FindCapacityOfLayout(icuNo);
            int countofbeds = FindCountOfBeds(icuNo);
            if (capacity == countofbeds)
            {
                return 0;
            }
            else
            {
                int serialNo = countofbeds + 1;
                return serialNo;
            }


        }
    }
}
