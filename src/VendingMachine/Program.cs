using System;
using CustomCollection;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory<string> inventory = new Inventory<string>(3, 3);
            string testItem1 = ("testItem1");
            string testItem2 = ("testItem2");

            Console.WriteLine(inventory.insertAt(testItem1, 0, 0));
            Console.WriteLine(inventory.insertAt(testItem2, 0, 0));
            Console.WriteLine(EqualityComparer<string>.Default.Equals(inventory.peekSlot(0,0), testItem2));
            Console.ReadKey();
        }
    }
}