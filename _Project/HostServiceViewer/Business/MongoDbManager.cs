using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Business
{
    public class MongoDbManager
    {
        //Robo 3T ile mongodbye bağlanarak kullanıcı adı ve şifresini değiştirebilirsin.
        //mongodb değişkeni docker-compose.yml dosyasından container_name üzerinden alınmaktadır.

        private const string MongoDb = "mongodb";//Docker Compose ile çalışacaksan burayı aç
        //private const string MongoDb = "127.0.0.1";//IIS Express ile çalışacaksan burayı aç

        private const string ConnectionString = "mongodb://admin:Hsv20211229++@" + MongoDb + ":27017/HostServiceViewerDb?w=majority";
        private const string DatabaseName = "HostServiceViewerDb";
        public List<Test> SelectAll()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            var testCollection = database.GetCollection<Test>("Test");
            var results = testCollection.Find(x => true).ToList();


            return results;
        }
        public List<Test> SelectActive()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            var testCollection = database.GetCollection<Test>("Test");
            var results = testCollection.Find(x => x.Active == true).ToList();

            return results;
        }
        public void Get()
        {

        }
        public void Find()
        {

        }
        public Test Insert(Test test)
        {
            test.Id = ObjectId.GenerateNewId();

            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            var testCollection = database.GetCollection<Test>("Test");
            testCollection.InsertOne(test);

            return test;
        }
        public UpdateResult Update(Test test)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            var testCollection = database.GetCollection<Test>("Test");

            var filter = Builders<Test>.Filter.Eq(s => s.Id, ObjectId.Parse(test.MongoDbId));

            var update = Builders<Test>.Update
            .Set(s => s.Name, test.Name)
            .Set(s => s.Description, test.Description)
            .Set(s => s.Type, test.Type)
            .Set(s => s.Ip, test.Ip)
            .Set(s => s.Port, test.Port)
            .Set(s => s.PortName, test.PortName)
            .Set(s => s.Url, test.Url)
            .Set(s => s.Timer, test.Timer)
            .Set(s => s.Order, test.Order)
            .Set(s => s.Active, test.Active);


            var result = testCollection.UpdateOne(filter, update);

            return result;
        }
        public void Delete(Test test)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);

            var testCollection = database.GetCollection<Test>("Test");
            testCollection.DeleteOne(x => x.Id == test.Id);

        }

    }
}
