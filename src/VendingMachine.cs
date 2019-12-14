/*Author: Ben Grieme - 2019
    About this class: This class integrates Inventory and Till systems into a single VendingMachine entity.
    This is a pretty simple and straightforward class. Most of its methods are simply calls to methods of its Till and/or Inventory components.*/
using System.Collections.Generic; //dictionary

namespace VendingProject
{
    public class VendingMachine
    {
        private const string displayMessage_DEFAULT = "Welcome! Please insert currency, then choose a product.";
        private Till till;
        private Inventory inventory;
        private VendingItem dispenserSlot;
        public string displayMessage { get; private set; }

        public VendingMachine(Inventory givenInventory, Till givenTill)
        {
            this.till = givenTill;
            this.inventory = givenInventory;
            this.displayMessage = displayMessage_DEFAULT;
        }

        /*Returns the Slot object held at the given inventory coordinates.*/
        public Slot peekSlot(int x, int y)
        {
            return this.inventory.peekSlot(x, y);
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
            this.till.resetBank();
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

        /*Returns a text-based representation of the vending machine, intended as a basic user interface.
        Please note: in my opinion, there is a lot of dirt and duct tape in this method and its helper methods. 
        It isn't intended to be an example of my best work, it only exists as a means to quickly interact with and observe what I've created.*/
        public string get_UI_string(int cellWidth)
        {
            int cells_wide = inventory.width; //number of cells (vending Slots) in a row of the inventory
            string vendingUI = this.inventory.stringUI(cellWidth) + "\n";
            int rowLength = 60; //determines the width of the box that appears underneath the Inventory UI.
            for (int i = 0; i <= rowLength; i++) //top border
            {
                vendingUI += "*";
            }
            vendingUI += "\n";
            string[] row_left = { "Holdings : " + this.getHoldingsString() }; //the tokens to be printed ont he left side
            string row_right = "~ Dispenser ~"; //token to be printed on the right side
            UI_Row_Builder(ref vendingUI, rowLength, ':', row_left, row_right, "LEFT");
            UI_Row_Builder(ref vendingUI, rowLength, ' ', null); //blank line with border chars in place
            row_left = new string[] { "~ Machine Bank Contents ~" };
            row_right = (dispenserSlot != null) ? dispenserSlot.name : "";
            UI_Row_Builder(ref vendingUI, rowLength, ' ', row_left, row_right);
            row_left = new string[] { "Pennies", "Nickels", "Dimes", "Quarters", "Dollars" };
            UI_Row_Builder(ref vendingUI, rowLength, '|', row_left);
            row_left = new string[] { till.bank[Currency.PENNY].ToString(), till.bank[Currency.NICKEL].ToString(), till.bank[Currency.DIME].ToString(), till.bank[Currency.QUARTER].ToString(), till.bank[Currency.DOLLAR].ToString(), };
            UI_Row_Builder(ref vendingUI, rowLength, '|', row_left);
            for (int i = 0; i <= rowLength; i++) //bottom border
            {
                vendingUI += "*";
            }
            vendingUI += "\n" + this.displayMessage;
            return vendingUI;
        }

        /*Modifies the given UI_string to add a UI row based on the given parameters.
        rowLength: Determines how many characters long a row will be.
        tokenDelimiter: sets the character to be used to separate the members of left_tokens
        left_tokens: an array of strings to be included on the left section of the UI string
        right_token: If given, this string will be included on the right section of the UI string
        leftSideAlignment: must be either LEFT, RIGHT, or CENTER. Determines the justification of the left side tokens.
        rightSideAlignment: must be either LEFT, RIGHT, or CENTER. Determines the justification of the right side token.
        borderChar: Sets the character to be used for the UI border*/
        private void UI_Row_Builder(ref string UI_string, int rowLength, char tokenDelimiter, string[] left_tokens, string right_token = "", string leftSideAlignment = "CENTER", string rightSideAlignment = "CENTER", char borderChar = '*')
        {
            if (left_tokens == null)
            {
                string[] temp = { "" };
                left_tokens = temp;
            }
            if (right_token == null)
            {
                right_token = "";
            }
            /*---Left side---*/
            int space_left = ((rowLength / 3) * 2) - 1; //left side gets 2/3 the total width, -1 due to borderChar taking up a char slot
            UI_string += borderChar;
            int spacesPerToken = space_left / left_tokens.Length; //total space / number of tokens
            int spacesRemainder = space_left % left_tokens.Length;
            for (int i = 0; i < left_tokens.Length; i++) //trim and align each token as necessary, add delimiting char to tokens as necessary
            {
                if (spacesRemainder > 0) //distributes extra spaces among the tokens to ensure aligned columns
                {
                    trim_and_justify(ref left_tokens[i], spacesPerToken + 1, tokenDelimiter, leftSideAlignment, (i < left_tokens.Length - 1));
                    spacesRemainder--;
                    continue;
                }
                trim_and_justify(ref left_tokens[i], spacesPerToken, tokenDelimiter, leftSideAlignment, (i < left_tokens.Length - 1));
            }
            foreach (string s in left_tokens)
            {
                UI_string += s;
            }
            UI_string += borderChar;
            /*---Right side---*/
            int space_right = (rowLength / 3) - 1; //right side gets 1/3 the total width, -1 due to borderChar taking up a char slot
            trim_and_justify(ref right_token, space_right, ' ', rightSideAlignment, false);
            UI_string += right_token + borderChar;
            UI_string += "\n";
        }

        /*Trims the given token if needed, justifies it based on the given justifcation LEFT, RIGHT, or CENTER. Adds a delimiter character to the token if delimit == true*/
        private void trim_and_justify(ref string token, int spacesPerToken, char tokenDelimiter, string justification = "CENTER", bool delimit = false)
        {
            token = (token.Length > spacesPerToken) ? token.Substring(0, spacesPerToken) : token; //trim token, if necessary
            token = (justification == "LEFT") ? token.PadRight(spacesPerToken) : token; //Align left, if that is chosen alignment
            token = (justification == "RIGHT") ? token.PadLeft(spacesPerToken) : token; //Align right, if that is chosen alignment
            token = (justification == "CENTER") ? token.PadLeft((spacesPerToken - token.Length) / 2 + token.Length).PadRight(spacesPerToken) : token; //Align center, if that is chosen alignment
            token = (delimit) ? token.Substring(0, token.Length - 1) + tokenDelimiter : token;
        }
    }
}