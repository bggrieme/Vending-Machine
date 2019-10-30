using System.Collections.Generic;
using Xunit;

public class TillTests
{
    Till till = new Till(200,150,100,50,0);

    [Fact (DisplayName = "Inserting $1 should set holdings to 1.00")]
    public void insertDollar_holdings_should_equal_1_00()
    {
        till.insertMoney(Currency.DOLLAR);
        Assert.True(till.holdings == (decimal)1.00);
    }

    [Fact (DisplayName = "Till.bank dollar balance should be 1")]
    public void bank_balance_check()
    {
        Assert.True(till.bank.GetValueOrDefault(Currency.DOLLAR) == 1);
    }

    [Fact (DisplayName = "returnHolding should produce 1 dollar")]
    public void return_dollar()
    {
        Assert.True(till.returnHolding().GetValueOrDefault(Currency.DOLLAR) == 1);
    }

    [Fact (DisplayName = "Should reset bank to argument given in constructor")]
    public void reset_bank()
    {
        till.insertMoney(Currency.DOLLAR, 5); //adds money to ensure a non-default state of the bank
        till.resetBank();
        Assert.True(till.bank.Values == till.defaultBank.Values);
    }

    [Fact (DisplayName = "till.holdings should == 0 after reset")]
    public void reset_holdings()
    {
        till.insertMoney(Currency.DIME, 7);
        till.resetBank();
        Assert.True(till.holdings == 0m);
    }

    [Fact (DisplayName = "Add dollar. spend 75c. holdings should equal 0.25")]
    public void spend_75c_from_oneDollar_return_25c()
    {
        till.insertMoney(Currency.DOLLAR);
        till.spend(0.75m);
        Assert.True(till.holdings == 0.25m);
    }

    /*attempts to add one of each type of Currency (minus PENNY, because the till can always "make change" for an inserted penny) to till, which should fail due to till.bank being completely empty.*/
    [Theory (DisplayName = "Should reject inserted money due to inability to make change for it.")]
    [InlineData (Currency.NICKEL)]
    [InlineData (Currency.DIME)]
    [InlineData (Currency.QUARTER)]
    [InlineData (Currency.DOLLAR)]
    public void reject_money_because_no_change_emptyBank(Currency c)
    {
        till = new Till(0,0,0,0,0); //new till with completely empty bank
        till.insertMoney(c);
        Assert.True(till.bank.GetValueOrDefault(c) == 0);
    }

    /*Attempts to add currency with a populated bank that should be able to make change*/
    [Theory (DisplayName = "Added currency to bank should be accepted")]
    [InlineData(Currency.PENNY)] //should always be accepted
    [InlineData(Currency.NICKEL, 5)]
    [InlineData(Currency.DIME, 5, 1)]
    [InlineData(Currency.QUARTER, 10, 1, 1)]
    [InlineData(Currency.DOLLAR, 25, 1, 2, 2)]
    public void should_accept_currency_because_can_make_change(Currency c, int pennies = 0, int nickels = 0, int dimes = 0, int quarters = 0, int dollars = 0)
    {
        till = new Till(pennies, nickels, dimes, quarters, dollars);
        int startingCount = till.bank.GetValueOrDefault(c);
        till.insertMoney(c);
        Assert.True(till.bank.GetValueOrDefault(c) == startingCount+1);
    }

    /*Attempts to add a dollar when bank value is > 1.00 BUT current bank is unable to return 1.00 exactly - should reject dollar*/
    [Fact(DisplayName = "Reject Dollar because can't make exact change despite > 1.00 value in bank")]
    public void reject_input_cause_no_exact_change()
    {
        till = new Till(0,0,9,1,0); //total value = 1.15, but 9 dimes and 1 quarter cannot break a dollar
        Assert.False(till.insertMoney(Currency.DOLLAR));
    }

    



}