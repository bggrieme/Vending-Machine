/*Author: Ben Grieme - 2019.
    About this program: I created this VendingMachine as an excuse to practice Object Oriented design principles, Test Driven Development, and version control via git/gitHub.
    This program is, in simplest terms, a model of a common vending machine. 
    For more information on each individual piece, see similar comments in each .cs source file.*/
using System;

namespace VendingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] options = {"Retrieve vended item", "Insert penny", "Insert nickel", "Insert dime", "Insert quarter", "Insert dollar", "Insert two dollars", "Return holdings"};
            KeyPad keyPad = new KeyPad(options);
            Inventory inv_3x5 = new Inventory(3, 5, getInitialItems());
            Till till_300 = new Till(0, 0, 9, 0, 3);
            VendingMachine machine = new VendingMachine(inv_3x5, till_300);            
            ConsoleKey key;
            int x, y;
            x = y = int.MinValue; //minValue reserved as sentinel value

            while (true)
            {
                update(ref machine, ref keyPad);
                key = Console.ReadKey().Key;
                keyPad.moveSelection(key);
                if (key == ConsoleKey.Enter)
                {
                    /*Getting x, y coordinates for product selection*/
                    if (keyPad.cursor >= 0)
                    {
                        if (x != int.MinValue && y == int.MinValue) //if num1 is set and num2 is not set
                        {
                            y = keyPad.cursor;
                        }
                        if (x == int.MinValue)
                        {
                            x = keyPad.cursor;
                            continue;
                        }
                    }

                    /*Attempt vending item if x and y coordinates have been entered*/
                    if (x >= 0 && y >= 0)
                    {
                        machine.vend(x, y);
                        x = y = int.MinValue;
                        continue;
                    }

                    /*An option has been selected*/
                    if (keyPad.cursor < 0)
                    {
                        switch (keyPad.cursor)
                        {
                            case -1: //retrieve vended item
                                machine.getDispensedItem();
                                break;
                            case -2: //insert penny
                                machine.insertCurrency(Currency.PENNY, 1);
                                break;
                            case -3: //insert nickel
                                machine.insertCurrency(Currency.NICKEL, 1);
                                break;
                            case -4: //insert dime
                                machine.insertCurrency(Currency.DIME, 1);
                                break;
                            case -5: //insert quarter
                                machine.insertCurrency(Currency.QUARTER, 1);
                                break;
                            case -6: //insert dollar
                                machine.insertCurrency(Currency.DOLLAR, 1);
                                break;
                            case -7: //insert 2 dollars
                                machine.insertCurrency(Currency.DOLLAR, 2);
                                break;
                            case -8: //return holdings
                                machine.returnHolding();
                                break;
                        }
                    }
                }
            }
        }

        /*Updates the screen. Clears console, prints Inventory UI, then prints KeyPad UI.*/
        private static void update(ref VendingMachine machine, ref KeyPad keyPad)
        {
            Console.Clear();//clear screen
            Console.WriteLine(machine.get_UI_string(15)); //draw current inventory
            keyPad.printDisplay();//draw current keypad
        }

        /*Just didn't want all of these initializations in the Main(). Pretend they're coming from a .csv or something anyway :P*/
        private static Slot[] getInitialItems()
        {
            Slot slot1 = new Slot(new VendingItem("Dr. Pepper", .99m), 10);
            Slot slot2 = new Slot(new VendingItem("Mountain Dew", 1.05m), 10);
            Slot slot3 = new Slot(new VendingItem("Pepsi", 1.75m), 10);
            Slot slot4 = new Slot(new VendingItem("RC Cola", 1m), 10);
            Slot slot5 = new Slot(new VendingItem("CocaCola", 1m), 10);
            Slot slot6 = new Slot(new VendingItem("Monster", 25m), 2);
            Slot slot7 = new Slot(new VendingItem("Snickers", .8m), 15);
            Slot slot8 = new Slot(new VendingItem("MilkyWay", .85m), 15);
            Slot slot9 = new Slot(new VendingItem("KitKat", .75m), 15);
            Slot slot10 = new Slot(new VendingItem("Hershey's Bar", 1m), 15);
            Slot slot11 = new Slot(new VendingItem("AlmondJoy", 1m), 15);
            Slot slot12 = new Slot(new VendingItem("Mounds", 1m), 15);
            Slot slot13 = new Slot(new VendingItem("RingPop", 1m), 10);
            Slot slot14 = new Slot(new VendingItem("FunDip", 1m), 1);
            Slot slot15 = new Slot(new VendingItem("Bag O' Air", 0m), 99999);
            Slot[] items = { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11, slot12, slot13, slot14, slot15 };
            return items;
        }
    }
}

