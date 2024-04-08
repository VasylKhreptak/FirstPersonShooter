using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.UI.InputFields
{
    public class UsernameRandomizer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_InputField _inputField;

        #region MonoBehaviour

        private void OnValidate() => _inputField ??= GetComponent<TMP_InputField>();

        private void Awake() => _inputField.text = GenerateRandomUsername();

        #endregion

        private string GenerateRandomUsername() => "Player_" + Random.Range(1000, 10000);
    }
}