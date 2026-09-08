using Infrastructure;

namespace Gameplay
{
    public class ArmyFactory : IArmyFactory
    {
        private readonly IAssetProvider _assetProvider;

        public ArmyFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameEntity Create()
        {
            GameEntity entity = CreateEntity.Game();
            CubeArmyConfig config = _assetProvider.Load<CubeArmyConfig>(KnownValues.AssetsPath.CubeArmyConfig);

            entity.AddCountAllyArmy(config.CountAllyCubes);
            entity.AddCountEnemyArmy(config.CountEnemiesCubes);
            entity.AddOffsetXArmy(config.OffsetX);
            entity.AddOffsetZArmy(config.OffsetZ);
            entity.AddRandomOffsetZArmy(config.RandomOffsetZ);
            entity.isArmyCreator = true;

            return entity;
        }
    }
}