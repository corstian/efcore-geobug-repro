using System;

namespace efcore_spatial_aggregateException
{
    public static class Extensions
    {
        public static int? ToInt(this string str)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(str)) return null;

                return Convert.ToInt32(str);
            }
            catch
            {
                return null;
            }
        }

        public static double? ToDouble(this string str)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(str)) return null;

                return Convert.ToDouble(str);
            }
            catch
            {
                return null;
            }
        }
    }
}
