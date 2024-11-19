namespace ConsoleApp1;

using System;

public static class DateTimeExt
{
    const string DateFormat = "dd-MM-yyyy HH:mm:ss:ffff";

    public static string GetDate(this DateTime date)
    {
        return date.ToString(DateFormat);
    }


}

