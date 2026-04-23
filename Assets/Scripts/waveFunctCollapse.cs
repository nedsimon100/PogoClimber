using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waveFunctCollapse : MonoBehaviour
{
    [Serialize]
    public List<GameObject> spawnableObjects = new List<GameObject>();
    private float maxSpawnDist=100;
    public bool repeating;
    public bool reflective;
    private int iterations;
    public Vector2 minMaxIterations = new Vector2(0,5);
    public int maxHeight;
    public GameObject finalRoom;
 //   waveFunctCollapseManager manager;
    Transform player;
    void Update()
    {
        waveFunctCollapseManager wfcm = FindAnyObjectByType<waveFunctCollapseManager>();
        
        if (wfcm.Ready)
        {

            player = FindAnyObjectByType<PlayerTag>().transform;


            if (maxHeight > 0 && maxHeight < Mathf.RoundToInt(this.transform.position.y / 20))
            {
                Instantiate(finalRoom, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else if ((transform.position.y) - player.position.y < maxSpawnDist)
            {
                if(wfcm.mode == waveFunctCollapseManager.seedMode.Seeded)
                {
                    Random.InitState(wfcm.seed + Mathf.RoundToInt(transform.position.y) - Mathf.RoundToInt(transform.position.x));
                    
                }
                iterations = Random.Range(Mathf.RoundToInt(minMaxIterations.x), Mathf.RoundToInt(minMaxIterations.y));
                if (repeating)
                {
                    Instantiate(this.gameObject, transform.position + new Vector3(0, transform.localScale.y, 0), transform.rotation);
                    Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Count)], transform.position, transform.rotation);
                }
                else
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        GameObject go = Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Count)], transform.position, transform.rotation);

                        if (reflective)
                        {
                            GameObject reGO = Instantiate(go.gameObject, new Vector3(-go.transform.position.x, go.transform.position.y, 0), go.transform.rotation);
                            reGO.transform.localScale = new Vector3(-reGO.transform.localScale.x, reGO.transform.localScale.y, reGO.transform.localScale.z);
                        }
                    }
                }

                Destroy(this.gameObject);
            }
        }
        

    }
}
