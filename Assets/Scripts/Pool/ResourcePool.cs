using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    private List<Resource> _resources = new List<Resource>();

    [SerializeField] private ResourceFabric _fabric;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public Resource Get()
    {
        Resource resource;

        if (_resources.Count == 0)
            _resources.Add(_fabric.Get());

        resource = _resources[0];

        _resources.Remove(resource);
        resource.gameObject.SetActive(true);

        resource.Destroyed += OnResorceDestroyed;

        return resource;
    }

    private void OnResorceDestroyed(Resource resource)
    {
        resource.Destroyed -= OnResorceDestroyed;

        resource.ReturnToDefault();
        _resources.Add(resource);

        resource.Transform.position = _transform.position;

        resource.gameObject.SetActive(false);
    }
}
