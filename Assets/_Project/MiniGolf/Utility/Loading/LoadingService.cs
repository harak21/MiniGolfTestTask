using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MiniGolf.Utility.Logging;

namespace MiniGolf.Utility.Loading
{
    [UsedImplicitly]
    public class LoadingService : ILoadingService
    {
        private readonly Stopwatch _watch = new();
        
        public async Task Load(ILoadUnit loadUnit)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(loadUnit);
                await loadUnit.Load();
                isError = false;
            }
            catch (Exception e)
            {
                Log.Loading.E(e);
                throw;
            }
            finally
            {
                OnLoadingFinish(loadUnit, isError);
            }
        }
        
        private void OnLoadingBegin(object unit)
        {
            _watch.Restart();
            Log.Loading.D($"{unit.GetType().Name} loading is started");
        }

        private void OnLoadingFinish(object unit, bool isError)
        {
            _watch.Stop();
            Log.Loading.D($"{unit.GetType().Name} is {(isError ? "NOT " : "")}loaded with time {_watch.ElapsedMilliseconds}ms");
        }
    }
}