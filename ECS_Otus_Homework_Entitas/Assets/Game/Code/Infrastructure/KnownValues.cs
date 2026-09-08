using UnityEngine;

namespace Infrastructure
{
    public static class KnownValues
    {
        public static class AssetsPath
        {
            public static readonly string AllyCubePrefab = "Ally Cube View";
            public static readonly string EnemyCubePrefab = "Enemy Cube View";
            public static readonly string AllyBulletPrefab = "Ally Bullet";
            public static readonly string EnemyBulletPrefab = "Enemy Bullet";
            public static readonly string CubeArmyConfig = "CubeArmy";
            public static readonly string CubeMovementConfig = "CubeMovement";
            public static readonly string CubeConfig = "CubeConfig";
            public static readonly string BulletConfig = "BulletConfig";
        }

        public static class Layers
        {
            public static readonly int Enemy = LayerMask.GetMask("Enemy");
            public static readonly int Ally = LayerMask.GetMask("Ally");
        }
    }
}