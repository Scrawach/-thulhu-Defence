using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _money;

        private Wallet _wallet;

        public void Construct(Wallet wallet)
        {
            if (_wallet != null)
                _wallet.Changed -= OnMoneyChanged;
            
            _wallet = wallet;
            _wallet.Changed += OnMoneyChanged;
            OnMoneyChanged(_wallet.Money);
        }

        private void OnMoneyChanged(int value) => 
            _money.text = value.ToString();
    }
}