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
        Time.timeScale = 0f;
        normalUI.SetActive(false);
        endscreen.SetActive(true);
        // change a player variable dependent on powerup hit
        if (var == EndType.Daily)
        {
            endscreen.GetComponent<MainMenu>().setDaily();
        }
        else if (var == EndType.Tower)
        {
            
            endscreen.GetComponent<MainMenu>().endScreen();
        }

            coll.enabled = false;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0f);
        
        
    }
    public void FixedUpdate()
    {
        if(player.GetComponent<PlayerController>().bumped == true)
        {
            // reset when player is bumped
            coll.enabled = true;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
        }
    }
}
