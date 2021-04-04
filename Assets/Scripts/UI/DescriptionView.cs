using System;
using Board.Tile;
using Building;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DescriptionView : MonoBehaviour
    {
        public Selector Selector;
        public GameObject DescriptionPanel;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI ValueName;
        public TextMeshProUGUI Value;
        public TextMeshProUGUI Bonus;
        public TextMeshProUGUI Price;
        
        public GameObject UpgradeMenu;
        public Button UpgradeButton;

        public CanvasFade Error;

        private GameObject _selectedObject;
        private Wallet _wallet;

        private Description _observedData;
        private bool _panelShown;
        
        public void Construct(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void OnEnable()
        {
            Selector.Selected += OnTileSelected;
            UpgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        private void OnDisable()
        {
            Selector.Selected -= OnTileSelected;
            UpgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }

        private void OnTileSelected(BoardTile tile)
        {
            if (tile.Building == null)
            {
                HidePanel();
                Unsubscribe();
                
                return;
            }
            
            ShowPanel();
            Description.text = tile.Description.Text;

            if (tile.Description.CanUpgrade)
            {
                UpgradeMenu.SetActive(true);
                _selectedObject = tile.Building;
                UpdateData(tile.Description);

                _observedData = tile.Description;
                _observedData.Changed += OnDataChanged;
            }
            else
            {
                Unsubscribe();
                UpgradeMenu.SetActive(false);
            }
        }

        private void ShowPanel()
        {
            if (_panelShown)
                return;

            _panelShown = true;
            DescriptionPanel.SetActive(true);
        }

        public void HidePanel()
        {
            if (_panelShown == false)
                return;

            _panelShown = false;
            DescriptionPanel.SetActive(false);
            Error.SetAlpha(0f);
        }

        private void Unsubscribe()
        {
            if (_observedData != null)
            {
                _observedData.Changed -= OnDataChanged;
                _observedData = null;
            }
        }

        private void OnDataChanged()
        {
            UpdateData(_observedData);
        }

        public void UpdateData(Description data)
        {
            var symbol = data.UpgradeBonus > 0 ? "+" : "";
            ValueName.text = data.ValueName;
            Value.text = data.Value.ToString("0.0");
            Bonus.text = $"{symbol}{data.UpgradeBonus:0.0}";
            Price.text = data.UpgradePrice.ToString();
        }
        
        private void OnUpgradeButtonClicked()
        {
            if (_selectedObject.TryGetComponent(out IUpgradable upgradable))
            {
                if (_wallet.HasMoreThan(upgradable.Price))
                {
                    _wallet.Take(upgradable.Price);
                    upgradable.Upgrade();
                }
                else
                {
                    Error.SetAlpha(1f);
                    Error.Hide();
                }
            }
        }
    }
}