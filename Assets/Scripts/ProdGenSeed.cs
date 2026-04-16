using TMPro;
using UnityEngine;

public class ProdGenSeed : MonoBehaviour
{
    public int CurrDate;
    public int Seed;

    void Start()
    {
        CurrDate = (10000*System.DateTime.Now.Year) + System.DateTime.Now.DayOfYear;
        Seed = CurrDate;

        if(FindAnyObjectByType<MainMenu>() != null)
        {
            FindAnyObjectByType<MainMenu>().dateDisplay.text = Seed.ToString();
        }
        
    }

}
