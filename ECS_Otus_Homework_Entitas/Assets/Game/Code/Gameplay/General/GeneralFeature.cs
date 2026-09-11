using Gameplay.Movement;
using Infrastructure;

namespace Gameplay.General
{
    public class GeneralFeature : Feature
    {
        public GeneralFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CubeArmyFeature>());
            Add(systemFactory.Create<MovementFeature>());
            Add(systemFactory.Create<AttackFeature>());
            Add(systemFactory.Create<FinishFeature>());
        }
    }
}