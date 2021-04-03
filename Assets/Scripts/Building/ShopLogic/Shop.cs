using System.Collections.Generic;
using System.Linq;
using Board.Tile;
using Infrastructure.Factory;
using Player;
using UI;
using UnityEngine;

namespace Building.ShopLogic
{
    public class Shop : MonoBehaviour
    {
        public Selector Selector;
        public ShopView ShopView;
        public ShopItem[] Items;

        private Dictionary<BuildingType, int> _catalog;
        private GameFactory _gameFactory;
        private Wallet _playerWallet;
        private BoardTile _lastSelectedTile;

        public void Construct(GameFactory gameFactory, Wallet playerWallet)
        {
            _gameFactory = gameFactory;
            _playerWallet = playerWallet;
            
            _catalog = Items.ToDictionary(
                key => key.Type,
                price => price.Price);
        }

        private void OnEnable()
        {
            Selector.Selected += OnTileSelected;
            ShopView.Purchased += OnBuildPurchased;
            ShopView.Closed += OnShopClosed;
        }
        
        private void OnDisable()
        {
            Selector.Selected -= OnTileSelected;
            ShopView.Purchased -= OnBuildPurchased;
            ShopView.Closed -= OnShopClosed;
        }
        
        private void OnTileSelected(BoardTile tile)
        {
            if (tile.Content == TileContent.Bused)
            {
                ShopView.Hide();
                return;
            }

            _lastSelectedTile = tile;
            ShopView.Show();
            ShopView.Jump(to: tile);
        }
        
        private void OnBuildPurchased(BuildingType type)
        {
            var itemPrice = _catalog[type];
            
            if (_playerWallet.HasMoreThan(itemPrice))
                Purchase(type, itemPrice);
            else
                ShopView.NotEnoughView();
        }

        private void Purchase(BuildingType type, int itemPrice)
        {
            ShopView.Hide();
            _playerWallet.Take(itemPrice);
            _gameFactory.CreateBuilding(type, _lastSelectedTile);
        }

        private void OnShopClosed() =>
            ShopView.Hide();
    }
}