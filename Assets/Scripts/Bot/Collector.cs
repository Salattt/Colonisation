using System;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Collector : MonoBehaviour
{
    [SerializeField] private Holder _holder;

    private Resource _target;

    public event Action ResourceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resource resource) && resource == _target)
        {
            ResourceCollected?.Invoke();

            resource.Transfered += OnResourceTransfered;

            _holder.SetupTarget(resource);

            resource.PickUp();
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
