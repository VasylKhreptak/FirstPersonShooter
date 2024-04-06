using Infrastructure.Data.Static;
using Infrastructure.Services.StaticData.Core;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string GameBalancePath = "StaticData/GameBalance";
        private const string GamePrefabsPath = "StaticData/GamePrefabs";

        public GameConfig Config { get; private set; }
        public GameBalance Balance { get; private set; }

        public GamePrefabs Prefabs { get; private set; }

        public void Load()
        {
            Config = Resources.Load<GameConfig>(GameConfigPath);
            Balance = Resources.Load<GameBalance>(GameBalancePath);
            Prefabs = Resources.Load<GamePrefabs>(GamePrefabsPath);
        }
    }
}