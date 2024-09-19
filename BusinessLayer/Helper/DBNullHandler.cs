namespace ClothingPro.BusinessLayer.Helper;
public class DBNullHandler
{
    /// <summary>
    /// Convert database value to DateTime or null  if DBNull   
    /// </summary>
    public static string FormatDateTime(object value)
    {
        try
        {
            if (value != null && Convert.ToString(value) != "")
            {
                return Convert.ToDateTime(value).ToString("MM/dd/yyyy");
            }

            else
            {
                return "";

            }
        }
        catch { return ""; }
    }


    /// <summary>
    /// Convert database value to DateTime or null  if DBNull   
    /// </summary>
    public static DateTime? DateTime(object value)
    {
        try
        {
            if (value != DBNull.Value && Convert.ToString(value) != "")
            {
                return Convert.ToDateTime(value);
            }
            else
            {
                return null;

            }
        }
        catch { return null; }
    }

    /// <summary>
    /// Convert database value to DateTime or defaultValue  if DBNull 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime? DateTime(object value, DateTime defaultValue)
    {
        if (value != DBNull.Value)
        {
            return Convert.ToDateTime(value);
        }
        else
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Convert database value to int or defaultValue  if DBNull   
    /// </summary>
    public static int Int(object value, int defaultValue)
    {
        try
        {
            if (value != DBNull.Value)
            {
                return int.Parse(value.ToString());
            }
            else
            {
                return defaultValue;
            }
        }
        catch { return defaultValue; }
    }

    /// <summary>
    /// Convert database value to Int32 or null  if DBNull   
    /// </summary>
    public static int? Int(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return int.Parse(value.ToString());
            }
            catch { return null; }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Convert database value to Int32 or null  if DBNull   
    /// </summary>
    public static Int32? Int32(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch { return null; }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Convert database value to Int32 or null  if DBNull   
    /// </summary>
    public static Int64? Int64(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch { return null; }
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Convert database value to Int32 or defaultValue  if DBNull   
    /// </summary>
    public static Int32 Int32(object value, int defaultValue)
    {
        try
        {
            if (value != DBNull.Value)
            {
                return Convert.ToInt32(value);
            }
            else
            {
                return defaultValue;
            }
        }
        catch { return defaultValue; }
    }

    /// <summary>
    /// Convert database value to Int16 or null  if DBNull   
    /// </summary>
    public static Int16? Int16(object value)
    {
        if (value != DBNull.Value)
        {
            return Convert.ToInt16(value);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Convert database value to Double or null  if DBNull   
    /// </summary>
    public static Double? Double(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch { return 0; }
        }
        else
        {
            return null;
        }
    }

    public static string FormatSingleDecimal(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(value).ToString("0.0");
                // return string.Format("{0:0.00}", value);
            }
            catch { return ""; }
        }
        else
        {
            return "";
        }
    }
    public static string FormatDecimal(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(value).ToString("0.00");
                // return string.Format("{0:0.00}", value);
            }
            catch { return ""; }
        }
        else
        {
            return "";
        }
    }
    public static string FormatDecimal3(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(value).ToString("0.000");
                // return string.Format("{0:0.00}", value);
            }
            catch { return ""; }
        }
        else
        {
            return "";
        }
    }
    public static string FormatDecimal4(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(value).ToString("0.0000");
            }
            catch { return ""; }
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// Convert database value to Decimal or null  if DBNull   
    /// </summary>
    public static Decimal? Decimal(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(string.Format("{0:0.00}", value));
            }
            catch { return 0; }
        }
        else
        {
            return 0;
        }
    }
    public static Decimal? DecimalSingle(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(string.Format("{0:0.0}", value));
            }
            catch { return 0; }
        }
        else
        {
            return 0;
        }
    }
    public static Decimal Decimal3(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(string.Format("{0:0.000}", value));
            }
            catch { return 0; }
        }
        else
        {
            return 0;
        }
    }
    public static Decimal Decimal4(object value)
    {
        if (value != DBNull.Value)
        {
            try
            {
                return Convert.ToDecimal(string.Format("{0:0.0000}", value));
            }
            catch { return 0; }
        }
        else
        {
            return 0;
        }
    }
    public static Decimal Decimal(object value, decimal defaultValue)
    {
        try
        {
            if (value != DBNull.Value)
            {
                return Convert.ToDecimal(string.Format("{0:0.00}", value));
            }
            else
            {
                return defaultValue;
            }
        }
        catch { return defaultValue; }
    }

    /// <summary>
    /// Convert database value to Boolean or false  if DBNull   
    /// </summary>
    public static Boolean Boolean(object value)
    {
        return value != DBNull.Value ? Convert.ToBoolean(value) : false;
    }


    /// <summary>
    /// Convert database value to String or empty string  if DBNull   
    /// </summary>
    public static string String(object value)
    {
        try
        {
            if (value != DBNull.Value)
                return value.ToString();
            else
                return "";
        }
        catch { return ""; }
    }

    /// <summary>
    /// Convert database value to String or empty string  if DBNull   
    /// </summary>
    public static char? Char(object value)
    {
        if (value != DBNull.Value)
        {
            return Convert.ToChar(value);
        }
        else
        {
            return null;
        }
    }
}