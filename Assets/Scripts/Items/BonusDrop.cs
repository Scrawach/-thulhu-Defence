using Infrastructure.Factory;
using Player;
using UnityEngine;

namespace Items
{
    public class BonusDrop : MonoBehaviour
    {
        [SerializeField] 
        private Vector2Int _valueRange;

        [SerializeField] 
        private GameObject _destroyEffect;

        private Score _score;

        public int Profit { get; private set; }

        public void Construct(GameFactory gameFactory) => 
            _score = gameFactory.Score;

        public void Take()
        {
            _score.Add(Profit);
            Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void Awake() => 
            Profit = Random.Range(_valueRange.x, _valueRange.y);

        private void OnMouseEnter() => 
            Take();
    }
}