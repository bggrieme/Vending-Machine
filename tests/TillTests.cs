using System.Collections.Generic;
using Xunit;

public class TillTests
{
    Till till = new Till(0,0,0,0,0);

    [Fact (DisplayName = "Inserting $1 should set holdings to 100")]
    public void insertDollar_holdings_should_equal_100()
    {
        till.insertMoney(Currency.DOLLAR);
        Assert.True(till.holdings == 100, "Holdings should == 100. Actual value: " + till.holdings);
    }

    [Fact (DisplayName = "Till.bank dollar balance should be 1")]
    public void bank_balance_check()
    {
        till.insertMoney(Currency.DOLLAR);
        Assert.True(till.bank[Currency.DOLLAR] == 1, "till.bank should hold 1 dollar. Actual dollar count: " + till.bank[Currency.DOLLAR]);
    }

    [Fact (DisplayName = "returnHolding dictionary should contain 1 dollar")]
    public void return_dollar()
    {
        till.insertMoney(Currency.DOLLAR);
        Assert.True(till.returnHolding()[Currency.DOLLAR] == 1);
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
        till.spend(75);
        Assert.True(till.holdings == 25);
    }

    [Fact (DisplayName = "Attempting to spend more than holdings should leave bank and holdings unchanged.")]
    public void attempt_spend_more_than_holdings()
    {
        till = new Till(0,0,0,0,0);
        till.insertMoney(Currency.QUARTER);
        till.spend(50);
        Assert.True(till.bank[Currency.QUARTER] == 1 && till.holdings == 25);
    }

    /*attempts to add one of each type of Currency (minus PENNY, because the till can always "make change" for an inserted penny) to till, which should fail due to till.bank being completely empty.*/
    [Theory (DisplayName = "Should be unable to make change because the bank is empty.")]
    [InlineData (Currency.PENNY)]
    [InlineData (Currency.NICKEL)]
    [InlineData (Currency.DIME)]
    [InlineData (Currency.QUARTER)]
    [InlineData (Currency.DOLLAR)]
    public void Should_be_unable_to_make_change_because_empty_bank(Currency c)
    {
        till = new Till(0,0,0,0,0); //new till with completely empty bank
        Assert.False(till.canMakeChange((int)c));
    }

    /*Checks if exact change can be made for several given values - this block should be able to make change*/
    [Theory (DisplayName = "Should be able to make exact change")]
    [InlineData (10, 5, 1)] //10cents from: 5 pennies, 1 nickel
    [InlineData (15, 0, 1, 1)] //15cents from: 1 nickel, 1 dime
    [InlineData (33, 3, 1, 0, 1)] //33cents from: 3 pennies, 1 nickel, 1 quarter
    [InlineData (75, 25, 0, 0, 2)] //75cents from: 25 pennies, 2 quarters
    [InlineData (399, 9, 16, 1, 0, 3)] //399cents from: 9 pennies, 16 nickels, 1 dime, 3 dollars
    [InlineData(501, 1, 1, 1, 1, 5)] //501cents from: 1 penny, 5 dollars
    [InlineData(1, 1)]//1cent from: 1 penny
    public void canMakeChange_should_return_true(int value, int pennies = 0, int nickels = 0, int dimes = 0, int quarters = 0, int dollars = 0)
    {
        till = new Till(pennies, nickels, dimes, quarters, dollars);
        Assert.True(till.canMakeChange(value));
    }

    /*Checks if exact change can be made for several given values - this block should NOT be able to make change*/
    [Theory (DisplayName = "Should NOT be able to make exact change")]
    [InlineData (10, 4, 1)]
    [InlineData (10, 9, 0, 0, 1)]
    [InlineData (30, 0, 0, 1, 1)]
    [InlineData (73, 2, 100, 100, 100, 100)]
    [InlineData (101, 0, 0, 0, 1, 1)]
    [InlineData (0, 1, 1, 1, 1, 1)]
    [InlineData (-25, 9, 78, 65, 1, 15)]
    [InlineData (int.MaxValue, 789, 456, 1231, 158, 48)]
    [InlineData (int.MinValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue)]
    public void canMakeChange_should_return_false(int value, int pennies = 0, int nickels = 0, int dimes = 0, int quarters = 0, int dollars = 0)
    {
        till = new Till(pennies, nickels, dimes, quarters, dollars);
        Assert.False(till.canMakeChange(value));
    }
}