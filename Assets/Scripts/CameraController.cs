using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public Vector3 camBounds;
    public Rigidbody2D rb;
    public Vector3 target;
    public float transitionSpeed = 1;
    [HideInInspector]
    public float floor;
    void Start()
    {
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y > target.y + camBounds.y)
        {
            target += camBounds * 2;
        }
        else if(Player.transform.position.y < target.y - camBounds.y)
        {
            target -= camBounds * 2;
        }
        floor = (transform.position.y + 20) / 20;
    }
    private void FixedUpdate()
    {
        rb.velocity = (target - this.transform.position)*transitionSpeed;
    }
}
