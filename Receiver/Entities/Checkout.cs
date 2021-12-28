using System.Linq;
using Newtonsoft.Json;

namespace Receiver.Entities
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
        public static Checkout DeserializeEntity(string content)
        {
            if(string.IsNullOrWhiteSpace(content))
                throw new Exception("Content is null, empty or whitespace");
            
            var obj = JsonConvert.DeserializeObject<Checkout>(content);
            
            if(obj is null)
                throw new Exception("Deserialized object is not valid");
            
            return obj;
        }
    }
}