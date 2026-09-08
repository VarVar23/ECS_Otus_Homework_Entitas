using Entitas;
using System.Collections.Generic;

namespace Gameplay.Movement
{
    public class SetDirectionalForwardSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(16);

        public SetDirectionalForwardSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.MovingForward,
                    GameMatcher.Moving,
                    GameMatcher.Direction,
                    GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach(var entity in _entities.GetEntities(_buffer))
            {
                entity.ReplaceDirection(entity.transform.Value.forward);
            }
        }
    }
}