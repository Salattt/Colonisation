using System;
using System.Collections.Generic;
using UnityEngine;

public class BotHandler : MonoBehaviour
{
    private List<Bot> _bots = new List<Bot>();
    private List<Bot> _unactiveBots = new List<Bot>();

    [SerializeField] private ResourceHolder _resourceHolder;
    [SerializeField] private Transform _pointToTransferResource;
    [SerializeField] private float _timeToUpdateUnactiveBots;

    private float _timer = 0;
    private bool _isRequestActive = false;

    public event Action<Bot> RequestComplitted;

    public int Quantity => _bots.Count;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer >= _timeToUpdateUnactiveBots)
            UpdateUnactiveBots();
    }

    public void Add(Bot bot)
    {
        _bots.Add(bot);
        _unactiveBots.Add(bot);

        bot.ResourceCollected += OnBotCollectedResource;
        bot.ResourceTransfered += OnBotTransferedResorce;
    }

    public void LeaveRequestForBot()
    {
        if(_unactiveBots.Count > 0)
        {
            Bot bot = _unactiveBots[0];

            _unactiveBots.Remove(bot);
            CompliteRequest(bot);

            return;
        }

        _isRequestActive= true;
    }

    private void CompliteRequest(Bot bot)
    {
        _bots.Remove(bot);

        bot.ResourceCollected -= OnBotCollectedResource;
        bot.ResourceTransfered -= OnBotTransferedResorce;

        RequestComplitted?.Invoke(bot);
    }

    private void UpdateUnactiveBots()
    {
        Bot bot;

        while(_resourceHolder.IsResourceAvaible && _unactiveBots.Count > 0)
        {
            bot = _unactiveBots[0];

            _unactiveBots.Remove(bot);
            OnBotTransferedResorce(bot);
        }
    }

    private void OnBotCollectedResource(Bot bot)
    {
        bot.GoToPoint(_pointToTransferResource.position);
    }

    private void OnBotTransferedResorce(Bot bot)
    {
        if (_isRequestActive)
        {
            _isRequestActive = false;

            CompliteRequest(bot);

            return;
        }

        if (_resourceHolder.TryGetClosestToPoint(bot.Transform.position, out Resource closestResource))
        {
            bot.SetupTarget(closestResource);
        }
        else
        {
            _unactiveBots.Add(bot);
        }
    }
}
