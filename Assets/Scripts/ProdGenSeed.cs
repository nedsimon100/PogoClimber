using System;
using TMPro;
using UnityEngine;

public class ProdGenSeed : MonoBehaviour
{
    public DateTime CurrDate;
    public int Seed;

    void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        CurrDate = System.DateTime.Today;
        Seed = CurrDate.GetHashCode();

    }
    public void NextDay()
    {
        CurrDate = CurrDate.AddDays(1);
        Seed = CurrDate.GetHashCode();
    }

    public void PrevDay()
    {
        CurrDate = CurrDate.AddDays(-1);
        Seed = CurrDate.GetHashCode();
    }
}
