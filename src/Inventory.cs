using System.Collections.Generic;

public class Inventory
{

    private Slot[,] inv;
    public int width { get; private set; }
    public int height { get; private set; }

    /*Constructor. Inventory dimensions must be provided. Can optionally include an array of Slots to populate the inventory with.
    Populates starting at the top left corner of the inventory, left-to-right top-to-bottom (like how English is written/read).
    Will sequentially pull elements from the given array until the array is empty or the inventory is full.*/
    public Inventory(int width, int height, Slot[] initialSlots = null)
    {
        this.width = width;
        this.height = height;
        this.inv = new Slot[width, height];
        int index = 0;
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                this.inv[x,y] = (initialSlots != null && initialSlots.Length > index) ? initialSlots[index] : new Slot(null, 0);
                index++;
            }
        }
    }

    /*Returns the Slot at the given location. Doesn't modify the inventory.*/
    public Slot peekSlot(int x, int y)
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
        if ((inv[x, y].item == default(VendingItem)))
        {
            this.inv[x, y] = new Slot(item);
            this.setQuantity(quantity, x, y); //easy way of disallowing inserting a new item with a quantity of -x
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool insertNew(Slot newSlot, int x, int y)
    {
        return this.insertNew(newSlot.item, newSlot.quantity, x, y);
    }

    /*Sets the given (x, y) location to an empty Slot*/
    public void clearSlot(int x, int y)
    {
        inv[x, y] = new Slot(null, 0);
    }

    /*Clears all slots in the inventory*/
    public void clearAllSlots()
    {
        this.inv = new Slot[this.width, this.height];
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
    public void setQuantity(int quantity, int x, int y)
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

    /*Produces a text-based graphical representation of the given inventory Slot.
    Really only intended for demo purposes.*/
    public string stringGUI(int cellWidth = 12)
    {
        string s_inv = "";
        for (int i = 0; i <= (cellWidth - 1) * this.width; i++) //top border
        {
            s_inv += "-";
        }
        for (int h = this.height - 1; h >= 0; h--)
        {
            stringGUI_buildRow(ref s_inv, cellWidth, h, "NAME");
            stringGUI_buildRow(ref s_inv, cellWidth, h, "QUANTITY");
            stringGUI_buildRow(ref s_inv, cellWidth, h, "PRICE");
            s_inv += "\n|";
            for (int w = 0; w < this.width; w++) //Slot grid location
            {
                string location = "[" + w + ", " + h + "]";
                location = location.PadLeft((cellWidth - 2 - location.Length) / 2 + location.Length).PadRight(cellWidth - 2); //this beautifully center-aligns the location string within the cell. Formula is basically: (availableSpace - locationStringLength) / 2 + locationStringLength, then simply padRight(availableSpace)
                s_inv += location + "|";
            }
            s_inv += "\n";
            for (int i = 0; i <= (cellWidth - 1) * this.width; i++) //bottom border
            {
                s_inv += "-";
            }
        }
        return s_inv;
    }

    private void stringGUI_buildRow(ref string s_inv, int cellWidth, int h, string dataDesignation)
    {
        string validationString = "NAME QUANTITY PRICE";
        if (!validationString.Contains(dataDesignation))
        {
            throw new System.Exception("Must provide valid dataDesignator value: \"NAME\", \"QUANTITY\", or \"PRICE\" are valid.");
        }
        s_inv += "\n|";
        for (int w = 0; w < this.width; w++) //product names
        {
            string dataString = "";
            if (this.inv[w, h].item == null) { } //prevents any of the 'else if's from occurring if the target Slot is empty.
            else if (dataDesignation == "NAME")
            {
                dataString = this.inv[w, h].item.name;
            }
            else if (dataDesignation == "QUANTITY")
            {
                dataString = "Count: " + this.inv[w, h].quantity;
            }
            else if (dataDesignation == "PRICE")
            {
                dataString = "$" + this.inv[w, h].item.price;
            }
            dataString = (dataString.Length <= cellWidth - 2) ? dataString : dataString.Substring(0, cellWidth - 2); //trim the item name to fit within the cell, if needed
            dataString = dataString.PadRight(cellWidth - 2);
            s_inv += dataString + "|";
        }
    }
}