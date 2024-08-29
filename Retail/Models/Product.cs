using Azure;
using Azure.Data.Tables;

namespace Retail.Models
{
    public class Product : ITableEntity
    {
        // The PartitionKey is used to partition the data for performance and scalability.
        public string PartitionKey { get; set; }

        // The RowKey uniquely identifies the entity within a PartitionKey.
        public string RowKey { get; set; }

        // Product-specific properties
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Required by the ITableEntity interface
        public DateTimeOffset? Timestamp { get; set; } // Make this property nullable
        public ETag ETag { get; set; }

        // Parameterless constructor
        public Product()
        {
        }

        // Constructor to initialize PartitionKey and RowKey
        public Product(string partitionKey, string rowKey, string name, string description, decimal price, int stockQuantity)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
