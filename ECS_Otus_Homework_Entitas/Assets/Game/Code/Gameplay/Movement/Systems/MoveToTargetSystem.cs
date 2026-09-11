using Entitas;
using Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Movement
{
    public class MoveToTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(4);
        private readonly ITimeService _timeService;

        public MoveToTargetSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;

            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Target,
                    GameMatcher.Moving,
                    GameMatcher.Speed));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var transform = entity.transform.Value;

                LookAtTarget(entity);
                entity.ReplaceWorldPosition(transform.position + (transform.forward * entity.speed.Value * _timeService.DeltaTime));
            }
        }

        private void LookAtTarget(GameEntity entity)
        {
            var direction = entity.target.Value.transform.Value.position - entity.transform.Value.position;

            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                var lookRotation = Quaternion.LookRotation(direction);
                var euler = entity.worldRotation.Value;

                euler.y = lookRotation.eulerAngles.y;

                entity.worldRotation.Value = euler;
                entity.transform.Value.rotation = Quaternion.Euler(euler);
            }
        }
    }
}