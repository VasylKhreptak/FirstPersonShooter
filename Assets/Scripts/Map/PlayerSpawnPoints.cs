using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class PlayerSpawnPoints
    {
        private readonly List<Transform> _spawnPoints;

        public PlayerSpawnPoints(List<Transform> spawnPoints)
        {
            _spawnPoints = new List<Transform>(spawnPoints);
        }

        public IReadOnlyList<Transform> Get() => _spawnPoints;
    }
}