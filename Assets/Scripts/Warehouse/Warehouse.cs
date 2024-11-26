using System;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    public event Action ResourceAdded;

    public int ResourceQuantity {  get; private set; } = 0;

    public void AddResource()
    {
        ResourceQuantity++;

        ResourceAdded?.Invoke();
    }

    public bool TryGetResource(int quantity)
    {
        if (ResourceQuantity >= quantity) 
        { 
            ResourceQuantity -= quantity;
            return true;
        }

        return false;
    }
}
