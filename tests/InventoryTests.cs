using Xunit;
using System.Collections.Generic; //EqualityComparer


public class InventoryTests
{
    
    VendingItem item1 = new VendingItem("Soda", 1.00m);
    VendingItem item2 = new VendingItem("Candy", .75m);
    Inventory inventory = new Inventory(3, 3);

    [Fact (DisplayName = "Peeking on empty slot should return default VendingItem")]
    public void peek_empty_slot_get_default_VendingItem()
    {
        Assert.True(inventory.peekSlot(0,0).item == default(VendingItem));
    }

    [Fact (DisplayName = "Peeking slot after adding an item should return the item added")]
    public void peek_filled_slot_get_VendingItem()
    {
        inventory.insertNew(item1, 5, 0, 0);
        Assert.True(inventory.peekSlot(0,0).item.Equals(item1));
    }

    [Fact (DisplayName = "Add item. clearSlot(). Slot should now be empty.")]
    public void addItem_then_clearSlot_shouldBeEmpty()
    {
        inventory.insertNew(item1, 5, 0, 0);
        inventory.clearSlot(0,0);
        Assert.True(inventory.peekSlot(0,0).item == default(VendingItem));        
    }

    [Fact (DisplayName = "Try add new item to occupied slot. Slot should be unchanged.")]
    public void addItem_toOccupied_shouldNotChange()
    {
        inventory.insertNew(item1, 4, 0, 0);
        inventory.insertNew(item2, 3, 0, 0);
        Assert.False(inventory.peekSlot(0,0).item.Equals(item2));
    }

    [Theory (DisplayName = "Adjust quantity of item in a slot.")]
    [InlineData (5)]
    [InlineData (0)]
    [InlineData (int.MaxValue)]
    public void adjust_quantity_of_occupied_slot(int quantity)
    {
        inventory.insertNew(item1, 999, 0, 0);
        inventory.setQuantity(quantity, 0, 0);
        Assert.True(inventory.peekSlot(0,0).quantity == quantity);
    }

    [Theory (DisplayName = "Adjust quantity to <= 0. New quantity should == 0.")]
    [InlineData (0)]
    [InlineData (-1)]
    [InlineData (int.MinValue)]
    public void adjust_quantity_to_subZero_shouldGet_zero(int quantity)
    {
        inventory.insertNew(item2, 999, 0, 0);
        inventory.setQuantity(quantity, 0, 0);
        Assert.True(inventory.peekSlot(0,0).quantity == 0);
    }

    [Fact (DisplayName = "Dispense occupied slot. Should return VendingItem and decrement slot's quantity.")]
    public void dispense_occupiedSlot()
    {
        int quantity = 5;
        inventory.insertNew(item1, quantity, 0, 0);
        Assert.True(inventory.dispense(0,0).Equals(item1) && inventory.peekSlot(0,0).quantity == quantity-1);
    }

    [Theory (DisplayName = "Dispensing slot with quantity <= 0 should throw Exception.")]
    [InlineData (0)]
    [InlineData (-5)]
    public void dispenseZeroQuantityOrLess_throws_exception(int quantity)
    {
        bool ex_Thrown = false;
        VendingItem item = new VendingItem("test", 0m);
        inventory.insertNew(item, quantity, 0, 0);
        try{
            inventory.dispense(0,0);
        }catch(System.Exception){
            ex_Thrown = true;
        }
        Assert.True(ex_Thrown, "Exception not thrown when it should have been.");
    }

    [Fact (DisplayName = "Dispensing empty slot should throw Exception.")]
    public void dispenseEmpty_throws_exception()
    {
        bool ex_Thrown = false;
        try{
            inventory.dispense(0,0);
        }catch(System.Exception){
            ex_Thrown = true;
        }
        Assert.True(ex_Thrown, "Exception not thrown when it should have been.");        
    }

    [Fact (DisplayName = "Adjust stock of empty slot. Should throw exception.")]
    public void adjustStock_of_emptySlot_get_exception()
    {
        bool ex_Thrown = false;
        try{
            inventory.setQuantity(5, 0,0);
        }catch(System.Exception){
            ex_Thrown = true;
        }
        Assert.True(ex_Thrown, "Exception not thrown when it should have been.");
    }
    

}

