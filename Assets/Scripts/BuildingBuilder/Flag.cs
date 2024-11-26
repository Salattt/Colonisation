using System;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Flag : MonoBehaviour
{
    private Bot _searchingBot;

    public event Action<Bot> BotDetected;

    public Transform Transform {  get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bot bot) && bot == _searchingBot)
        {
            BotDetected?.Invoke(bot);
            _searchingBot = null;
            gameObject.SetActive(false);
        }
    }

    public void SetupBot(Bot bot)
    {
        _searchingBot = bot;
    }
}
