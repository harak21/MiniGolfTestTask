using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility.Logging;
using Newtonsoft.Json;

namespace MiniGolf.SaveLoad
{
    [UsedImplicitly]
    public class SaveLoadService : ISaveLoadService
    {
        public void Save(string path, ISaveData data)
        {
            using StreamWriter sw = new StreamWriter(path, false);
            sw.Write(JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
            sw.Close();
        }
        
        public async Task<TData> LoadAsync<TData>(string path) where TData : ISaveData
        {
            if (!File.Exists(path))
            {
                return default;
            }
            
            try
            {
                using StreamReader sr = new StreamReader(path);
                var rawData = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<TData>(rawData);
                sr.Close();
                return data;
            }
            catch (Exception e)
            {
                Log.Loading.E(e);
                return default;
            }
        }
    }
}