using Infrastructure;
using Zenject;

namespace Gameplay.General
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();

            BindFactories();
            BindServices();
            BindProviders();
        }

        private void BindFactories()
        {
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<ICubeFactory>().To<CubeFactory>().AsSingle();
            Container.Bind<IArmyFactory>().To<ArmyFactory>().AsSingle();
            Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
        }

        private void BindProviders()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}