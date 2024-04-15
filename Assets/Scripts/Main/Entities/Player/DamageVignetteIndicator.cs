using System;
using DG.Tweening;
using FishNet.Object;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Main.Entities.Player
{
    public class DamageVignetteIndicator : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health.Health _health;
        [SerializeField] private PostProcessVolume _volume;

        [Header("Preferences")]
        [SerializeField] private float _targetIntensity = 0.5f;
        [SerializeField] private float _firstHalfDuration = 0.1f;
        [SerializeField] private AnimationCurve _firstHalfCurve;
        [SerializeField] private float _secondHalfDuration = 0.2f;
        [SerializeField] private AnimationCurve _secondHalfCurve;

        private IDisposable _damageSubscription;
        private Tween _intensityTween;
        private Vignette _vignette;

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsOwner == false)
            {
                enabled = false;
                return;
            }

            _vignette = _volume.profile.GetSetting<Vignette>();
            _damageSubscription = _health.Value
                .Pairwise()
                .Where(pair => pair.Previous > pair.Current)
                .Subscribe(_ => Trigger());
        }

        private void OnDestroy()
        {
            if (IsOwner == false)
                return;

            _damageSubscription?.Dispose();
            KillIntensityKill();
            SetVignetteEnabled(false);
        }

        [Button]
        private void Trigger()
        {
            SetVignetteEnabled(true);
            KillIntensityKill();
            _intensityTween = DOTween
                .Sequence()
                .Append(CreateIntensityTween(_targetIntensity, _firstHalfDuration, _firstHalfCurve))
                .Append(CreateIntensityTween(0f, _secondHalfDuration, _secondHalfCurve))
                .OnComplete(() => SetVignetteEnabled(false))
                .Play();
        }

        private Tween CreateIntensityTween(float intensity, float duration, AnimationCurve curve) =>
            DOTween
                .To(GetIntensity, SetIntensity, intensity, duration)
                .SetEase(curve);

        private void KillIntensityKill() => _intensityTween?.Kill();

        private float GetIntensity() => _vignette.intensity.value;

        private void SetIntensity(float intensity) => _vignette.intensity.value = intensity;

        private void SetVignetteEnabled(bool enabled) => _vignette.enabled.value = enabled;
    }
}