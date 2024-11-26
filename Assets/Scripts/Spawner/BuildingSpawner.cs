using UnityEngine;

public class BuildingSpawner : Spawner<Building>
{
    public void SpawnBuilding(Bot bot, Vector3 position)
    {
        Spawn(position).AddBot(bot);
    }
}
