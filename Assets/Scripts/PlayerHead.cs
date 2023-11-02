using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{

    public GameObject Player;

    private void Start()
    {
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Player.GetComponent<PlayerController>().StartCoroutine("bump");
    }
}
