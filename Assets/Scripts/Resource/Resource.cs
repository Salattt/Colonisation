using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private Vector3 _startScale;

    public event Action<Resource> Transfered;
    public event Action<Resource> Destroyed;

    public Transform Transform {  get; private set; }

    private void Awake()
    {
        Transform = transform;
        _startScale = Transform.localScale;
    }

    public void ReturnToDefault()
    {
        Transform.localScale = _startScale;
    }

    public void Transfer()
    {
        Transfered?.Invoke(this);
        Destroyed?.Invoke(this);
    }
}
