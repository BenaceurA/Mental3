using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
namespace Mental3
{
    class MapReader
    {
        public static MapData Read(string source)
        {
            string rawData = File.ReadAllText("Content/Maps/" + source + "/" + source + ".json");
            return JsonConvert.DeserializeObject<MapData>(rawData);
        }
    }
}
