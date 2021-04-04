using UnityEngine;

namespace Building
{
    public class TowerDistanceView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _plane;
        
        public void Show() => 
            _plane.SetActive(true);

        public void Hide() => 
            _plane.SetActive(false);
    }
}