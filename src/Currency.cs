/*Author: Ben Grieme - 2019
    About this class: This class stores the value of each of these U.S. currency types in the form of cents.
    First, I want to say that I am aware there are almost certainly better ways to have done this. For example, a Dictionary<string, decimal> might better serve this purpose.
    However, I wanted to better familiarize myself with the use of enums in C#.
    As I discovered, they cannot contain non-integral values - hence the primary reason why monetary values are mostly stored as ints instead of as decimal throughout most of this project.
    I took this as a good opportunity to experiment with loss-less conversions between decimal types and int types.*/

/*Enum representing values of different US currencies in cents*/
public enum Currency
{
    PENNY = 1,
    NICKEL = 5,
    DIME = 10,
    QUARTER = 25,
    DOLLAR = 100
}