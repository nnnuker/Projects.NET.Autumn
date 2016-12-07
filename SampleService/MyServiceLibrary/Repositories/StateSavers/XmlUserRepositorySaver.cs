using System;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Repositories.RepositoryStates;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Repositories.StateSavers
{
    public class XmlUserRepositorySaver : IStateSaver<UserRepositorySnapshot>
    {
        private readonly string filePath;

        public XmlUserRepositorySaver()
        {
            filePath = Directory.GetCurrentDirectory() + @"\RepositoryStateSnapshot.xml";
        }

        public XmlUserRepositorySaver(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                filePath = Directory.GetCurrentDirectory() + @"\RepositoryStateSnapshot.xml";

            this.filePath = filePath;
        }

        public void Save(UserRepositorySnapshot state)
        {
            var formatter = new XmlSerializer(typeof(UserRepositorySnapshot));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, state);
            }
        }

        public UserRepositorySnapshot Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(UserRepositorySnapshot));

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlTextReader reader = new XmlTextReader(fs);

                if (formatter.CanDeserialize(reader))
                {
                    return formatter.Deserialize(reader) as UserRepositorySnapshot;
                }

                return new UserRepositorySnapshot();
            }
        }
    }
}
