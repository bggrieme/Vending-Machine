/*Author: Ben Grieme - 2019
    About this class: This class represents a product for sale. It stores a product name and a price.
    For an explanation on why this is the only entity throughout the entire project to represent monetary values as decimal instead of int, please see the "About this class" comment in Currency.cs*/

using System; //Decimal.Round

public class VendingItem
{
    public string name{get; set;}
    public decimal price{get; private set;} 

    public VendingItem(string givenName, decimal givenPrice)
    {
        this.name = givenName;
        this.price = Decimal.Round(givenPrice, 2); //rounds givenPrice to 2 decimal places
        this.price += 0.00m; //forces sig figs.
    }

    public void setPrice(decimal newPrice)
    {
        this.price = newPrice;
    }

}