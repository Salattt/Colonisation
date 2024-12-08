using System;
using UnityEngine;

public class FlagSearcher : MonoBehaviour
{
    public event Action FlagDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Flag flag))
        {
            FlagDetected?.Invoke();
        }
    }
}
