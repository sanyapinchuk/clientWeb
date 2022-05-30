using System.Diagnostics.CodeAnalysis;

namespace ClientWeb.Models.Common
{
    public class mvcProductComparer : IEqualityComparer<mvcProduct>
    {
        public bool Equals(mvcProduct? x, mvcProduct? y)
        {
            if(x == null && y == null)
                return true;
            if(x == null || y == null)
                return false;
            if(x.Id != y.Id)
                return false;
            if(x.Name != y.Name)
                return false;
            //deefault auantity can be different
            return true;
        }

        public int GetHashCode([DisallowNull] mvcProduct obj)
        {
            return 0;
        }
    }
}
