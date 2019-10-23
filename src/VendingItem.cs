using System; //Decimal.Round

public class VendingItem
{
    string name{get; set;}
    int quantity{get; set;}
    decimal price{get; set;} 

    public VendingItem(string givenName, int givenQuantity, decimal givenPrice)
    {
        name = givenName;
        quantity = givenQuantity;
        price = Decimal.Round(givenPrice, 2); //rounds givenPrice to 2 decimal places
    }

}