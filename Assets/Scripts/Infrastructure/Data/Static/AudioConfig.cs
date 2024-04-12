using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Data.Static
{
    [Serializable]
    public class AudioConfig
    {
        [SerializeField] private List<AudioClip> _clips;
        [SerializeField] private AudioSettings _settings;

        public IReadOnlyList<AudioClip> Clips => _clips;
        public AudioSettings Settings => _settings;
    }
}