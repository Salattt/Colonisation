using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private BotMover _mover;
    [SerializeField] private Collector _collector;

    public event Action<Bot> ResourceTransfered;
    public event Action<Bot> ResourceCollected;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
    }

    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
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

    private void OnResourceCollected()
    {
        _mover.Stop();
        ResourceCollected?.Invoke(this);
    }
}
