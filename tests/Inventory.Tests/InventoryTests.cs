using Xunit;
using CustomCollection;

public class InventoryTests
{
    string testItem1 = "testItem1";
    string testItem2 = "testItem2";
    string tempItemStorage;
    Inventory<string> inventory = new Inventory<string>(3, 3); //an inventory collection for Item type

    /*Peeks at an empty slot. Should get default() value for the type*/
    [Fact]
    public void peekEmpty()
    {
       
    }

    /*Attempts to insertAt into an empty slot. Should successfully do so.*/
    [Fact]
    public void insertAtEmpty()
    {
        Assert.True(inventory.insertAt(testItem1, 0, 0), "Failed to insert item at (0, 0).");
    }

    /*Attempts to insertAt into an occupied slot. This should not be successful.*/
    [Fact]
    public void insertAtOccupied()
    {
        Assert.False(inventory.insertAt(testItem2, 0, 0), "Inserted item at (0, 0), but should not have been able to.");
    }

    /*Removes the previously added testItem1 and compares it to testItem2. They should not be equal.*/
    [Fact]
    public void removeAtOccupied()
    {
        tempItemStorage = inventory.removeAt(0, 0);
        Assert.True(tempItemStorage.Equals(testItem1), "The item removed compared NOT equal to testItem1 when it should have.");
        Assert.False(tempItemStorage.Equals(testItem2), "The item removed compard equal to testItem2 when it should not have.");
    }

    /*Attempts to remove from an empty slot.*/
    [Fact]
    public void removeAtUnOccupied()
    {
        Assert.True(inventory.removeAt(0, 0).Equals(default(string)), "Attempted to remove an empty slot and compare the result with default(Type) but comparison failed.");
    }


}

