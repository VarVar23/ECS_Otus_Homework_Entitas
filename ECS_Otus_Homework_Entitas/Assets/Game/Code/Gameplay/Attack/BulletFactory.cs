using Infrastructure;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class BulletFactory : IBulletFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IAssetProvider _assetProvider;
        private DiContainer _container;

        public BulletFactory(IAssetProvider assetProvider, IIdentifierService identifierService, DiContainer container)
        {
            _identifierService = identifierService;
            _assetProvider = assetProvider;
            _container = container;
        }

        public GameEntity CreateAlly(Vector3 position, Vector3 rotation)
        {
            GameEntity entity = CreateEntity.Game();
            BulletConfig config = _assetProvider.Load<BulletConfig>(KnownValues.AssetsPath.BulletConfig);
            EntityMonoView bulletPrefab = _assetProvider.Load<EntityMonoView>(KnownValues.AssetsPath.AllyBulletPrefab);
            EntityMonoView view = _container.InstantiatePrefabForComponent<EntityMonoView>(bulletPrefab);

            view.transform.position = position;
            view.transform.rotation = Quaternion.Euler(rotation);

            entity.AddView(view);
            entity.AddDamage(config.Damage);
            entity.AddWorldPosition(position);
            entity.AddWorldRotation(rotation);
            entity.AddSpeed(config.Speed);
            entity.AddTransform(view.transform);
            entity.AddDirection(view.transform.forward);
            entity.AddCooldown(config.TimeLife);
            entity.AddCooldownLeft(config.TimeLife);
            entity.AddId(_identifierService.Next()); 

            entity.isMoving = true;
            entity.isMovingForward = true;
            entity.isBullet = true;
            entity.isAlly = true;

            view.Entity = entity;

            return entity;
        }

        public GameEntity CreateEnemy(Vector3 position, Vector3 rotation)
        {
            GameEntity entity = CreateEntity.Game();
            BulletConfig config = _assetProvider.Load<BulletConfig>(KnownValues.AssetsPath.BulletConfig);
            EntityMonoView bulletPrefab = _assetProvider.Load<EntityMonoView>(KnownValues.AssetsPath.EnemyBulletPrefab);
            EntityMonoView view = _container.InstantiatePrefabForComponent<EntityMonoView>(bulletPrefab);

            view.transform.position = position;
            view.transform.rotation = Quaternion.Euler(rotation);

            entity.AddView(view);
            entity.AddDamage(config.Damage);
            entity.AddWorldPosition(position);
            entity.AddWorldRotation(rotation);
            entity.AddSpeed(config.Speed);
            entity.AddTransform(view.transform);
            entity.AddDirection(view.transform.forward);
            entity.AddCooldown(config.TimeLife);
            entity.AddCooldownLeft(config.TimeLife);
            entity.AddId(_identifierService.Next());

            entity.isMoving = true;
            entity.isMovingForward = true;
            entity.isBullet = true;
            entity.isEnemy = true;

            view.Entity = entity;

            return entity;
        }
    }
}