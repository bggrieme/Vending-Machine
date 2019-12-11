using System.Collections.Generic; //dictionary

namespace VendingProject
{
    public class VendingMachine
    {
        private const string displayMessage_DEFAULT = "Welcome! Please insert currency, then choose a product.";
        private Till till;
        private Inventory inventory;
        private VendingItem dispenserSlot;
        public string displayMessage {get; private set;}


        public VendingMachine(Inventory givenInventory, Till givenTill)
        {
            this.till = givenTill;
            this.inventory = givenInventory;
            displayMessage = displayMessage_DEFAULT;
        }

        /*Returns the Slot object held at the given inventory coordinates.*/
        public Slot peekSlot(int x, int y)
        {
            return inventory.peekSlot(x, y);
        }

        /*Inserts the given currency in the given quantity into the VendingMachine's Till for spending*/
        public void insertCurrency(Currency c, int quantity)
        {
            this.till.insertMoney(c, quantity);
        }

        /*Returns currency equal to the value of the Till's current holding*/
        public Dictionary<Currency, int> returnHolding()
        {
            return this.till.returnHolding();
        }

        /*Attempts to dispense the item held at [x, y] in the Inventory.
        if the targeted slot is not empty or out of stock, the user has inserted enough money, exact change can be made post-purchase, and the dispenserSlot is ready to receive items,
        then this method will set the dispenserSlot to the targeted item and return true.*/
        public bool vend(int x, int y)
        {
            Slot targetSlot = inventory.peekSlot(x, y);
            if (targetSlot.item == null)
            {
                this.displayMessage = "Error: Given location holds no item to vend.";
                return false;
            }
            if (targetSlot.quantity <= 0)
            {
                this.displayMessage = "Error: Selected item is out of stock.";
                return false;
            }
            if (this.till.holdings < (int)(targetSlot.item.price * 100)) //convert item price from fractional dollars to whole cents in integer form
            {
                this.displayMessage = "Error: Insufficient funds.";
                return false;
            }
            if (!this.till.canMakeChange(this.till.holdings - (int)(targetSlot.item.price * 100)))
            {
                this.displayMessage = "Error: Insufficient funds in bank to make change if item were purchased.";
                return false;
            }
            if (this.dispenserSlot != null)
            {
                this.displayMessage = "Error: Object detected in dispenser slot.";
                return false;
            }
            this.dispenserSlot = this.inventory.dispense(x, y);
            this.till.spend((int)(targetSlot.item.price * 100));
            this.displayMessage = displayMessage_DEFAULT;
            return true;
        }

        /*Returns the item held in dispenserSlot, if any. Sets dispenserSlot to null. */
        public VendingItem getDispensedItem()
        {
            VendingItem temp = dispenserSlot;
            this.dispenserSlot = null;
            return temp;
        }

        /*Returns a string representation of the till's holdings value in the format "$.cc" */
        public string getHoldingsString()
        {
            decimal holdings = till.holdings / 100.00m; //convert from cents to dollars
            return holdings.ToString("C"); //"C" specifier converts the decimal to a currency format. Default culture seems to be en-US, so no need to specify currency symbol.
        }

        /*Resets the till to its initial state (as though a worker came along, emptied the till, and restocked it to a default setting)*/
        public void resetTill()
        {
            till.resetBank();
        }

        /*Sets the quantity of product at the given slot location.*/
        public void setQuantity(int quantity, int x, int y)
        {
            this.inventory.setQuantity(quantity, x, y);
        }

        /*Clears the given [x,y] slot of product, if any. Sets the slot to the new product at the given quantity*/
        public void setNewProduct(VendingItem newProduct, int quantity, int x, int y)
        {
            this.inventory.insertNew(newProduct, quantity, x, y);
        }

        /*Clears the contents of the given slot location.*/
        public void setSlotToEmpty(int x, int y)
        {
            this.inventory.clearSlot(x, y);
        }

        public string inventoryUI(int cellWidth) //TODO consider refactoring to something more meaningful.. perhaps something like get_UI_string() ?
        {
            string vendingUI = inventory.stringUI(cellWidth);
            vendingUI += "\nCurrent Holdings: " + this.getHoldingsString() + "\nDispenser contents: ";
            if (this.dispenserSlot != null)
            {
                vendingUI+= this.dispenserSlot.name;
            } 
            vendingUI += "\n" + this.displayMessage;
            return vendingUI;
        }
    }
}