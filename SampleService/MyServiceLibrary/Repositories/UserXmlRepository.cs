using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MyServiceLibrary.Repositories
{
    public class UserXmlRepository : IRepository<User>
    {
        private readonly string filePath;
        private readonly List<User> users = new List<User>();

        public UserXmlRepository()
        {
            filePath = Directory.GetCurrentDirectory() + @"\StateSnapshot.xml";
        }

        public UserXmlRepository(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path is null or empty string");
            }

            filePath = path;
        }

        public bool Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User argument is null");

            if (!users.Exists(u => u.Id == user.Id))
            {
                users.Add(user);
                return true;
            }

            return false;
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return users.FindAll(predicate);
        }

        public IList<User> GetAll()
        {
            return users;
        }

        public bool Delete(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            var findResult = users.Find(user => user.Id == userId);
            if (findResult != null)
            {
                return users.Remove(findResult);
            }

            return false;
        }

        public bool Save()
        {
            var formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, users);
            }

            return true;
        }

        public bool Load()
        {
            var list = LoadUsers();

            foreach (var user in list)
            {
                Add(user);
            }

            return true;
        }

        private List<User> LoadUsers()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlTextReader reader = new XmlTextReader(fs);

                if (formatter.CanDeserialize(reader))
                {
                    return formatter.Deserialize(reader) as List<User>;
                }

                return new List<User>();
            }
        }
    }
}
