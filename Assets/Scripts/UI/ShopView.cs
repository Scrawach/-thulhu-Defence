using System;
using Board.Tile;
using Building;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopView : MonoBehaviour
    {
        public Button TowerPurchase;
        public Button TemplePurchase;
        public Button CloseButton;
        public CanvasFade ErrorLabel;

        public event Action<BuildingType> Purchased;
        public event Action Closed;

        private void OnEnable()
        {
            TowerPurchase.onClick.AddListener(OnTowerPurchased);
            TemplePurchase.onClick.AddListener(OnTemplePurchased);
            CloseButton.onClick.AddListener(OnClosedClicked);
        }
        
        private void OnDisable()
        {
            TowerPurchase.onClick.RemoveListener(OnTowerPurchased);
            TemplePurchase.onClick.RemoveListener(OnTemplePurchased);
            CloseButton.onClick.RemoveListener(OnClosedClicked);
        }

        public void NotEnoughView()
        {
            ErrorLabel.SetAlpha(1f);
            ErrorLabel.Hide();
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide()
        {
            ErrorLabel.SetAlpha(0f);
            gameObject.SetActive(false);
        }

        private void OnTowerPurchased() => 
            Purchased?.Invoke(BuildingType.Tower);

        private void OnTemplePurchased() => 
            Purchased?.Invoke(BuildingType.Temple);
        
        private void OnClosedClicked() => 
            Closed?.Invoke();

        public void Jump(BoardTile to) => 
            transform.position = to.transform.position + 3.5f * Vector3.up;
    }
}