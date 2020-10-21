using AlertToCareApi.Utilities;
using System.Linq;

namespace AlertToCareApi.EntriesValidator
{
    public class IcuValidator
    {
        private static bool CheckIfIcuIsPresent(int icuNo)
        {
            ConfigDbContext context = new ConfigDbContext();
            var icuStore = context.Icu.ToList();
            if (icuStore.FirstOrDefault(item => item.IcuNo == icuNo) == null)
            {
                return false;
            }
            return true;
        }

        private static bool CheckIfIcuIsFull(int icuNo)
        {
            BedIdentification bedIdentification = new BedIdentification();
            var bedSerialNo = bedIdentification.FindBedSerialNo(icuNo);
            if (bedSerialNo == 0)
            {
                return true;
            }
            return false;
        }

        public static void CheckForValidIcu(int icuNo, ref string message)
        {
            bool validIcu = false;
            CheckForValidIcu(icuNo, ref validIcu, ref message);
        }

        public static void CheckForValidIcu(int icuNo, ref bool validIcu, ref string message)
        {
            if (CheckIfIcuIsPresent(icuNo))
            {
                if (!CheckIfIcuIsFull(icuNo))
                {
                    validIcu = true;
                    message = "";
                }
                else
                {
                    validIcu = false;
                    message = "No More Beds Can be Added In This ICU Layout, ICU Is Full";
                }
            }
            else
            {
                validIcu = false;
                message = "The Inserted ICU No Is Not Available";
            }
        }
    }
}
