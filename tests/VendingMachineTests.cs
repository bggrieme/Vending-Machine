using Xunit;
using VendingProject;

public class VendingMachineTests
{
    static Slot item1 = new Slot(new VendingItem("item1", .01m), 2);
    static Slot item2 = new Slot(new VendingItem("item2", .05m), 2);
    static Slot item3 = new Slot(new VendingItem("item3", .75m), 2);
    static Slot item4 = new Slot(new VendingItem("item4", 1m), 2);
    static Slot[] items = {item1, item2, item3, item4};
    static Inventory inv_2x2 = new Inventory(2, 2, items);
    static Till till_300 = new Till(300, 60, 30, 12, 3);
    VendingMachine machine = new VendingMachine(inv_2x2, till_300);


    [Theory (DisplayName = "Inserting currency should change the Holding String")]
    [InlineData ("$0.00")]
    [InlineData ("$1.00", 50, 3, 1, 1)]
    [InlineData ("$1.99", 4, 0, 2, 3, 1)]
    [InlineData ("$1000.00", 100, 0, 0, 0, 999)]
    public void insertCurrency_should_change_holdingsValue(string expectedVal, int pennies=0, int nickels=0, int dimes=0, int quarters=0, int dollars=0)
    {
        machine.insertCurrency(Currency.PENNY, pennies);
        machine.insertCurrency(Currency.NICKEL, nickels);
        machine.insertCurrency(Currency.DIME, dimes);
        machine.insertCurrency(Currency.QUARTER, quarters);
        machine.insertCurrency(Currency.DOLLAR, dollars);
        Assert.True(machine.getHoldingsString().Equals(expectedVal), "Expected: \"" + expectedVal + "\", got: \"" + machine.getHoldingsString() + "\"");
    }

    [Fact (DisplayName = "Attempt to vend two items with all checks met.")]
    public void vendItem_getDispensedItem_vendAnotherItem_allChecksMet()
    {
        machine.insertCurrency(Currency.PENNY, 2);
        //first vending
        Assert.True(machine.vend(0, 1), "Vending first item failed.");
        Assert.True(machine.getDispensedItem().Equals(inv_2x2.peekItem(0, 1)), "Dispensed item did not equal contents at Inventory[0, 1]");
        Assert.True(machine.getDispensedItem() == null, "dispenserSlot was not nulled after getDispensedItem() called.");
        Assert.True(machine.getHoldingsString() == "$0.01", "Holding expected to be \"$0.01\", instead got " + machine.getHoldingsString());
        //second vending
        Assert.True(machine.vend(0, 1), "Vending second item failed."); //slot [0,1] contains two of item1 which cost 1cent each
        Assert.True(machine.getDispensedItem().Equals(inv_2x2.peekItem(0, 1)), "Dispensed item did not equal contents at Inventory[0, 1]");
        Assert.True(machine.getDispensedItem() == null, "dispenserSlot was not nulled after getDispensedItem() called.");        
        Assert.True(machine.getHoldingsString() == "$0.00", "Holding expected to be \"$0.00\", instead got " + machine.getHoldingsString());
    }

    [Fact (DisplayName = "Attempt to vend item without adequate holdings. Should not vend! Then add holdings, should vend.")]
    public void attemptToVend_inadequateHoldings_shouldNotVend_addHoldings_shouldNowVend()
    {
        machine.insertCurrency(Currency.DIME, 9); //90 cents in holdings
        Assert.False(machine.vend(1,0), "Item should not have vended, but it did.");
        machine.insertCurrency(Currency.DIME, 1); //100 cents in holdings
        Assert.True(machine.vend(1,0), "Item should have vended, but it did not.");
    }

    [Fact (DisplayName = "Attempt to vend when exact change cannot be made. Should not vend! Add currency so change can be made, should now vend.")]
    public void attemptToVend_changeCannotBeMade_vendFails_thenAddProperChange_vendSucceeds()
    {
        Till till = new Till(0, 0, 0, 2, 1); //two quarters, one dollar.
        machine = new VendingMachine(inv_2x2, till); //reconstruct vendingmachine with new till
        machine.insertCurrency(Currency.DOLLAR, 1); //insert $1
        Assert.False(machine.vend(0,0), "Vending should have failed due to inability to return exact change, but vend() returned true anyway.");
        machine.insertCurrency(Currency.QUARTER, 1); //machine now holds 3 quarters and two dollars. Exact change can be made
        Assert.True(machine.vend(0,0), "Item should have been able to vend, but it failed to do so.");
    }

}