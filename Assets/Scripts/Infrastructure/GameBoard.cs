using System.Collections.Generic;
using Board;
using Board.Tile;
using Building;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure
{
    public class GameBoard
    {
        private readonly GameFactory _gameFactory;
        private readonly Transform _container;

        public BoardTile CenterTile;

        public GameBoard(GameFactory gameFactory, Transform container)
        {
            _gameFactory = gameFactory;
            _container = container;
        }

        public List<BoardTile> Generate(Vector2Int size)
        {
            var tiles = new List<BoardTile>(size.x * size.y);
            var homePlaced = false;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var position = new Vector3(x, 0f, y);
                    var tile = _gameFactory.CreateBoardTile(position, _container);
                    tiles.Add(tile);

                    if (!homePlaced)
                    {
                        if (x >= size.x / 2 && y >= size.y / 2)
                        {
                            homePlaced = true;
                            _gameFactory.CreateBuilding(BuildingType.Home, tile);
                        }
                    }
                }
            }

            return tiles;
        }
        
        public List<BoardTile> Generate(char[,] size, Vector2Int center)
        {
            var tiles = new List<BoardTile>(size.GetLength(0) * size.GetLength(1));

            for (int x = 0; x < size.GetLength(0); x++)
            {
                for (int y = 0; y < size.GetLength(1); y++)
                {
                    BoardTile tile;
                    
                    if (x == center.x && y == center.y)
                    {
                        tile = AddTile(x, y, tiles);
                        _gameFactory.CreateBuilding(BuildingType.Home, tile);
                        GenerateLeftWall(size, y, x, tile);
                        GenerateDownWall(size, x, y, tile);
                        continue;
                    }
                    
                    if (size[x, y] != '#')
                        continue;
                    
                    tile = AddTile(x, y, tiles);

                    GenerateLeftWall(size, y, x, tile);
                    GenerateDownWall(size, x, y, tile);
                }
            }

            return tiles;
        }

        private void GenerateDownWall(char[,] size, int x, int y, BoardTile tile)
        {
            var down = y - 1;
            
            if (down >= 0)
            {
                if (size[x, down] != '#')
                    _gameFactory.CreateDownTile(new Vector3(x, 0f, y), tile.transform);
            }
            else
            {
                _gameFactory.CreateDownTile(new Vector3(x, 0f, y), tile.transform);
            }
        }

        private void GenerateLeftWall(char[,] size, int y, int x, BoardTile tile)
        {
            var left = x - 1;
            
            if (left >= 0)
            {
                if (size[left, y] != '#')
                    _gameFactory.CreateLeftTile(new Vector3(x, 0f, y), tile.transform);
            }
            else
            {
                _gameFactory.CreateLeftTile(new Vector3(x, 0f, y), tile.transform);
            }
        }

        private BoardTile AddTile(int x, int y, List<BoardTile> tiles)
        {
            var position = new Vector3(x, 0f, y);
            var tile = _gameFactory.CreateBoardTile(position, _container);
            tiles.Add(tile);
            return tile;
        }
    }
}