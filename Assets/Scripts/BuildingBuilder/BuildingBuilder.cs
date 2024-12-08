using System;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    [SerializeField] private Flag _flag;
    [SerializeField] private BuilderMouseTrecker _mouseTrecker;
    [SerializeField] private BuildingSpawner _buildingSpawner;

    public event Action FlagPlaced;
    public event Action BuildingSpawned;

    private void OnEnable()
    {
        _mouseTrecker.Pressed += PlaceFlag;
    }

    private void OnDisable()
    {
        _mouseTrecker.Pressed -= PlaceFlag;
    }

    public void Activate()
    {
        _mouseTrecker.Treck();
    }

    public void Build(Bot bot)
    {
        bot.FlagDetected += OnBotDetectFlag;

        bot.GoToPoint(_flag.Transform.position);
    }

    private void PlaceFlag(Vector3 position)
    {
        _flag.gameObject.SetActive(true);

        _flag.Transform.position = position;

        FlagPlaced?.Invoke();
    }

    private void OnBotDetectFlag(Bot bot)
    {
        bot.FlagDetected -= OnBotDetectFlag;

        _buildingSpawner.SpawnBuilding(bot, _flag.Transform.position);
        BuildingSpawned?.Invoke();
    }
}
