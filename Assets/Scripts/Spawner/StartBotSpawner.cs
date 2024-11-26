using UnityEngine;

public class StartBotSpawner : BotSpawner
{
    [SerializeField] private Building _building;

    private void OnEnable()
    {
        _building.AddBot(SpawnBot(transform.position));
    }
}