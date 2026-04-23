using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrig : MonoBehaviour
{
    public CircleCollider2D coll;
    public SpriteRenderer rend;
    private GameObject player;
    public enum EndType {Tower, Daily }
    public EndType var;
    public GameObject endscreen;
    public GameObject normalUI;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            pow();
        }
    }
    

    public void pow()
    {
       // Time.timeScale = 0f;
        Destroy(player.GetComponent<PlayerController>());
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        // change a player variable dependent on powerup hit
        if (var == EndType.Daily)
        {
            FindAnyObjectByType<MainMenu>().setDaily();
        }
        else if (var == EndType.Tower)
        {

            FindAnyObjectByType<MainMenu>().endScreen();
        }

            coll.enabled = false;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0f);

        Destroy(this);
    }
    
}
