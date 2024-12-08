using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] private float ScaleMultiplier;

    private Resource _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetupTarget(Resource target)
    {
        _target = target;
        _target.Transform.rotation = _transform.rotation;
        _target.Transform.localScale *= ScaleMultiplier;

        _target.transform.SetParent(_transform);

        _target.transform.localPosition = Vector3.zero;
    }

    public void Stop()
    {
        _target.transform.parent = null;
    }
}
