using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletUpdate : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _positive;
        
        [SerializeField] 
        private TextMeshProUGUI _negative;

        [SerializeField]
        private RectTransform _spawnPosition;

        public void UpdateData(int value)
        {
            if (value < 0)
            {
                var ui = Instantiate(_negative, _spawnPosition.position, Quaternion.identity, transform);
                ui.text = $"{value}";
            }
            else
            {
                var ui = Instantiate(_positive, _spawnPosition.position, Quaternion.identity, transform);
                ui.text = $"+{value}";
            }
        }
    }
}