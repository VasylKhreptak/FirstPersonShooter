using UnityEngine;

namespace Utilities.GameObjectUtilities
{
    public class Lifetime : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private float _lifetime = 10f;

        private void Awake() => Destroy(gameObject, _lifetime);
    }
}