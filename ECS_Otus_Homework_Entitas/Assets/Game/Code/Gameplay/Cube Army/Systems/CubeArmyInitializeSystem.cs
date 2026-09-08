using Entitas;
using UnityEngine;

namespace Gameplay
{
    public class CubeArmyInitializeSystem : IInitializeSystem
    {
        private readonly ICubeFactory _cubeFactory;
        private readonly IGroup<GameEntity> _entities;

        public CubeArmyInitializeSystem(GameContext context, ICubeFactory cubeFactory)
        {
            _cubeFactory = cubeFactory;

            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ArmyCreator,
                    GameMatcher.OffsetXArmy,
                    GameMatcher.OffsetZArmy,
                    GameMatcher.RandomOffsetZArmy,
                    GameMatcher.CountEnemyArmy,
                    GameMatcher.CountAllyArmy));
        }

        public void Initialize()
        {
            foreach (var entity in _entities)
            {
                Vector3 startPosition = Vector3.zero;

                for(int i = 0; i < entity.countAllyArmy.Value; i++)
                {
                    _cubeFactory.CreateAlly(startPosition, Vector3.zero);
                    startPosition += new Vector3(entity.offsetXArmy.Value, 0, Random.Range(-entity.randomOffsetZArmy.Value, entity.randomOffsetZArmy.Value));
                }

                startPosition = new Vector3(0, 0, entity.offsetZArmy.Value);

                for (int i = 0; i < entity.countEnemyArmy.Value; i++)
                {
                    _cubeFactory.CreateEnemy(startPosition, new Vector3(0, 180, 0));
                    startPosition += new Vector3(entity.offsetXArmy.Value, 0, Random.Range(-entity.randomOffsetZArmy.Value, entity.randomOffsetZArmy.Value));
                }
            }
        }
    }
}