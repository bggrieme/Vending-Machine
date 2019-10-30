using System.Collections.Generic; //dictionary

public class Till
{
    public Dictionary<Currency, int> bank { get; private set; }
    public Dictionary<Currency, int> defaultBank { get; private set; } //the state of the bank when constructed
    public decimal holdings { get; private set; } //stores the balance of currency added but not yet spent


    public Till(int startingPENNIES, int startingNICKELS, int startingDIMES, int startingQUARTERS, int startingDOLLARS)
    {
        this.defaultBank.Add(Currency.PENNY, startingPENNIES);
        this.defaultBank.Add(Currency.NICKEL, startingNICKELS);
        this.defaultBank.Add(Currency.DIME, startingDIMES);
        this.defaultBank.Add(Currency.QUARTER, startingQUARTERS);
        this.defaultBank.Add(Currency.DOLLAR, startingDOLLARS);
        this.bank = this.defaultBank;
    }

    /*Resets the bank to the originally constructed defaultBank, zeroes holdings*/
    public void resetBank()
    {
        this.bank = this.defaultBank;
        holdings = 0m;
    }

    /*Checks to see if the till can make change for the given currency. 
    If it can, adds the currency to the bank, increases holdings, and returns true.
        recursively repeats this until it either returns false or quantity <= 0
    If it can not, returns false.*/
    public bool insertMoney(Currency c, int quantity = 1)
    {

        if (!canMakeChange(c) || quantity <= 0)
        {
            return false;
        }
        else
        {
            
            return true;
        }
    }

    /*Returns currency equivalent to the holdings value using the least change possible*/
    public Dictionary<Currency, int> returnHolding()
    {
        Dictionary<Currency, int> refund = new Dictionary<Currency, int>();
        return refund;
    }

    /*Deducts the value from the holdings bank*/
    public void spend(decimal value)
    {

    }

    /*Checks to make sure the system can make change for any currency given*/
    private bool canMakeChange(Currency c)
    {
        return true;
    }
}