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

    public void printDisplay(ConsoleColor backgroundColor = ConsoleColor.DarkGreen)
    {
        //print numpad 1-9
        //print zeroBar
        //print options
    }

    private void moveLeft()
    {
        //TODO
    }

    private void moveRight()
    {
        //TODO
    }

    private void moveUp()
    {
        //TODO
    }

    private void moveDown()
    {
        //TODO
    }


}