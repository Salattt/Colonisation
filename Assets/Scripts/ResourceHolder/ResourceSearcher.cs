using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ResourceSearcher : MonoBehaviour
{
    [SerializeField] private ResourceHolder _holder;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resource resource))
        {
            _holder.Add(resource);
        }
    }
}
