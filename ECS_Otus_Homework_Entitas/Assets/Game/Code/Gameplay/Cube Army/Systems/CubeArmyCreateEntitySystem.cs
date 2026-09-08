using Entitas;

namespace Gameplay
{
    public class CubeArmyCreateEntitySystem : IInitializeSystem
    {
        private readonly IArmyFactory _armyFactory;

        public CubeArmyCreateEntitySystem(GameContext context, IArmyFactory armyFactory)
        {
            _armyFactory = armyFactory;
        }

        public void Initialize()
        {
            _armyFactory.Create();  
        }
    }
}