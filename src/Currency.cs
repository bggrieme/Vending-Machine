
/*Enum representing values of different US currencies in cents*/
public enum Currency
{
    //note: this.getIndex() is dependant on the order of this enum and should be updated if this enum is appended
    PENNY = 1,
    NICKEL = 5,
    DIME = 10,
    QUARTER = 25,
    DOLLAR = 100
}

static class CurrencyMethods
{
    public static int getIndex(this Currency c)
    {
        /*TODO: may be able to write this in such a way that removes magic numbers
        and thus will not break if the enum is modified.
        Perhaps by iterating through the enum with a loop, counting and comparing as it goes.*/
        switch (c)
        {
            case Currency.PENNY:
                return 0;
            case Currency.NICKEL:
                return 1;
            case Currency.DIME:
                return 2;
            case Currency.QUARTER:
                return 3;
            case Currency.DOLLAR:
                return 4;
        }
    }

    public static string getName(this Currency c)
    {
        return Enum.GetName(typeof(Currency), c.getIndex());
    }
}