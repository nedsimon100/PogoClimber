using UnityEngine;

public class waveFunctCollapseManager : MonoBehaviour
{
    public int seed = 0;
    public bool Ready = false;
    public enum seedMode 
    { 
    Seeded,
    Random,
    chosen
    }
    public seedMode mode;
    void Awake()
    {
        switch (mode)
        {
            case seedMode.Seeded:
                seed = FindAnyObjectByType<ProdGenSeed>().Seed;
                break;
            case seedMode.Random:
                seed = Random.Range(int.MinValue, int.MaxValue);
                break;
            case seedMode.chosen:
                break;
        }

        Ready = true;
    }

    
    void Update()
    {
        
    }
}
