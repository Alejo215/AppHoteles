﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Guest : BaseDocument
    {
        [BsonElement("bookingId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? BookingId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("lastName")]
        public string LastName { get; set; } = null!;

        [BsonElement("gender")]
        public string Gender { get; set; } = null!;

        [BsonElement("documentType")]
        public string? DocumentType { get; set; }

        [BsonElement("document")]
        public string Document { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;
    }
}
