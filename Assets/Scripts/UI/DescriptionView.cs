using System;
using Board.Tile;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DescriptionView : MonoBehaviour
    {
        public Selector Selector;
        public GameObject DescriptionPanel;
        public TextMeshProUGUI Description;

        public void OnEnable() => 
            Selector.Selected += OnTileSelected;

        private void OnDisable() => 
            Selector.Selected -= OnTileSelected;

        private void OnTileSelected(BoardTile tile)
        {
            if (tile.Description == null)
            {
                DescriptionPanel.SetActive(false);
                return;
            }
            
            DescriptionPanel.SetActive(true);
            Description.text = tile.Description.Text;
        }
    }
}