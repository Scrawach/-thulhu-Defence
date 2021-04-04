using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Environment;
using Infrastructure.Factory;
using Player;
using UnityEngine;

public class Game
{
    private readonly Vector2Int _boardSize;
    private readonly Transform _boardContainer;

    public GameFactory GameFactory { get; private set; }
    public GameBoard GameBoard { get; private set; }
    public Wallet Wallet { get; private set; }
    public Score Score { get; private set; }

    public Game(Vector2Int boardSize, Transform boardContainer)
    {
        _boardSize = boardSize;
        _boardContainer = boardContainer;
    }
    
    public void Initialize(int targetScore, RandomMapData data)
    {
        var assetProvider = new AssetProvider();
        Wallet = new Wallet();
        Score = new Score(targetScore);

        GameFactory = new GameFactory(assetProvider, Wallet, Score);
        GameBoard = new GameBoard(GameFactory, _boardContainer);

        var randomMapCreator = new RandomMapCreator(data);
        var map = randomMapCreator.Create();
        GameBoard.Generate(map, data.StartPosition);
    }
}
