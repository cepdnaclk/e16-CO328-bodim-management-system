using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using BodimApi.Models;
using BC = BCrypt.Net.BCrypt;

namespace BodimApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IBodimDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UserCollection);
        }
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User GetUser(string email) =>
            _users.Find<User>(user => user.Email == email).FirstOrDefault();

        public User Create(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            _users.InsertOne(user);
            return user;
        }
        public void Update(string id, User userIn)
        {
            userIn.Password = BC.HashPassword(userIn.Password);
            _users.ReplaceOne(user => user.Id == id, userIn);
        }
        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}