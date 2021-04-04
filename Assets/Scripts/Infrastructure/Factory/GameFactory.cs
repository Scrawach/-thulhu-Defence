using Board.Tile;
using Building;
using Cthulhu;
using EnemyLogic;
using EnemyLogic.BulletLogic;
using Infrastructure.AssetManagement;
using Items;
using Player;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly Wallet _wallet;
        private readonly Score _score;
        
        private GameObject _home;
        private Health _homeHealth;

        public GameObject Home => _home;
        public Wallet Wallet => _wallet;
        public Score Score => _score;
        

        public GameFactory(AssetProvider assetProvider, Wallet wallet, Score score)
        {
            _assetProvider = assetProvider;
            _wallet = wallet;
            _score = score;
        }

        public BoardTile CreateBoardTile(Vector3 position, Transform parent) => 
            _assetProvider.Initialize(AssetPath.BoardTile, position, parent).GetComponent<BoardTile>();

        public void CreateLeftTile(Vector3 position, Transform parent) =>
            _assetProvider.Initialize(AssetPath.BoardLeft, position, parent);

        public void CreateDownTile(Vector3 position, Transform parent) =>
            _assetProvider.Initialize(AssetPath.BoardDown, position, parent);

        public void CreateDrop(DropType type, Vector3 position)
        {
            switch (type)
            {
                case DropType.Money:
                    var money = _assetProvider.Initialize(AssetPath.MoneyDrop, position);
                    money.GetComponent<MoneyDrop>().Construct(this);
                    break;
                case DropType.Bonus:
                    var bonus = _assetProvider.Initialize(AssetPath.BonusDrop, position);
                    bonus.GetComponent<BonusDrop>().Construct(this);
                    break;
            }
        }
        
        public void CreateBuilding(BuildingType type, BoardTile tile)
        {
            tile.Content = TileContent.Bused;
            
            switch (type)
            {
                case BuildingType.Home:
                    _home = _assetProvider.Initialize(AssetPath.Home, tile.transform.position);
                    _homeHealth = _home.GetComponent<Health>();
                    _home.GetComponent<Rise>().Construct(Score);
                    _home.GetComponent<Temple>().Construct(this, _homeHealth);
                    tile.SetBuilding(_home);
                    break;
                case BuildingType.Tower:
                    var tower = _assetProvider.Initialize(AssetPath.Tower, tile.transform.position);
                    tile.SetBuilding(tower);
                    break;
                case BuildingType.Temple:
                    var temple = _assetProvider.Initialize(AssetPath.Temple, tile.transform.position); 
                    temple.GetComponent<Temple>().Construct(this, _homeHealth);
                    tile.SetBuilding(temple);
                    break;
                default:
                    Debug.LogError("Not expected building type!");
                    break;
            }
        }

        public void CreateEnemy(Vector3 position)
        {
            CreateEnemy(AssetPath.Submarine, position);
        }
        
        public void CreateRocket(Vector3 position)
        {
            CreateEnemy(AssetPath.RocketEnemy, position);
        }
        
        private void CreateEnemy(string path, Vector3 position)
        {
            var enemy = _assetProvider.Initialize(path, position);
            
            if (enemy.TryGetComponent(out MoveToTarget moveToTarget))
                moveToTarget.Construct(_home.transform);
            
            if (enemy.TryGetComponent(out RotateToTarget rotateToTarget))
                rotateToTarget.Construct(_home.transform);
            
            if (enemy.TryGetComponent(out MoveWithChange moveWithChanges))
                moveWithChanges.Construct(_home.transform);
            
            if (enemy.TryGetComponent(out Bullet bulletComponent))
                bulletComponent.Construct(_home.GetComponent<Health>());
            
            if (enemy.TryGetComponent(out DeathDrop deathDrop))
                deathDrop.Construct(this);
            
            if (enemy.TryGetComponent(out AttackThenAround attackThenAround))
                attackThenAround.Construct(_home.GetComponent<Health>(), this);
        }

        public void CreateBullet(Vector3 position)
        {
            var bullet = _assetProvider.Initialize(AssetPath.Bullet, position);
            
            if (bullet.TryGetComponent(out MoveToTarget moveToTarget))
                moveToTarget.Construct(_home.transform);
            
            if (bullet.TryGetComponent(out RotateToTarget rotateToTarget))
                rotateToTarget.Construct(_home.transform);
            
            if (bullet.TryGetComponent(out Bullet bulletComponent))
                bulletComponent.Construct(_home.GetComponent<Health>());
        }
    }
}