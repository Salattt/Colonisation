using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private BotMover _mover;
    [SerializeField] private Collector _collector;
    [SerializeField] private FlagSearcher _flagSearcher;

    private Resource _currentResource;

    public event Action<Bot> ResourceTransfered;
    public event Action<Bot> ResourceCollected;
    public event Action<Bot> FlagDetected;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _flagSearcher.FlagDetected += OnFlagDetected;
    }

    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _flagSearcher.FlagDetected -= OnFlagDetected;
    }

    public bool TryGetResource(out Resource resource)
    {
        resource = null;

        if (_currentResource != null)
        {
            resource = _currentResource;
            _currentResource = null;

            return true;
        }

        return false;
    }

    public void SetupTarget(Resource target)
    {
        _collector.SetupTarget(target);
        _mover.SetupTarget(target.Transform.position);

        target.Transfered += OnResourceTransfered;
    }

    public void GoToPoint(Vector3 point)
    {
        _mover.SetupTarget(point);    
    }

    private void OnResourceTransfered(Resource resource)
    {
        resource.Transfered -= OnResourceTransfered;
        
        _mover.Stop();
        ResourceTransfered?.Invoke(this);
    }

    private void OnResourceCollected(Resource resource)
    {
        _currentResource = resource;

        _mover.Stop();
        ResourceCollected?.Invoke(this);
    }

    private void OnFlagDetected()
    {
        FlagDetected?.Invoke(this);
    }
}
