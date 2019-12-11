/*Author: Ben Grieme - 2019
    About this class: This is entirely an user-interface class. In order to demonstrate my VendingMachine project, I needed some way for an user to interact with it and give input.
    As such, I could either allow the user to type in input and then go through the mess of validating that input, or I could simply control their means of input - this class is a way to control input.*/
using System;

public class KeyPad
{
    private const string zeroBar = "    0    ";
    private string[] options;
    public int cursor { get; private set; } // e.g. 0-9. Additional options will start counting at -1 and decrement the further down the list

    public KeyPad(string[] additionalOptions = null)
    {
        this.cursor = 5; //arbitrary starting point, could be set to any 0-9 with no issues, though it may have an effect on test cases
        if (additionalOptions != null) { this.options = additionalOptions; }
        else { this.options = new string[1]; }
    }

    /*Moves the cursor based on the passed arrow key argument.*/
    public void moveSelection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.LeftArrow:
                moveLeft();
                break;
            case ConsoleKey.RightArrow:
                moveRight();
                break;
            case ConsoleKey.UpArrow:
                moveUp();
                break;
            case ConsoleKey.DownArrow:
                moveDown();
                break;
        }
    }

    /*Prints a text-based display of the keypad and its current cursor position.*/
    public void printDisplay(ConsoleColor backgroundColor = ConsoleColor.DarkGreen)
    {
        //print numpad 1-9
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x <= 2; x++)
            {
                int currentNum = 3 * y + (x + 1);
                Console.Write("[");
                if (cursor == currentNum) { Console.BackgroundColor = backgroundColor; }
                Console.Write(currentNum);
                Console.ResetColor();
                Console.Write("] ");
            }
            Console.WriteLine();
        }
        //print 0 bar
        Console.Write("[");
        if (cursor == 0) { Console.BackgroundColor = backgroundColor; }
        Console.Write(zeroBar);
        Console.ResetColor();
        Console.Write("]\n");
        //print options
        if (options[0] != null && options.Length > 0)
        {
            char ch = 'A';
            for (int i = 0; i < this.options.Length; i++)
            {
                if (cursor == (-i - 1)) { Console.BackgroundColor = backgroundColor; }
                Console.Write((char)(ch + i));
                Console.ResetColor();
                Console.WriteLine(": " + options[i]);
            }
        }
    }

    /*Movest he cursor left. Wrapping numpad 1-9 horizontally and doing nothing when cursor is <= 0*/
    private void moveLeft()
    {
        if (cursor > 0)
        {
            if (cursor == 7) { cursor = 9; }
            else if (cursor == 4) { cursor = 6; }
            else if (cursor == 1) { cursor = 3; }
            else { cursor--; }
        }
    }

    /*Movest he cursor right. Wrapping numpad 1-9 horizontally and doing nothing when cursor is <= 0*/
    private void moveRight()
    {
        if (cursor > 0)
        {
            if (cursor == 9) { cursor = 7; }
            else if (cursor == 6) { cursor = 4; }
            else if (cursor == 3) { cursor = 1; }
            else { cursor++; }
        }
    }

    /*Moves cursor up. Does nothing if cursor >= 7*/
    private void moveUp()
    {
        if (cursor < 7 && cursor > 0) { cursor += 3; }
        else if (cursor == 0) { cursor = 2; }
        else if (cursor < 0) { cursor++; }
    }

    /*Moves cursor down. Does nothing if cursor is at 0 with no options or at bottom option if options were included.*/
    private void moveDown()
    {
        if (cursor < 0 && cursor > -options.Length) { cursor--; }
        else if (cursor == 0 && this.options[0] != null) { cursor--; }
        else if (cursor > 0 && cursor < 4) { cursor = 0; }
        else if (cursor >= 4) { cursor -= 3; }
    }
}