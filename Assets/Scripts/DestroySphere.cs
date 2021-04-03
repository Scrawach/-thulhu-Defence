using System;
using System.Collections;
using EnemyLogic;
using UnityEngine;

public class DestroySphere : MonoBehaviour
{
    [SerializeField] private float _riseSpeed;
    
    private void Awake()
    {
        StartCoroutine(Scaling());
    }

    private IEnumerator Scaling()
    {
        var scale = 0f;
        while (scale <= 25f)
        {
            scale += _riseSpeed * Time.deltaTime;
            transform.localScale = Vector3.one * scale;
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TargetPoint targetPoint))
        {
            targetPoint.Health.ApplyDamage(10000);
        }
    }
}