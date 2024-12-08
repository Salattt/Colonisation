using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingBuilder _builder;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private BotHandler _botHandler;
    [SerializeField] private BotSpawner _botSpawner;
    [SerializeField] private int _resourceToSpawnBot;
    [SerializeField] private int _resourceToSpawnBuilding;
    [SerializeField] private int _botToSpawnBuilding;

    private Transform _transform;

    private bool _isBuilderActive = false;
    private bool _isBotRequestSended = false;

    public event Action<int> ResourceAdded;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _warehouse.ResourceAdded += OnResourceAdded;
        _builder.BuildingSpawned += OnBuildingSpawned;
        _builder.FlagPlaced += OnFlagPlaced;
        _botHandler.RequestComplitted += OnRequestComplitted;

        ResourceAdded?.Invoke(_warehouse.ResourceQuantity);
    }

    private void OnDisable()
    {
        _warehouse.ResourceAdded -= OnResourceAdded;
        _builder.BuildingSpawned -= OnBuildingSpawned;
        _builder.FlagPlaced -= OnFlagPlaced;
        _botHandler.RequestComplitted -= OnRequestComplitted;
    }

    public void AddBot(Bot bot)
    {
        _botHandler.Add(bot);
    }

    private void OnResourceAdded()
    {
        if (_isBuilderActive && _botHandler.Quantity >= _botToSpawnBuilding && _isBotRequestSended == false)
        {
            if(_warehouse.TryGetResource(_resourceToSpawnBuilding))
            {
                _botHandler.LeaveRequestForBot();

                _isBotRequestSended = true;
            }

            ResourceAdded?.Invoke(_warehouse.ResourceQuantity);

            return;
        }

        if (_warehouse.TryGetResource(_resourceToSpawnBot))
            _botHandler.Add(_botSpawner.Spawn(_transform.position));

        ResourceAdded?.Invoke(_warehouse.ResourceQuantity);
    }

    private void OnFlagPlaced()
    {
        _isBuilderActive = true;

        OnResourceAdded();
    }

    private void OnBuildingSpawned()
    {
        _isBuilderActive = false;
        _isBotRequestSended = false;
    }

    private void OnRequestComplitted(Bot bot)
    {
        _builder.Build(bot);
    }
}
