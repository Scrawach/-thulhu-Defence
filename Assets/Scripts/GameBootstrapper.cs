using Building;
using Building.ShopLogic;
using Cthulhu;
using EnemyLogic;
using EnemyLogic.Spawner;
using Infrastructure.Environment;
using UI;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] 
    private int _initMoney = default;

    [SerializeField] 
    private int _targetScore = default;
    
    [SerializeField] 
    private Vector2Int _boardSize = default;

    [SerializeField] 
    private Transform _boardContainer = default;

    [SerializeField] 
    private Shop _shop = default;

    [SerializeField] 
    private DescriptionView _descriptionView = default;

    [SerializeField] 
    private WalletView _walletView = default;

    [SerializeField] 
    private LevelProgressView _levelProgressSlider = default;

    [SerializeField] 
    private LevelProgress _levelProgress = default;

    [SerializeField] 
    private EnemySpawner _enemySpawner = default;

    [SerializeField] 
    private RandomMapData _mapData = default;

    private void Awake()
    {
        var game = new Game(_boardSize, _boardContainer);
        game.Initialize(_targetScore, _mapData);

        game.Wallet.Add(_initMoney);
        _shop.Construct(game.GameFactory, game.Wallet);
        _descriptionView.Construct(game.Wallet);
        _walletView.Construct(game.Wallet);

        _enemySpawner.Construct(game.GameFactory);
        _levelProgressSlider.Construct(game.Score);
        _levelProgress.Construct(game.GameFactory.Home.GetComponent<Health>(), game.GameFactory.Home.GetComponent<Rise>());
    }
}