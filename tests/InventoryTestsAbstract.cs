using Xunit;
using CustomCollection;
using System.Collections.Generic; //EqualityComparer

/*Abstract so the testing framework does not attempt to run these tests.
TODO: I know it is possible to build an abstract test class 
and then instantiate several instances of the class using different data types,
with each instance running all tests using that data type. I have not yet figured out how to do that, but I will.
For now this experiment will take a back seat to the other simpler and more familiar aspects of this project.*/
public abstract class InventoryFacts<T>
{
    T item1, item2;
    T tempItemStorage;
    Inventory<T> inventory; //3x3 inventory collection to be tested
    
    public InventoryFacts(T testItem1, T testItem2){
        item1 = testItem1;
        item2 = testItem2;
        inventory = new Inventory<T>(3, 3);
    }

    /*Peeks at an empty slot. Should get default() value for the type*/
    [Fact]
    public void peekEmpty_ShouldEqualDefaultValueForType()
    {
       Assert.True(EqualityComparer<T>.Default.Equals(inventory.peekSlot(0,0), default(T)), "Empty slot was not equal to default type value.");
    }

    /*Attempts to insertAt into an empty slot. Should successfully do so.*/
    [Fact]
    public void insertAtEmpty_ReturnsTrue()
    {
        Assert.True(inventory.insertAt(item1, 0, 0), "Failed to insert item at (0, 0).");
    }

    /*Attempts to insertAt into an occupied slot. This should not be successful.*/
    [Fact]
    public void insertAtOccupied_ReturnsFalse()
    {
        Assert.False(inventory.insertAt(item2, 0, 0), "Inserted item at (0, 0), but should not have been able to.");
    }

    /*Removes the previously added testItem1 and compares it to testItem2. They should not be equal.*/
    [Fact]
    public void removeAtOccupied_AndVerifySlotEmpty()
    {
        tempItemStorage = inventory.removeAt(0, 0);
        Assert.True(EqualityComparer<T>.Default.Equals(inventory.peekSlot(0,0), default(T)), "After removal, slot should equal default value for the type.");
    }

    /*Attempts to remove from an empty slot.*/
    [Fact]
    public void removeAtEmpty()
    {
        Assert.True(inventory.removeAt(0, 0).Equals(default(T)), "Attempted to remove an empty slot and compare the result with default(Type) but comparison failed.");
    }


}

