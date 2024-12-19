
using System;
using System.Globalization;
public static class DbUtils
{
    public static string DBShortDatetoString(string indate)
    {
        IFormatProvider MyDateTimeFormat = new CultureInfo("el-GR");
        string shortdate = null;

        try
        {
            DateTime dt = DateTime.Parse(indate, MyDateTimeFormat);
            shortdate = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
        }
        catch { }

        return shortdate;
    }


    public static short SHORTdr(object o)
    {
        if (o != DBNull.Value)
        {
            return short.Parse(o.ToString());
        }

        return 0;

    }

    public static int INTdr(object o)
    {
        if (o != DBNull.Value)
        {
            return int.Parse(o.ToString());
        }

        return 0;

    }

    public static int INTdrwithNull(object o)
    {
        if (o != DBNull.Value)
        {
            return int.Parse(o.ToString());
        }

        return -1;

    }


    public static long LONGdr(object o)
    {
        if (o != DBNull.Value)
        {
            return long.Parse(o.ToString());
        }

        return 0;

    }
    public static string STRINGdr(object o)
    {
        if (o != DBNull.Value)
        {
            return o.ToString();
        }

        return null;

    }

    public static decimal DECIMALdr(object o)
    {
        if (o != DBNull.Value)
        {
            return decimal.Parse(o.ToString());
        }

        return 0;

    }

    public static DateTime DATEdr(object o)
    {
        if (o != DBNull.Value)
        {
            return DateTime.Parse(o.ToString());
        }

        return DateTime.MinValue;

    }
}