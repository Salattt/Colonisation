using UnityEngine;

public class ResourceFabric : MonoBehaviour
{
    [SerializeField] private Resource _blueprint;

    public Resource Get()
    {
        return Instantiate(_blueprint); 
    }
}
