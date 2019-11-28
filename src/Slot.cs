public class Slot
{
    public VendingItem item;
    public int quantity;
    public Slot(VendingItem givenItem, int givenQuantity = 0)
    {
        item = givenItem;
        quantity = givenQuantity;
    }
}