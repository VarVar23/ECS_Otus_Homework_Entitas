using Infrastructure;

namespace Gameplay.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<DirectionalDeltaMoveSystem>());
            Add(systemFactory.Create<SetDirectionalForwardSystem>());
            Add(systemFactory.Create<TransformWorldPositionSystem>());
            Add(systemFactory.Create<TransformWorldRotationSystem>());
            Add(systemFactory.Create<StopFrontOtherCubeSystem>());
        }
    }
}