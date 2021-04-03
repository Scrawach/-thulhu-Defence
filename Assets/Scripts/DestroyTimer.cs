using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float Time = 2f;
        
    private void Awake() => 
        Destroy(gameObject, Time);
}