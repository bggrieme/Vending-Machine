using System;

/*Enum representing values of different US currencies in cents*/
public enum Currency
{
    PENNY = 1,
    NICKEL = 5,
    DIME = 10,
    QUARTER = 25,
    DOLLAR = 100
}

static class CurrencyMethods
{
    /*Returns the 0-based index position of the enum if it exists. Returns -1 if it does not exist. */
    public static int getIndex(this Currency c)
    {
        int[] vals = (int[])Enum.GetValues(typeof(Currency));
        for (int i = 0; i < vals.Length; i++)
        {
            if ((int)c == vals[i])
            {
                return i;
            }
        }
        return -1;
    }

    /*Returns the variable name of the enum */
    public static string getName(this Currency c)
    {
        return Enum.GetName(typeof(Currency), c);
    }
}