using System; //Enum.
using System.Collections.Generic; //dictionary

public class Till
{
    private Currency[] arr_Currency = (Currency[])Enum.GetValues(typeof(Currency));
    private Dictionary<Currency, int> changeBank; //a reference bank that stores the number of each currency needed to make proper change
    public Dictionary<Currency, int> bank { get; private set; } //stores the number of each Currency held in the Till
    public Dictionary<Currency, int> defaultBank { get; private set; } //the state of the bank when constructed
    public int holdings { get; private set; } //stores the balance of currency added but not yet spent, in cents

    public Till(int startingPENNIES, int startingNICKELS, int startingDIMES, int startingQUARTERS, int startingDOLLARS)
    {
        int[] startingCounts = new int[] { startingPENNIES, startingNICKELS, startingDIMES, startingQUARTERS, startingDOLLARS };
        this.defaultBank = new Dictionary<Currency, int>();
        this.bank = new Dictionary<Currency, int>();
        this.changeBank = new Dictionary<Currency, int>();
        for (int i = 0; i < arr_Currency.Length; i++)
        {
            this.defaultBank.Add(arr_Currency[i], startingCounts[i]);
            this.bank.Add(arr_Currency[i], startingCounts[i]);
            this.changeBank.Add(arr_Currency[i], 0);
        }
    }

    /*Resets the bank to the originally constructed defaultBank, zeroes holdings*/
    public void resetBank()
    {
        this.bank = this.defaultBank;
        holdings = 0;
    }

    /*Adds the given currency to till's bank. Increases the till's holdings value appropriately.*/
    public void insertMoney(Currency c, int quantity = 1)
    {
        this.bank[c] += quantity;
        this.holdings += (int)c*quantity;
    }

    /*Refunds currency equivalent to the holdings value using the least change possible.*/
    public Dictionary<Currency, int> returnHolding()
    {
        canMakeChange(this.holdings); //updates changeBank
        Dictionary<Currency, int> refund = new Dictionary<Currency, int>();
        foreach(KeyValuePair<Currency, int> kvp in this.changeBank) //copy this.changeBank values into the new refund Dictionary
        {
            refund.Add(kvp.Key, kvp.Value);
        }
        foreach (Currency key in this.arr_Currency) //withdraw all currencies held within this.changeBank from this.bank
        {
            this.bank[key] -= this.changeBank[key];
        }
        this.holdings = 0;
        return refund;
    }

    /*If holdings is less than the argument, returns false. Otherwise deducts the argument from holdings and returns true.*/
    public bool spend(int val_in_cents)
    {
        if (this.holdings < val_in_cents)
        {
            return false;
        }
        else
        {
            this.holdings -= val_in_cents;
            return true;
        }
    }

    /*Returns true if exact change can be made for the given value. Returns false otherwise.*/
    public bool canMakeChange(int val_in_cents)
    {
        int value = val_in_cents;
        int[] arr_currencyCounts = new int[this.bank.Count];
        for (int i = this.arr_Currency.Length - 1; i >= 0; i--) //iterate through an array containing each Currency from back to front.
        {
            calculateChange(ref value, this.arr_Currency[i], ref arr_currencyCounts[i]);
            this.changeBank[this.arr_Currency[i]] = arr_currencyCounts[i]; //update changeBank for use in returnHolding()
        }
        return value == 0;
    }

    /*Helper method. Calculates how much of the given currency is required to make change for the given value (not exceeding what the bank actually has).*/
    private void calculateChange(ref int value, Currency c, ref int num_Currency_needed)
    {
        /*while the value is greater than the value of the currency &&
        the number of the currency needed is less than what the bank has,
        subtract the value of the currency from the value to be returned and increase the count of the currency needed*/
        while (value >= (int)c && num_Currency_needed < bank[c])
        {
            value -= (int)c;
            num_Currency_needed++;
        }
    }

    /*Helper method. Zeroes all Values in the given Dictionary's key-value pairs. Doesn't change Keys*/
    private void zero_bank(ref Dictionary<Currency, int> dict)
    {
        foreach (Currency key in this.arr_Currency)
        {
            dict[key] = 0;
        }
    }
}