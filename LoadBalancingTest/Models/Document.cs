using System;
using MongoDB.Bson;

namespace LoadBalancingTest.Models
{
    public abstract class Document : IDocument
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

