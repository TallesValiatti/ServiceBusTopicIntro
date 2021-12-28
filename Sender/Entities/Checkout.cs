using System.Linq;

namespace Sender.Entities
{
    public class Checkout
    {
        public IEnumerable<Product> Products { get; private set; }
        public double Amount 
        { 
            get 
            { 
                return this.Products.Sum( x => x.Value);
            }
        }

        public Checkout(IEnumerable<Product> products)
        {
            this.Products = products;
        }

        public string SerializeEntity()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}