using System;

namespace Board.Tile
{
    [Serializable]
    public class Description
    {
        public string Text;
        public bool CanUpgrade;
        public int UpgradePrice;
        public float UpgradeBonus;
        public string ValueName;
        public float Value;

        public event Action Changed;
        
        public void Update()
        {
            Changed?.Invoke();
        }
    }
}