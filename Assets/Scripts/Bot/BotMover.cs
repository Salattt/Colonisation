using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _distanceToStop;
    [SerializeField] private float _speed;

    private Transform _transform;
    private Vector3 _target;
    private bool _isMoving;

    private void Awake()
    {
        _transform = transform;
        _distanceToStop *= _distanceToStop;
    }

    private void Update()
    {
        if (_isMoving)
            Move();

        if(_isMoving && (_target - _transform.position).sqrMagnitude < _distanceToStop)
            _isMoving = false;
    }

    public void SetupTarget(Vector3 target)
    {
        _target = target;
        _isMoving = true;
    }

    public void Stop()
    {
        _isMoving = false;
    }

    private void Move()
    {
        Vector3 direction = (_target - _transform.position).normalized;

        _transform.position += direction * _speed * Time.deltaTime;
    }
}
