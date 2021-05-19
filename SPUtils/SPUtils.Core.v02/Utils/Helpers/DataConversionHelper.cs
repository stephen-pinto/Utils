using System;
using System.Collections.Generic;
using System.Text;

namespace SPUtils.Core.v02.Utils.Helpers
{
    public static class DataConversionHelper
    {
        public static bool TryConvert<T>(string str, ref T value)
        {
            bool result = false;
            object nullable = null;

            //Verify if we have some data to parse
            if (string.IsNullOrEmpty(str))
                return false;

            if (typeof(T) == typeof(int) || typeof(T) == typeof(double))
            {
                //For double and integer we can use the same method for accuracy
                double tempValue;

                //Parsing based on globalization garentees safety for different locale based conversions
                if (double.TryParse(str, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out tempValue))
                {
                    if (typeof(T) == typeof(int))
                    {
                        int tempIntValue = (int)tempValue;
                        value = (T)Convert.ChangeType(tempIntValue, typeof(T));
                        return true;
                    }
                    else
                    {
                        value = (T)Convert.ChangeType(tempValue, typeof(T));
                        return true;
                    }
                }
            }
            else if (typeof(T) == typeof(float))
            {
                float tempValue;
                if (float.TryParse(str, out tempValue))
                {
                    value = (T)Convert.ChangeType(tempValue, typeof(T));
                    return true;
                }
            }
            else if (typeof(T) == typeof(decimal))
            {
                decimal tempValue;
                if (decimal.TryParse(str, out tempValue))
                {
                    value = (T)Convert.ChangeType(tempValue, typeof(T));
                    return true;
                }
            }

            if (typeof(T).IsValueType)
                value = (T)Activator.CreateInstance(typeof(T));
            else
                value = (T)nullable;

            return result;
        }
    }
}
