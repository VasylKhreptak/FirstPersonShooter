using Extensions;
using TMPro;
using UnityEngine;

namespace UI.Chat
{
    public class MessageTextItem : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text _tmp;

        [Header("Preferences")]
        [SerializeField] private float _stayDuration = 10f;
        [SerializeField] private float _fadeDuration = 1f;

        private float _time;
        private float _alpha;

        #region MonoBehaviour

        private void OnValidate() => _tmp ??= GetComponent<TMP_Text>();

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time < _stayDuration)
                return;

            _alpha = Mathf.Lerp(1f, 0f, (_time - _stayDuration) / _fadeDuration);

            _tmp.color = _tmp.color.WithAlpha(_alpha);

            if (_alpha <= 0f)
                Destroy(gameObject);
        }

        #endregion
    }
}