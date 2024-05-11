using Leopotam.EcsLite;

namespace CubeECS
{
    public static class EcsWorldManager
    {
        private static EcsWorld _ecsWorld;

        public static void SetEcsWorld(EcsWorld world)
        {
            _ecsWorld = world;
        }

        public static EcsWorld GetEcsWorld()
        {
            if (_ecsWorld == null)
            {
                UnityEngine.Debug.LogError("EcsWorld is not initialized!");
            }
            return _ecsWorld;
        }
    }
}