using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider
    {
        public GameObject Initialize(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Initialize(string path, Vector3 at, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity, parent);
        }
    }
}