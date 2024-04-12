using System;
using UnityEngine;

namespace Infrastructure.Data.Static
{
    [Serializable]
    public class AudioSettings
    {
        [SerializeField] private float _spatialBlendLocal;
        [SerializeField] private float _spatialBlendClients = 1f;
        [SerializeField] private AudioRolloffMode _rolloffMode = AudioRolloffMode.Linear;
        [SerializeField] private float _minDistance = 1f;
        [SerializeField] private float _maxDistance = 50f;

        public float SpatialBlendLocal => _spatialBlendLocal;
        public float SpatialBlendClients => _spatialBlendClients;
        public AudioRolloffMode RolloffMode => _rolloffMode;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
    }
}