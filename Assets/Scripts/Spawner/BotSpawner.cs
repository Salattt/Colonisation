using UnityEngine;

public class BotSpawner : Spawner<Bot>
{
    public Bot SpawnBot(Vector3 position)
    {
        return Spawn(position);
    }
}
