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
        _flag.BotDetected += OnBotDetected;
    }

    private void OnDisable()
    {
        _mouseTrecker.Pressed -= PlaceFlag;
        _flag.BotDetected -= OnBotDetected;
    }

    public void Activate()
    {
        _mouseTrecker.Treck();
    }

    public void Build(Bot bot)
    {
        bot.GoToPoint(_flag.Transform.position);
        _flag.SetupBot(bot);
    }

    private void PlaceFlag(Vector3 position)
    {
        _flag.gameObject.SetActive(true);

        _flag.Transform.position = position;

        FlagPlaced?.Invoke();
    }

    private void OnBotDetected(Bot bot)
    {
        _buildingSpawner.SpawnBuilding(bot, _flag.Transform.position);
        BuildingSpawned?.Invoke();
    }
}
