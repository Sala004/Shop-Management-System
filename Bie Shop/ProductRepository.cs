using Bie_Shop.Contracts;
using Bie_Shop.General;
using Bie_Shop.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop
{
    internal class ProductRepository
    {
        string filePath = "products.txt";

        private void checkForExistingProductFile()
        {
            bool ExistingFileFound = File.Exists(filePath);
            if(!ExistingFileFound)
            {
               using FileStream fs = File.Create(filePath);
            }
        }

        public List<Product> LoadProductsFromFile()
        {
            List<Product> products = new List<Product>();
            try
            {
                checkForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(filePath);
                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    bool success = int.TryParse(productSplits[0], out int productId);
                    if (!success)
                    {
                        productId = 0; //defaul value
                    }

                    string name = productSplits[1];
                    string description = productSplits[2];

                    success = int.TryParse(productSplits[3], out int maxItemsInStock);
                    if (!success)
                    {
                        maxItemsInStock = 100; //default value
                    }

                    success = int.TryParse(productSplits[4], out int ItemPrice);
                    if (!success)
                    {
                        ItemPrice = 0; //default value
                    }

                    success = Enum.TryParse(productSplits[5], out Currency currency);
                    if (!success)
                    {
                        currency = Currency.Dollar; //default value
                    }

                    success = Enum.TryParse(productSplits[6], out UnitType unitType);
                    if (!success)
                    {
                        unitType = UnitType.PerItem; //default value
                    }
                    string productType = productSplits[7];
                    Product product = null;

                    switch (productType)
                    {
                        case "1":
                            success = int.TryParse(productSplits[8], out int amountPerBox);
                            if (!success)
                            {
                                amountPerBox = 1;//default value
                            }

                            product = new BoxedProduct(productId, name, description, new Price() { itemPrice = ItemPrice, Currency = currency }, maxItemsInStock, amountPerBox);
                            break;

                        case "2":
                            product = new FreshProduct(productId, name, description, new Price() { itemPrice = ItemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                        case "3":
                            product = new BulkProduct(productId, name, description, new Price() { itemPrice = ItemPrice, Currency = currency }, maxItemsInStock);
                            break;
                        case "4":
                            product = new RegularProduct(productId, name, description, new Price() { itemPrice = ItemPrice, Currency = currency }, unitType, maxItemsInStock);
                            break;
                    }

                    products.Add(product);
                }
            }

            catch (IndexOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while parsing the file, please check the data!");
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException fnfx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found");
                Console.WriteLine(fnfx.Message);
            }
            catch (Exception iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(iex.Message);
            }
            finally 
            {
                Console.ResetColor();
            }

            return products;
        }

        public void SaveToFile(List<ISaveable> saveables)
        {
            if (saveables == null || saveables.Count == 0)
            {
                Console.WriteLine("No items to save!");
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach(var item in saveables)
            {
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.AppendAllText(filePath, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved items successfully");
            Console.ResetColor();
        }
    }
}
