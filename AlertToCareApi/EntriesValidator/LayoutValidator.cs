using AlertToCareApi.Utilities;
using System.Linq;

namespace AlertToCareApi.EntriesValidator
{
    public abstract class LayoutValidator
    {
        public static bool CheckIfLayoutIsPresent(int layoutId)
        {
            var layoutStore = LayoutInformation.Layouts;
            if (layoutStore.FirstOrDefault(item => item.LayoutId == layoutId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
