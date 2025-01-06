using Bie_Shop.Contracts;
using Bie_Shop.General;

namespace Bie_Shop.ProductManagement
{
    public sealed class BoxedProduct : Product, ISaveable
    {
        private int amountPerBox;

        public int AmountPerBox
        {
            get
            {
                return amountPerBox;
            }
            set
            {
                if (amountPerBox > 0)
                {
                    amountPerBox = value;
                }
            }
        }
        public BoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int amountPerBox) : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public override void UseProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = AmountPerBox * smallestMultiple;
                    break;
                }
            }
            base.UseProduct(batchSize);
        }

        public override void IncreaseStock()
        {
            AmountInStock += AmountPerBox;
        }

        public override void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount * amountPerBox;
            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount * amountPerBox;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{createSimpleProductRepresentation} stack overflow, {newStock - AmountInStock} item(s) ordered that couldn't be stored");
            }

            if(AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.itemPrice};{(int)Price.Currency};{(int)UnitType};{1}; {AmountPerBox}";
        }

        //public void Log(string message)
        //{
        //    Console.WriteLine(message);
        //}
    }
}
