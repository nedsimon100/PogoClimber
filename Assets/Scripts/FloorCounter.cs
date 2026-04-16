using TMPro;
using UnityEngine;

public class FloorCounter : MonoBehaviour
{
    public TextMeshProUGUI floorCtr;
    void Start()
    {
        
    }


    void Update()
    {
        int floor = Mathf.RoundToInt(this.transform.position.y / 20);
        floorCtr.text = floor.ToString();
    }
}
