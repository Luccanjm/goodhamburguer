using System.ComponentModel;

namespace GoodHamburguer.Enums
{
    public enum ProductTypes
    {
        [Description("Sandwiches")]
        Sandwiches = 1,
        [Description("Soda")]
        Soda = 2,
        [Description("Fries")]
        Fries = 3,
        [Description("Others")]
        Others = 4
    }
}
