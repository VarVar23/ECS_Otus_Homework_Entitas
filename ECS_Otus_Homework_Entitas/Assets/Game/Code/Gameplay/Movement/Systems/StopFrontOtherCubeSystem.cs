using Entitas;
using Infrastructure;
using System.Collections.Generic;

namespace Gameplay.Movement
{
    public class StopFrontOtherCubeSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<GameEntity> _ally;
        private readonly IGroup<GameEntity> _enemy;
        private List<GameEntity> _buffer = new(256);

        public StopFrontOtherCubeSystem(GameContext context, IPhysicsService physicsService)
        {
            _physicsService = physicsService;

            _ally = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Ally,
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.StopDistance,
                    GameMatcher.Moving));

            _enemy = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform,
                    GameMatcher.StopDistance,
                    GameMatcher.Moving));
        }

        public void Execute()
        {
            foreach (var ally in _ally.GetEntities(_buffer))
            {
                if(_physicsService.RayCast(ally.worldPosition.Value, ally.transform.Value.forward, ally.stopDistance.Value, KnownValues.Layers.Enemy))
                {
                    ally.isMoving = false;
                    ally.isStartShoot = true;
                }
            }

            foreach (var enemy in _enemy.GetEntities(_buffer))
            {
                if (_physicsService.RayCast(enemy.worldPosition.Value, enemy.transform.Value.forward, enemy.stopDistance.Value, KnownValues.Layers.Ally))
                {
                    enemy.isMoving = false;
                    enemy.isStartShoot = true;
                }
            }
        }
    }
}