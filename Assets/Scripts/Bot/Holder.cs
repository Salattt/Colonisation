using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private float ScaleMultiplier;

    private Resource _target;
    private Transform _transform;

    private bool _isActive = false;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(_isActive)
            _target.Transform.position = transform.position;
    }

    public void SetupTarget(Resource target)
    {
        _target = target;
        _target.Transform.rotation = _transform.rotation;
        _target.Transform.localScale *= ScaleMultiplier;
        _isActive = true;
    }

    public void Stop()
    {
        _isActive = false;
    }
}
