using System.Linq;
using System.Reflection;

namespace Application.Credit.Common.Utils
{
    public static class GetUpdatedFields
    {
        public static void updatedFields<T>(ref T objOld, T obj) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var oldValue = property.GetValue(objOld);
                var newValue = property.GetValue(obj);
                if (!AllEqual(oldValue, newValue))
                {
                    property.SetValue(objOld, newValue);
                }
            }
        }

        public static bool AllEqual<T>(params T[] values)
        {
            if (values == null || values.Length == 0)
                return true;
            if (values.All(v => v == null)) return true;
            return values.All(v => v.Equals(values[0]));
        }
    }
}
