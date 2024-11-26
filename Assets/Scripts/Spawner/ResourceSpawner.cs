using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _periodecallySpawnTime;
    [SerializeField] private int _periodicallyResourceToSpawn;
    [SerializeField] private int _startResourceToSpawn;
    [SerializeField] private ResourcePool _pool;

    private BoxCollider _collider;
    private Transform _transform;
    private float _timer = 0;

    private void Awake()
    {
        _transform = transform;
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        SpawnMany(_startResourceToSpawn);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _periodecallySpawnTime)
        {
            SpawnMany(_periodicallyResourceToSpawn);
            _timer = 0;
        }
    }

    private void SpawnMany(int quantity)
    {
        for (int i = 0; i < quantity; i++) 
        {
            _pool.Get().Transform.position = GetRandomSpawnPoint();
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(_transform.position.x + Random.Range(-_collider.size.x, _collider.size.x) / 2, _transform.position.y,
            _transform.position.z + Random.Range(-_collider.size.z, _collider.size.z) / 2);
    }
}
