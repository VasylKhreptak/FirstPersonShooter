using System;
using Map;
using Networking;
using UnityEngine;
using Zenject;

namespace Main.Zenject
{
    public class MainInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private MapCamera _mapCamera;

        #region MonoBehaviour

        private void OnValidate() => _mapCamera ??= FindObjectOfType<MapCamera>(true);

        #endregion

        public override void InstallBindings()
        {
            Container.Bind<ClientServerConnection>().AsSingle();
            Container.BindInstance(_mapCamera).AsSingle();
        }
    }
}