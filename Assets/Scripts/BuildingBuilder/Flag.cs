using UnityEngine;

public class Flag : MonoBehaviour
{
    public Transform Transform {  get; private set; }

    private void Awake()
    {
        Transform = transform;
    }
}
