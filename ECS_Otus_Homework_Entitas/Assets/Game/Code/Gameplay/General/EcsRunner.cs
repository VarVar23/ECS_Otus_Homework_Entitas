using Infrastructure;
using UnityEngine;
using Zenject;

namespace Gameplay.General
{
    public class EcsRunner : MonoBehaviour
    {
        private GeneralFeature _battleFeature;
        private ISystemFactory _systemsFactory;

        [Inject]
        private void Construct(ISystemFactory systemsFactory)
        {
            _systemsFactory = systemsFactory;
        }

        private void Start()
        {
            _battleFeature = _systemsFactory.Create<GeneralFeature>();
            _battleFeature.Initialize();
        }

        private void Update()
        {
            _battleFeature.Execute();
            _battleFeature.Cleanup();
        }

        private void OnDestroy()
        {
            _battleFeature.TearDown();
        }
    }
}