using UnityEngine;

public class waveFunctCollapseManager : MonoBehaviour
{
    public int seed = 0;
    bool repeating = false;
    public enum seedMode 
    { 
    Daily,
    Random,
    chosen
    }
    public seedMode mode;
    void Start()
    {
        switch (mode)
        {
            case seedMode.Daily:
                seed = System.DateTime.Now.Year + System.DateTime.Now.DayOfYear;
                break;
            case seedMode.Random:
                seed = Random.Range(int.MinValue, int.MaxValue);
                break;
            case seedMode.chosen:
                break;
        }
        Random.InitState(seed);
    }

    
    void Update()
    {
        
    }
}
