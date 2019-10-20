using System.Collections.Generic; //EqualityComparer

namespace CustomCollection
{
    /*A basic grid-based collection*/
    public class Inventory<T>
    {
        public T[][] inv { get; private set; } //an array of Type arrays
        public Inventory(int width, int height)
        {
            inv = new T[width][];
            for (int i = 0; i < width; i++)
            {
                inv[i] = new T[height];
            }
        }

        /*Returns whatever is held at the given slot. Doesn't modify the inventory.*/
        public T peekSlot(int x, int y)
        {
            return inv[x][y];
        }

        /*Attempts to insert the given item at the given x,y coordinate
        Returns true if inserted successfully, false otherwise*/
        public bool insertAt(T item, int x, int y)
        {
            if (EqualityComparer<T>.Default.Equals(inv[x][y], default(T))) //if inventory[x][y] == whatever the default value for T is [which it will if [x][y] is empty]
            {
                /* TODO there is either something wrong with this if statement, or the InventoryTest insertatOccupied() has an error
                 main() tests show insertAt() working as expected, though.. so I'm at a loss here.*/
                inv[x][y] = item;
                return true;
            }
            else
            {
                return false;
            }
        }

        /*Temporarily stores the item at x,y. Nulls x,y. Returns whatever was there.*/
        public T removeAt(int x, int y)
        {
            if (EqualityComparer<T>.Default.Equals(inv[x][y], default(T))) //if [x][y] == whatever default T is
            {
                return default(T);
            }
            else
            {
                T temp = inv[x][y];
                inv[x][y] = default(T);
                return temp;
            }
        }


        /*Nulls the given x,y slot*/
        public void deleteAt(int x, int y)
        {

        }

        /*Swaps the given x,y with the given x1,y1*/
        public void swap(int x, int y, int x1, int y1)
        {

        }

        /*Returns a string representation of the contents.*/
        public string toString()
        {
            return "";
        }
    }
}