using System;
using UnityEngine;

public class BuilderMouseTrecker : MonoBehaviour
{
    [SerializeField] private int _mouseButton;

    private RaycastHit[] _hits;

    private bool _isActive = false;

    public event Action<Vector3> Pressed;

    private void Update()
    {
        if(_isActive && Input.GetMouseButtonDown(_mouseButton))
        {
            _isActive = false;

            _hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));

            foreach(RaycastHit hitInfo in _hits)
            {
                if (hitInfo.collider.TryGetComponent<Ground>(out _))
                {
                    Pressed?.Invoke(hitInfo.point);
                }
            }
        }
    }

    public void Treck()
    {
        _isActive = true;
    }
}
