using UnityEngine;

public class EndlessScore : MonoBehaviour
{

    void Update()
    {
        int floor = Mathf.RoundToInt(this.transform.position.y / 20);
        if (floor > PlayerPrefs.GetInt("BestHeight", -1))
        {
            PlayerPrefs.SetInt("BestHeight", floor);
            PlayerPrefs.Save();
        }
    }
}
