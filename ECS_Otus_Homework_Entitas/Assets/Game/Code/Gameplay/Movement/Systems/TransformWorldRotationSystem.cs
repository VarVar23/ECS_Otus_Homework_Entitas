using Entitas;
using UnityEngine;

namespace Gameplay.Movement
{
    public class TransformWorldRotationSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _entities;

        public TransformWorldRotationSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Transform,
                    GameMatcher.WorldRotation));
        }


        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.transform.Value.rotation = Quaternion.Euler(entity.worldRotation.Value);
            }
        }
    }
}