using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Movement
{
    public class FindTargetAfterKillSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _waitEntities;
        private readonly IGroup<GameEntity> _cubes;
        private readonly IFinishFactory _finishFactory;
        private readonly List<GameEntity> _buffer = new(4);
        private bool _finishCreate = false;

        public FindTargetAfterKillSystem(GameContext context, IFinishFactory finishFactory)
        {
            _waitEntities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.FindTarget)
                .NoneOf(
                    GameMatcher.Target));

            _cubes = context.GetGroup(GameMatcher.
                AllOf(
                    GameMatcher.Cube,
                    GameMatcher.Transform)
                .NoneOf(
                    GameMatcher.Destroy));

            _finishFactory = finishFactory;
        }

        public void Execute()
        {
            foreach(var waitEntity in _waitEntities.GetEntities(_buffer))
            {
                float minDistance = float.MaxValue;
                GameEntity target = null;

                foreach(var cube in _cubes)
                {
                    if (waitEntity.isAlly && cube.isAlly || waitEntity.isEnemy && cube.isEnemy) continue;

                    float distance = Vector3.Distance(waitEntity.transform.Value.position, cube.transform.Value.position);
                    
                    if(distance < minDistance)
                    {
                        target = cube;
                        minDistance = distance;
                    }
                }

                if(target != null)
                {
                    waitEntity.AddTarget(target);
                    waitEntity.isFindTarget = false;
                    waitEntity.isMoving = true;
                }
                else
                {
                    if(!_finishCreate)
                    {
                        _finishFactory.Create();
                        _finishCreate = true;
                    }
                    
                }
            }
        }
    }
}