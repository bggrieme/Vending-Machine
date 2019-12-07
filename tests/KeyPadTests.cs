using Xunit;

public class KeyPadTests
{
    static string[] options = { "This", "is", "a", "TEST!" };
    KeyPad pad = new KeyPad(options);

    [Fact(DisplayName = "Moving cursor beyond boundary horizontally should wrap to other side.")]
    public void Horizontal_cursorMovement_shouldWrap__testsLeftAndRight_1_through_9()
    {
        for (int i = 0; i < 15 && pad.cursor < 9; i++) //sets the pad's cursor position to 9
        {
            pad.moveSelection(System.ConsoleKey.UpArrow);
            pad.moveSelection(System.ConsoleKey.RightArrow);
        }
        Assert.True(pad.cursor == 9, "Cursor must == 9 at this point. Try increasing the maximum allowed cycles of the above loop in test or, I don't know, don't set the default starting position of cursor to < 0. :)");
        for (int i = 9; i > 0; i -= 3) // i == 9, 6, 3
        {
            Assert.True(pad.cursor == i, "Cursor was expected to be set to " + i + ", but instead is: " + pad.cursor + " (i == " + i + ")");
            pad.moveSelection(System.ConsoleKey.RightArrow);
            Assert.True(pad.cursor == i - 2, "Cursor was expected to be set to " + (i - 2) + " after wrapping right, but instead is: " + pad.cursor + " (i == " + i + ")");
            pad.moveSelection(System.ConsoleKey.LeftArrow);
            Assert.True(pad.cursor == i, "Cursor was expected to be set to 9 after wrapping left, but instead is: " + pad.cursor + " (i == " + i + ")");
            pad.moveSelection(System.ConsoleKey.DownArrow);
        }
        Assert.True(pad.cursor == 0);
    }

    [Fact (DisplayName = "Vertical movement should range from -this.options.length to 8.")]
    public void vertical_movement()
    {
        for (int i = 0; i < 15 && pad.cursor > -options.Length; i++) //sets the pad's cursor position to lowest position
        {
            pad.moveSelection(System.ConsoleKey.DownArrow);
        }
        Assert.True(pad.cursor == -4, "Cursor should have been set to the lowest possible position (" + (-4) + ") but was instead: " + pad.cursor);
        for(int i = 1; i <= 4; i++) //iterate up options. Cursor == -4 at start of loop and should == 0 at end of loop
        {
            pad.moveSelection(System.ConsoleKey.UpArrow);
            Assert.True(pad.cursor == -4+i, "Cursor should == " + (-4+i) + ", but instead it is: " + pad.cursor + " (i == " + i + ")");
        }
            pad.moveSelection(System.ConsoleKey.UpArrow);
            Assert.True(pad.cursor == 2, "Cursor should == 2, but instead it is: " + pad.cursor);
            pad.moveSelection(System.ConsoleKey.UpArrow);
            Assert.True(pad.cursor == 5, "Cursor should == 5, but instead it is: " + pad.cursor);
            pad.moveSelection(System.ConsoleKey.UpArrow);
            Assert.True(pad.cursor == 8, "Cursor should == 8, but instead it is: " + pad.cursor);
    }
}