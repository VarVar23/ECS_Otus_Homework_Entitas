using Infrastructure;
using Zenject;

namespace Gameplay.General
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<ISystemFactory>().To<ISystemFactory>();
        }
    }
}