using System;
using UnityEngine;

namespace Board.Tile
{
    public class BoardTile : MonoBehaviour
    {
        public TileContent Content;
        public Description Description;
        
        public event Action<BoardTile> Selected;
        public event Action<BoardTile> Unselected;
        
        public void Select() => 
            Selected?.Invoke(this);

        public void Unselect() => 
            Unselected?.Invoke(this);
    }
}