using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    private List<Resource> _resources = new List<Resource>();

    public void Add(Resource resource)
    {
        _resources.Add(resource); 
    }

    public bool IsResourceAvaible => _resources.Count > 0;

    public bool TryGetClosestToPoint(Vector3 point, out Resource closestResource)
    {
        closestResource = null;

        if(_resources.Count == 0)
            return false;

        closestResource = _resources[0];

        foreach (Resource resource in _resources) 
        { 
            if((resource.Transform.position - point).sqrMagnitude < (closestResource.Transform.position - point).sqrMagnitude)
                closestResource = resource;
        }

        _resources.Remove(closestResource);

        return true;
    }
}
