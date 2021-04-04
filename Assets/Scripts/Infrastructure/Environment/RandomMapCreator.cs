using UnityEngine;

namespace Infrastructure.Environment
{
    public class RandomMapCreator
    {
        private readonly RandomMapData _data;
        private RectInt _walkersBorder;

        private int _createdObjects;

        public RandomMapCreator(RandomMapData data)
        {
            _data = data;
            _createdObjects = 0;
            //Random.InitState(seed);
        }
        
        public char[,] Create()
        {
            var map = CreateEmpty();
            var walkersRouter = SetupWalkersRouter(_data.InitWalkersCount);
            var maxCreatedObjects = _data.GetMaxObjectsCount();

            while (maxCreatedObjects > _createdObjects)
            {
                UpdateWalkers(map, walkersRouter);
            }

            return map;
        }

        private char[,] CreateEmpty()
        {
            var size = _data.GetMapSize();
            return new char[size.x, size.y];
        }

        private MapWalkersRouter SetupWalkersRouter(int walkersCount)
        {
            var size = _data.GetMapSize();
            var walkersBorders = new RectInt(Vector2Int.one, size - 2 * Vector2Int.one);
            var walkersRouter = new MapWalkersRouter(walkersBorders);

            for (var i = 0; i < walkersCount; i++)
            {
                walkersRouter.CreateWalker(_data.WalkerData, _data.StartPosition);
            }
            
            return walkersRouter;
        }
        
        private void UpdateWalkers(char[,] map, MapWalkersRouter router)
        {
            foreach (var walker in router.Walkers)
            {
                TryCreateMapObject(map, walker.Position);
                walker.MoveRandom();
            }
            
            router.TryCreateWalker(_data.ChanceSpawnWalker, _data.WalkerData, _data.MaxWalkerCount);
            router.TryKillWalker(_data.ChanceKillWalker, _data.MinWalkerCount);
        }
        
        private void TryCreateMapObject(char[,] map, Vector2Int position)
        {
            if (map[position.x, position.y] == '#')
                return;

            _createdObjects++;
            map[position.x, position.y] = '#';
        }
    }
}