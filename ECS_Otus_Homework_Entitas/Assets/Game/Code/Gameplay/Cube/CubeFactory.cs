using Infrastructure;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CubeFactory : ICubeFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IIdentifierService _identifierService;
        private readonly DiContainer _container;

        public CubeFactory(IAssetProvider assetProvider, IIdentifierService identifierService, DiContainer container)
        {
            _assetProvider = assetProvider;
            _identifierService = identifierService;
            _container = container;
        }

        public GameEntity CreateAlly(Vector3 position, Vector3 rotation)
        {
            GameEntity entity = CreateEntity.Game();
            EntityMonoView view = _assetProvider.Load<EntityMonoView>(KnownValues.AssetsPath.AllyCubePrefab);
            CubeConfig config = _assetProvider.Load<CubeConfig>(KnownValues.AssetsPath.CubeConfig);
            EntityMonoView created = _container.InstantiatePrefabForComponent<EntityMonoView>(view);

            float cooldown = Random.Range(config.MinCooldown, config.MaxCooldown);

            entity.AddHp(config.Hp);
            entity.AddId(_identifierService.Next());
            entity.AddView(created);
            entity.AddDirection(Vector3.zero);
            entity.AddTransform(created.transform);
            entity.AddWorldPosition(position);
            entity.AddWorldRotation(rotation);
            entity.AddSpeed(Random.Range(config.MinSpeed, config.MaxSpeed));
            entity.AddStopDistance(Random.Range(config.MinStopDistance, config.MaxStopDistance));
            entity.AddCooldown(cooldown);
            entity.AddCooldownLeft(cooldown);

            entity.isMovingForward = true;
            entity.isMoving = true;
            entity.isAlly = true;
            entity.isCube = true;

            created.Entity = entity;

            return entity;
        }

        public GameEntity CreateEnemy(Vector3 position, Vector3 rotation)
        {
            GameEntity entity = CreateEntity.Game();
            EntityMonoView view = _assetProvider.Load<EntityMonoView>(KnownValues.AssetsPath.EnemyCubePrefab);
            CubeConfig config = _assetProvider.Load<CubeConfig>(KnownValues.AssetsPath.CubeConfig);
            EntityMonoView created = _container.InstantiatePrefabForComponent<EntityMonoView>(view);

            float cooldown = Random.Range(config.MinCooldown, config.MaxCooldown);

            entity.AddHp(config.Hp);
            entity.AddId(_identifierService.Next());
            entity.AddView(created);
            entity.AddDirection(Vector3.zero);
            entity.AddTransform(created.transform);
            entity.AddWorldPosition(position);
            entity.AddWorldRotation(rotation);
            entity.AddSpeed(Random.Range(config.MinSpeed, config.MaxSpeed));
            entity.AddStopDistance(Random.Range(config.MinStopDistance, config.MaxStopDistance));
            entity.AddCooldown(cooldown);
            entity.AddCooldownLeft(cooldown);

            entity.isMovingForward = true;
            entity.isMoving = true;
            entity.isEnemy = true;
            entity.isCube = true;

            created.Entity = entity;

            return entity;
        }
    }
}