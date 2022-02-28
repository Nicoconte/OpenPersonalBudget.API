using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace OpenPersonalBudget.API.Data.Repositories
{
    public class AppMsgRepository : IAppMsgRepository
    {

        private JObject _jsonContent;

        public AppMsgRepository()
        {
            var file = File.OpenText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Assets/ApplicationMessages.json");
            
            JsonTextReader reader = new JsonTextReader(file);
            _jsonContent = (JObject)JToken.ReadFrom(reader);

            //release file
            file.Close();
        }

        public string Get(string id)
        {
            return _jsonContent[id].ToString();
        }
    }
}
