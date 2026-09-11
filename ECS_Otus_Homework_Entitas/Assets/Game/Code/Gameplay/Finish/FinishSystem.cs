using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class FinishSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _liveCubes;
        private readonly IGroup<GameEntity> _finishes;
        private readonly List<GameEntity> _buffer = new(1);

        public FinishSystem(GameContext context)
        {
            _liveCubes = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Cube)
                .NoneOf(
                    GameMatcher.Destroy));

            _finishes = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Finish));
        }

        public void Execute()
        {
            foreach(var finish in _finishes.GetEntities(_buffer))
            {
                int countAlly = 0;
                int countEnemy = 0;

                foreach(var entity in _liveCubes)
                {
                    if(entity.isAlly)
                    {
                        countAlly++;
                    }
                    else if(entity.isEnemy)
                    {
                        countEnemy++;
                    }

                    entity.isShoot = false;
                    entity.isStartShoot = false;
                    entity.isMoving = false;
                }

                if (countAlly > countEnemy)
                {
                    Debug.Log("Победили <color=blue>синии</color>. В живых осталось: " + countAlly);
                }
                else
                {
                    Debug.Log("Победили <color=red>красные</color>. В живых осталось: " + countEnemy);
                }

                finish.isFinish = false;
            }
        }
    }
}