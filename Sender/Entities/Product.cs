namespace Sender.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }

        public Product(Guid id, string name, double value)
        {
            this.Id = id;
            this.Name = name;
            this.Value = value;
        }
    }
}