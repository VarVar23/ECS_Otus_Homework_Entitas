using Entitas;
using System.Collections.Generic;

namespace Gameplay
{
    public class SpawnBulletSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _weapons;
        private readonly IBulletFactory _bulletFactory;
        private readonly List<GameEntity> _buffer = new(50);

        public SpawnBulletSystem(GameContext context, IBulletFactory bulletFactory)
        {
            _weapons = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StartShoot,
                    GameMatcher.Shoot));

            _bulletFactory = bulletFactory;
        }

        public void Execute()
        {
            foreach (var weapon in _weapons.GetEntities(_buffer))
            {
                if(weapon.isAlly)
                {
                    _bulletFactory.CreateAlly(weapon.worldPosition.Value, weapon.worldRotation.Value);
                }

                if(weapon.isEnemy)
                {
                    _bulletFactory.CreateEnemy(weapon.worldPosition.Value, weapon.worldRotation.Value);
                }
                
                weapon.isShoot = false;
            }
        }
    }
}