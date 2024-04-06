using Infrastructure.Data.SaveLoad.Core;
using Infrastructure.Data.Static;

namespace Infrastructure.Services.StaticData.Core
{
    public interface IStaticDataService : ILoadHandler
    {
        public GameConfig Config { get; }
        public GameBalance Balance { get; }
        public GamePrefabs Prefabs { get; }
    }
}