using System;
using Building;
using UnityEngine;
using UnityEngine.Serialization;

namespace Board.Tile
{
    public class BoardTile : MonoBehaviour
    {
        public TileContent Content;
        public Description Description;
        public GameObject Building;
        
        public event Action<BoardTile> Selected;
        public event Action<BoardTile> Unselected;

        public void SetBuilding(GameObject building)
        {
            Building = building;

            if (building.TryGetComponent(out BuildingDescription description))
                Description = description.Description;
        }

        public void Select() => 
            Selected?.Invoke(this);

        public void Unselect() => 
            Unselected?.Invoke(this);
    }
}