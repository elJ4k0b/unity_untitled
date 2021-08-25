using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform position;
    private Vector2 direction;
    public float speed { get; private set; } = 2;
    void Start()
    {
        direction = new Vector2(0, 1);
        Vector3 startposition = new Vector3(0, 0, -10);
        position.position = startposition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
