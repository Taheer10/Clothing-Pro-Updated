namespace ClothingPro.BusinessLayer.Helper;
public class NullHandler
{
    /// <summary>
    ///     Returns DBNull.Value if a DateTime is null.
    /// </summary>
    public static object DateTime(DateTime? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    public static DateTime DateTime(DateTime? value, DateTime defaultValue)
    {
        if (value == null) { return defaultValue; }
        else { return value.Value; }
    }

    /// <summary>
    ///     Returns DBNull.Value if an Int16 is null.
    /// </summary>
    public static object Int16(short? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    /// <summary>
    ///     Returns DBNull.Value if an Int32 is null.
    /// </summary>
    public static object Int32(int? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    public static int Int32(int? value, int defaltValue)
    {
        if (value == null) { return defaltValue; }
        else { return value.Value; }
    }

    /// <summary>
    ///     Returns DBNull.Value if an Int32 is null.
    /// </summary>
    public static object Int64(long? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    public static long Int64(long? value, long defaltValue)
    {
        if (value == null) { return defaltValue; }
        else { return value.Value; }
    }


    /// <summary>
    ///     Returns DBNull.Value if a nullable Decimal is null.
    /// </summary>
    public static object Decimal(decimal? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    /// <summary>
    /// returns the defaultValue if a nullable Decimal is null.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static decimal Decimal(decimal? value, decimal defaultValue)
    {
        if (value == null) { return defaultValue; }
        else { return value.Value; }
    }

    /// <summary>
    ///     Returns DBNull.Value if a double is null.
    /// </summary>
    public static object Double(double? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value.Value; }
    }

    /// <summary>
    ///     Returns DBNull.Value if a nullable bool is null.
    /// </summary>
    public static object Boolean(bool? value)
    {
        if (value == null) { return DBNull.Value; }
        else { return Convert.ToBoolean(value); }
    }

    /// <summary>
    ///     Returns the default value if a nullable bool is null.
    /// </summary>
    public static bool Boolean(bool? value, bool defaultValue)
    {
        if (value == null) { return defaultValue; }
        else { return Convert.ToBoolean(value); }
    }


    /// <summary>
    ///     Returns DBNull.Value if a nullable String is null.
    /// </summary>
    public static object String(string value)
    {
        if (value == null) { return DBNull.Value; }
        else { return value; }
    }


    /// <summary>
    ///     Returns defaultValue if a nullable String is null.
    /// </summary>
    public static string String(string value, string defaultValue)
    {
        if (value == null) { return defaultValue; }
        else { return value; }
    }



    internal static object String(DateTime? _CLR_DATE)
    {
        throw new NotImplementedException();
    }
}