/*Author: Ben Grieme - 2019
    About this class: This is a simple container class. It represents a single product slot in a vending machine. It holds a VendingItem (the product to be dispensed) and tracks the quantity of that item.
    It is used within the Inventory class, as the heart of Inventory is a 2-D array of slots.*/

public class Slot
{
    public VendingItem item;
    public int quantity;
    public Slot(VendingItem givenItem, int givenQuantity = 0)
    {
        item = givenItem;
        quantity = givenQuantity;
    }
}