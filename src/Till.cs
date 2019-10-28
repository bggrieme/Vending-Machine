using System.Collections.Generic; //dictionary

public class Till
{
    public Dictionary<Currency, int> bank {get; private set;}
    public Dictionary<Currency, int> defaultBank {get; private set;}
    public decimal holdings{get; private set;} //stores the balance of currency added but not yet spent

    
    public Till(Dictionary<Currency, int> defaultBank)
    {
        
    }

    /*Resets the bank to the originally constructed defaultBank*/
    public void resetBank()
    {

    }

    /*Inserts currency into the system, adding it to the bank and counting its value into a temporary holding*/
    public void insertMoney(Currency c)
    {

    }

    /*Empties the temporary holdings bank, returning each piece of currency*/
    public Currency returnHolding()
    {
        return Currency.PENNY;
    }

    /*Deducts the value of the given item from the holdings bank*/
    public void transaction(VendingItem item)
    {

    }

    /*Checks to make sure the system can make change for any currency given*/
    private bool canMakeChange()
    {
        return true;
    }

    /*Returns the last currency added*/
    private Currency rejectInserted()
    {
        return Currency.PENNY;
    }
}