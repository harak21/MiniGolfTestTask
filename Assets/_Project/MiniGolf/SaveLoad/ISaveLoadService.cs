using System.Threading.Tasks;

namespace MiniGolf.SaveLoad
{
    public interface ISaveLoadService
    {
        void Save(string path, ISaveData data);
        Task<TData> LoadAsync<TData>(string path) where TData : ISaveData;
    }
}