using System.Collections.Generic;

public class Inventory
{
    public struct slot
    {
        public VendingItem item;
        public int quantity;
        public slot(VendingItem givenItem, int givenQuantity = 0)
        {
            item = givenItem;
            quantity = givenQuantity;
        }
    }
    private slot[,] inv;


    public Inventory(int width, int height)
    {
        this.inv = new slot[width, height];
    }

    /*Returns the slot struct at the given location. Doesn't modify the inventory.*/
    public slot peekSlot(int x, int y)
    {
        return inv[x, y];
    }

    /*Returns the item held at the given location.*/
    public VendingItem peekItem(int x, int y)
    {
        return inv[x, y].item;
    }

    /*Returns the quantity of whatever is held at the given location.*/
    public int peekQuantity(int x, int y)
    {
        return inv[x, y].quantity;
    }

    /*If given location is unoccupied, sets the given (x, y) location to hold the given quantity of the given item
    If a quantity < 0 is given, the quantity will be adjusted to 0
    Returns true if successfully inserted, false otherwise.*/
    public bool insertNew(VendingItem item, int quantity, int x, int y)
    {
        if ((inv[x,y].item == default(VendingItem)))
        {
            this.inv[x, y] = new slot(item);
            this.adjustStock(quantity, x, y); //easy way of disallowing inserting a new item with a quantity of -x
            return true;
        }
        else
        {
            return false;
        }
    }

    /*Sets the given (x, y) location to an empty slot*/
    public void clearSlot(int x, int y)
    {
        inv[x, y] = default(slot);
    }

    /*Returns a single item from the given [x][y]. 
    Throws an error if there is no item to dispense at the given location.*/
    public VendingItem dispense(int x, int y)
    {
        if (inv[x, y].item == default(VendingItem) || inv[x, y].quantity <= 0)
        {
            throw new System.Exception("System shows no product available to dispense.");
        }
        inv[x, y].quantity--;
        return inv[x, y].item;
    }

    /*Sets the quantity of the item held at (x, y) to the given quantity. If the given quantity is less than zero, will default to zero.*/
    public void adjustStock(int quantity, int x, int y)
    {
        if (inv[x, y].item == default(VendingItem))
        {
            throw new System.Exception("System shows no product at the location. Add a new product first.");
        }
        if (quantity < 0)
        {
            quantity = 0;
        }
        inv[x, y].quantity = quantity;

    }
}