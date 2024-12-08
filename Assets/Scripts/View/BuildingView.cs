using TMPro;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _building.ResourceAdded += OnResourceAdded;
    }

    private void OnDisable()
    {
        _building.ResourceAdded -= OnResourceAdded;
    }

    private void OnResourceAdded(int quantity)
    {
        _text.text = quantity.ToString();
    }
}
