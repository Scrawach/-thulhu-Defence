using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _money;

        [SerializeField] 
        private WalletUpdate _walletUpdate;

        private Wallet _wallet;

        private int _prevValue;

        public void Construct(Wallet wallet)
        {
            if (_wallet != null)
                _wallet.Changed -= OnMoneyChanged;
            
            _wallet = wallet;
            _wallet.Changed += OnMoneyChanged;
            OnMoneyChanged(_wallet.Money);
        }

        private void OnMoneyChanged(int value)
        {
            _money.text = value.ToString();
            _walletUpdate.UpdateData(value - _prevValue);
            _prevValue = value;
        }
    }
}