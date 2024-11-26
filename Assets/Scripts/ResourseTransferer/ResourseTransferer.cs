using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class ResourseTransferer : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Resource resource) && resource.IsPickedUp)
        {
            _warehouse.AddResource();
            resource.Transfer();
        }
    }
}
