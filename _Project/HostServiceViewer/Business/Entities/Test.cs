using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities
{
    public class Test
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string MongoDbId
        {
            get { return Id.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Id = ObjectId.Parse(value);
            }
        }

        //[BsonElement("_id")]
        //[JsonProperty("_id")]
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }//ping,telnet,page
        public string Ip { get; set; }
        public int Timer { get; set; }
        public int Port { get; set; }

        public string PortName { get; set; }

        public string Url { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
    }
    /*
{
"_id" : ObjectId("61cce1b7afd49102088425e8"),
"Name" : "Ping",
"Description" : "Google Ping",
"Type" : "ping",
"Ip" : "8.8.8.8",
"Timer" : 2,
"Port" : 0,
"PortName" : "",
"Url" : "",
"Active" : true,
"Order" : 0
}     
     */
}
