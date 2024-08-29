using Azure;
using Azure.Data.Tables;
using System;

namespace Retail.Models
{
    public class CustomerProfile : ITableEntity
    {
        // The PartitionKey is used to partition the data for performance and scalability.
        public string PartitionKey { get; set; }

        // The RowKey uniquely identifies the entity within a PartitionKey.
        public string RowKey { get; set; }

        // Customer-specific properties
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Required by the ITableEntity interface
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Parameterless constructor
        public CustomerProfile()
        {
        }

        // Constructor to initialize PartitionKey and RowKey
        public CustomerProfile(string partitionKey, string rowKey, string name, string lastName, string email, string phoneNumber)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
