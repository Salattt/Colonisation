using System.Collections;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _distanceToStop;
    [SerializeField] private float _speed;

    private Transform _transform;
    private Vector3 _target;
    private Coroutine _movingRoutine;

    private void Awake()
    {
        _transform = transform;
        _distanceToStop *= _distanceToStop;
    }

    public void SetupTarget(Vector3 target)
    {
        _target = target;

        if(_movingRoutine != null)
            StopCoroutine(_movingRoutine);

        _movingRoutine = StartCoroutine(MoveRoutine());
    }

    public void Stop()
    {
        StopCoroutine(_movingRoutine);
    }

    private void Move()
    {
        _transform.position += (_target - _transform.position).normalized * _speed * Time.fixedDeltaTime;
    }

    private IEnumerator MoveRoutine()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while (true) 
        {
            Move();

            if ((_target - _transform.position).sqrMagnitude < _distanceToStop)
                Stop();

            yield return waitForFixedUpdate;
        }
    }
}
