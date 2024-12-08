using System;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Collector : MonoBehaviour
{
    [SerializeField] private Holder _holder;

    private Resource _target;

    public event Action<Resource> ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resource resource) && resource == _target)
        {
            ResourceCollected?.Invoke(resource);

            resource.Transfered += OnResourceTransfered;

            _holder.SetupTarget(resource);
        }
    }

    public void SetupTarget(Resource target)
    {
        _target = target;
    }

    private void OnResourceTransfered(Resource resource)
    {
        resource.Transfered -= OnResourceTransfered;

        _holder.Stop();
    }
}
