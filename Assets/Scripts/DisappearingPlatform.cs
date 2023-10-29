using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{

    public BoxCollider2D coll;
    public SpriteRenderer rend;
    public float timer = 3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("disappear");
    }

    IEnumerator disappear()
    {
        coll.enabled = false;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0.2f);
        yield return new WaitForSeconds(timer);
        coll.enabled = true;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
    }

}
