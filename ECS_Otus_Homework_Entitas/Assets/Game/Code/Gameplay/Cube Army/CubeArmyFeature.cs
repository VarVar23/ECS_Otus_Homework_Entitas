using Infrastructure;

namespace Gameplay
{
    public class CubeArmyFeature : Feature
    {
        public CubeArmyFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CubeArmyCreateEntitySystem>());
            Add(systemFactory.Create<CubeArmyInitializeSystem>());
        }
    }
}