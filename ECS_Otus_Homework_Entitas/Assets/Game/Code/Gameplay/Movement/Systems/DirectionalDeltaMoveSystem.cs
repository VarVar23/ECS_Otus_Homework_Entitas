using Entitas;
using Infrastructure;
using UnityEngine;

namespace Gameplay.Movement
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public DirectionalDeltaMoveSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;

            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Speed,
                    GameMatcher.Moving,
                    GameMatcher.Direction));
        }

        public void Execute()
        {
            foreach(var entity in _entities)
            {
                Vector3 position = entity.worldPosition.Value + entity.direction.Value * entity.speed.Value * _timeService.DeltaTime;
                entity.ReplaceWorldPosition(position);
            }
        }
    }
}