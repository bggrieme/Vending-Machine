using System; //Decimal.Round

public class VendingItem
{
    string name{get; set;}
    decimal price{get; set;} 

    public VendingItem(string givenName, decimal givenPrice)
    {
        this.name = givenName;
        this.price = Decimal.Round(givenPrice, 2); //rounds givenPrice to 2 decimal places
    }

}