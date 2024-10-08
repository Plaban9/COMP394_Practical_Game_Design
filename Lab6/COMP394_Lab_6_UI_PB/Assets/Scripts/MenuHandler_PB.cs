using UnityEngine;

public class MenuHandler_PB : MonoBehaviour
{
    public static MenuHandler_PB Instance
    {
        get; private set;
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
