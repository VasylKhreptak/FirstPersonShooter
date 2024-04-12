using Infrastructure.Serialization;
using UnityEngine;
using AudioType = Infrastructure.Data.Static.Core.AudioType;

namespace Infrastructure.Data.Static
{
    [CreateAssetMenu(fileName = "GameAudioConfig", menuName = "ScriptableObjects/Static/GameAudioConfig", order = 0)]
    public class GameAudioConfig : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<AudioType, AudioConfig> _audioConfigs =
            new SerializedDictionary<AudioType, AudioConfig>();

        public AudioConfig this[AudioType audioType] => _audioConfigs[audioType];
    }
}