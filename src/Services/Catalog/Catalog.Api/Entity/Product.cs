using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Catalog.Api.Entity
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { set; get; }

        public string Category { set; get; }
        public string Summary { set; get; }
        public string Description { set; get; }
        public string ImageFile { set; get; }
        public decimal Price { set; get; }
    }
}
