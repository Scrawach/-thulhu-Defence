using System;
using Infrastructure.Factory;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    public class MoneyDrop : MonoBehaviour
    {
        [SerializeField] 
        private Vector2Int _valueRange;

        [SerializeField] 
        private GameObject _destroyEffect;

        private Wallet _wallet;

        public int Profit { get; private set; }

        public void Construct(GameFactory gameFactory) => 
            _wallet = gameFactory.Wallet;

        public void Take()
        {
            _wallet.Add(Profit);
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void Awake() => 
            Profit = Random.Range(_valueRange.x, _valueRange.y);

        private void OnMouseEnter() => 
            Take();
    }
}