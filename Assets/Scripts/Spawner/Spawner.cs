using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _blueprint;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    protected T Spawn(Vector3 position)
    {
        return Instantiate(_blueprint, position, _transform.rotation);
    }
}
