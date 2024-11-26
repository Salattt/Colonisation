using UnityEngine;

public class MouseTrecker : MonoBehaviour
{
    [SerializeField] private int _mouseButton;

    private RaycastHit[] _hits;

    private void Update()
    {
        if ( Input.GetMouseButtonDown(_mouseButton))
        {
            _hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));

            foreach (RaycastHit hit in _hits) 
            { 
                if(hit.collider.TryGetComponent(out BuildingBuilder builder))
                {
                    builder.Activate();
                }
            }
        }
    }
}
