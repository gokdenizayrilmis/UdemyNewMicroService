using MongoDB.Bson.Serialization.Attributes;

namespace UdemyNewMicroService.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
