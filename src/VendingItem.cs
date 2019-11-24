using System; //Decimal.Round

public class VendingItem
{
    public string name{get; set;}
    public decimal price{get; private set;} 

    public VendingItem(string givenName, decimal givenPrice)
    {
        this.name = givenName;
        this.price = Decimal.Round(givenPrice, 2); //rounds givenPrice to 2 decimal places
    }

    public void setPrice(decimal newPrice)
    {
        this.price = newPrice;
    }

}