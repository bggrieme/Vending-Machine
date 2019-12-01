using System.Collections.Generic; //dictionary

namespace VendingProject
{
    public class VendingMachine
    {
        private string displayMessage, displayHoldings;
        private Till till;
        private Inventory inventory;
        public VendingItem dispenserSlot;

        public VendingMachine(Inventory givenInventory, Till givenTill)
        {
            this.till = givenTill;
            this.inventory = givenInventory;
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

        /*attempts to dispense the item held at [x, y] in the Inventory.
        If till holdings >= item value, and
        if change can be made after the chosen item is vended, and
        if the dispenserSlot is empty,
        the selected item will be dispensed to the dispenserSlot and this method will return true*/
        public bool vend(int x, int y)
        {
            return false;
            //TODO after tests are written. DONE. okay to Implement.
        }

        /*Returns the item held in dispenserSlot, if any. Sets dispenserSlot to null. */
        public VendingItem getDispensedItem()
        {
            return dispenserSlot;
            //TODO after tests are written. DONE. okay to Implement.
        }

        /*Returns a string representation of the till's holdings value in the format "$.cc" */
        public string getHoldingsString()
        {
            return "TODO";
            //TODO after tests are written. DONE. okay to implement
        }

        /*Resets the till to its initial state (as though a worker came along, emptied the till, and restocked it to a default setting)*/
        public void resetTill()
        {
            till.resetBank();
        }

        /*Sets the quantity of product at the given slot location.*/
        public void setQuantity(int quantity, int x, int y)
        {
            //TODO after tests
        }

        /*Clears the given [x,y] slot of product, if any. Sets the slot to the new product at the given quantity*/
        public void setNewProduct(VendingItem newProduct, int quantity, int x, int y)
        {
            //TODO after tests
        }

        /*Clears the contents of the given slot location.*/
        public void setSlotToEmpty(int x, int y)
        {
            this.inventory.clearSlot(x, y);
        }

        //TODO: what does the vending machine have to do?
        // accept currency
        // return currency
        // given keypad input, attempt to vend the related item
        //check if holdings >= item value
        //check if change can be made if the chosen item is vended
        //check if dispenserSlot is empty
        //communicate the results of these checks via displayMessage
        //if successful, set dispenserSlot (vend)
        // get current holdings as string in "$.cc" format
        // clear dispenserSlot
        // reset till
        // adjust slot quantity
        // change inventory slot


    }
}