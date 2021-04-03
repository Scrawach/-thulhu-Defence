using System;
using Board;
using Board.Tile;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private readonly RaycastHit[] _selected = new RaycastHit[1];

    private BoardTile _selectedTile;
    private Camera _camera;

    public event Action<BoardTile> Selected;

    private void Awake() => 
        _camera = Camera.main;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (EventSystem.current.IsPointerOverGameObject(-1))
            return;
        

        SelectObject();
    }

    private void SelectObject()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.RaycastNonAlloc(ray, _selected) <= 0)
            return;
        
        var selected = _selected[0].collider;

        if (selected.TryGetComponent(out BoardTile tile))
            SelectTile(tile);
    }

    private void SelectTile(BoardTile tile)
    {
        Selected?.Invoke(tile);

        if (_selectedTile != null)
            _selectedTile.Unselect();

        _selectedTile = tile;
        _selectedTile.Select();
    }
}