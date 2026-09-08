using Entitas;

namespace Gameplay.Movement
{
    public class TransformWorldPositionSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _entities;

        public TransformWorldPositionSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Transform,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach(var entity in _entities)
            {
                entity.transform.Value.position = entity.worldPosition.Value;
            }
        }
    }
}