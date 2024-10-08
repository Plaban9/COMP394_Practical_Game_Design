using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using static UnityEditor.PlayerSettings;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab;
    // Speed at which the AppleTree moves
    public float speed = 1f;
    // Distance where AppleTree turns around
    public float leftEdge = -10f;
    public float rightEdge = 10f;
    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;
    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    private Vector3 _currentPosition;

    void Start()
    {
        // Dropping apples every second
        // Dropping apples every second
        Invoke(nameof(DropApple), 2f);
    }

    void Update()
    {
        // Basic Movement
        BasicMovement();
        // Changing Direction
        CheckBounds();
    }

    private void FixedUpdate()
    {
        ChangeDirection();
    }

    private void CheckBounds()
    {
        if (_currentPosition.x < leftEdge)
        { // a
            speed = Mathf.Abs(speed); // Move right // b
        }
        else if (_currentPosition.x > rightEdge)
        { // c
            speed = -Mathf.Abs(speed); // Move left // c
        }
    }

    private void BasicMovement()
    {
        _currentPosition = transform.position; // b
        _currentPosition.x += speed * Time.deltaTime; // c
        transform.position = _currentPosition; // d
    }

    private void ChangeDirection()
    {
        if (Random.value < chanceToChangeDirections)
        { // a
            speed *= -1; // Change direction // b
        }
    }

    void DropApple()
    { // b
        GameObject apple = Instantiate<GameObject>(applePrefab); // c
        apple.transform.position = transform.position; // d
        Invoke(nameof(DropApple), secondsBetweenAppleDrops); // e
    }
}
