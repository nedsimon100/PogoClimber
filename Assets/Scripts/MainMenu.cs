using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI thyme;
    public TextMeshProUGUI bouncescore;
    public TextMeshProUGUI gravscore;
    private GameObject player;
    string BestTimeKey = "BestTimeOne";
    string EndlessKey = "BestHeight";
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI dateDisplay;
    string dailyBestTimeKey;
    private void Start()
    {
        //
        //PlayerPrefs.DeleteAll();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.GetInt(BestTimeKey, -1);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt(BestTimeKey,-1) == -1)
        {
            bestTime.text = "--:--:--";
        }
        else
        {
            bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(BestTimeKey));
        }
            
        
    }
    public void GetDaily()
    {
        dateDisplay.text = FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
        dailyBestTimeKey = "BestTime" + FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
        PlayerPrefs.GetInt(dailyBestTimeKey, -1);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt(dailyBestTimeKey, -1) == -1)
        {
            bestTime.text = "--:--:--";
        }
        else
        {
            bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(dailyBestTimeKey));
        }
    }
    public void changeMusicVolume(float vol)
    {
        FindAnyObjectByType<AudioManager>().theme.source.volume = vol;
    }
    public void changeSFXVolume(float vol)
    {
        FindAnyObjectByType<AudioManager>().changeMusicVolume(vol, "Bounce");
        FindAnyObjectByType<AudioManager>().changeMusicVolume(vol, "oof");
    }
    public void play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void playEndless()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
    public void playSeeded()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }
    public void tutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void endScreen()
    {
        if(Time.timeSinceLevelLoad < PlayerPrefs.GetInt(BestTimeKey)|| PlayerPrefs.GetInt(BestTimeKey,-1)< 0)
        {
            
            PlayerPrefs.SetInt(BestTimeKey,Mathf.FloorToInt(Time.timeSinceLevelLoad));
            PlayerPrefs.Save();
            
        }
        bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(BestTimeKey));
        thyme.text = MakeTimeReadable(Time.timeSinceLevelLoad);
    }

    public string MakeTimeReadable(float score)
    {
        return (Mathf.Floor((score / 60) / 60).ToString()) + ":" + (Mathf.Floor((score / 60) % 60).ToString()) + ":" + (Mathf.Floor(score % 60).ToString());
    }

    public void SeededEndScreen()
    {
        if (Time.timeSinceLevelLoad < PlayerPrefs.GetInt(BestTimeKey) || PlayerPrefs.GetInt(BestTimeKey, -1) < 0)
        {

            PlayerPrefs.SetInt(BestTimeKey, Mathf.FloorToInt(Time.timeSinceLevelLoad));
            PlayerPrefs.Save();

        }
        bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(BestTimeKey));
        thyme.text = MakeTimeReadable(Time.timeSinceLevelLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void pause()
    {
        Time.timeScale = 0f;
    }
    public void resume()
    {
        Time.timeScale = 1f;
    }
    public void menuMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void prevDay()
    {
        FindAnyObjectByType<ProdGenSeed>().Seed -= 1;
        dateDisplay.text = FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
    }
    public void nextDay()
    {
        if (FindAnyObjectByType<ProdGenSeed>().Seed < FindAnyObjectByType<ProdGenSeed>().CurrDate)
        {
            FindAnyObjectByType<ProdGenSeed>().Seed += 1;
            GetDaily();
        }
    }
}
