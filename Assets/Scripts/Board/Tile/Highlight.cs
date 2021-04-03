using UnityEngine;

namespace Board.Tile
{
    [RequireComponent(typeof(BoardTile))]
    public class Highlight : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _highlightMaterial;
        [SerializeField] private Material _selectedMaterial;
        
        private Material _originalMaterial;
        private BoardTile _tile;
        
        private bool _isSelected;
        
        private void Awake()
        {
            _tile = GetComponent<BoardTile>();
            _originalMaterial = _meshRenderer.material;
        }

        private void OnEnable()
        {
            _tile.Selected += OnTileSelected;
            _tile.Unselected += OnTileUnselected;
        }
        
        private void OnDisable()
        {
            _tile.Selected -= OnTileSelected;
            _tile.Unselected -= OnTileUnselected;
        }

        private void OnTileUnselected(BoardTile tile)
        {
            _isSelected = false;
            Enable(false);
        }
        
        private void OnTileSelected(BoardTile tile)
        {
            _isSelected = true;
            _meshRenderer.material = _selectedMaterial;
        }

        private void Enable(bool select)
        {
            if (_isSelected)
                return;

            _meshRenderer.material = select ? _highlightMaterial : _originalMaterial;
        }

        private void OnMouseEnter() => 
            Enable(true);

        private void OnMouseExit() => 
            Enable(false);
    }
}