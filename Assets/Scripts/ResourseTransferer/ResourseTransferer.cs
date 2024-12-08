using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class ResourseTransferer : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Bot bot) && bot.TryGetResource(out Resource resource))
        {
            _warehouse.AddResource();
            resource.Transfer();
        }
    }
}
