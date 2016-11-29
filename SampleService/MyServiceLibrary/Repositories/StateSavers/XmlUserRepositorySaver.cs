using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Repositories.RepositoryStates;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MyServiceLibrary.Repositories.StateSavers
{
    public class XmlUserRepositorySaver : IStateSaver<UserRepositorySnapshot>
    {
        public void Save(UserRepositorySnapshot state, string filePath)
        {
            var formatter = new XmlSerializer(typeof(UserRepositorySnapshot));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, state);
            }
        }

        public UserRepositorySnapshot Load(string filePath)
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
