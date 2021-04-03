using UnityEngine;

namespace Building
{
    public class LaserBeam : MonoBehaviour
    {
        public LineRenderer LineRenderer = default;
        public GameObject Laser;

        public void Shoot(Vector3 point)
        {
            LineRenderer.SetPosition(1, point);
        }

        public void Deactivate()
        {
            Laser.SetActive(false);
        }

        public void Activate()
        {
            Laser.SetActive(true);
        }
    }
}