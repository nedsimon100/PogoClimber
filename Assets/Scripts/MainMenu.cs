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
    string BestTimeKey = "BestTime";
    public TextMeshProUGUI bestTime;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.GetInt(BestTimeKey, -1);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt(BestTimeKey) == -1)
        {
            bestTime.text = "--:--:--";
        }
        else
        {
            bestTime.text = (Mathf.Floor((PlayerPrefs.GetInt(BestTimeKey) / 60) / 60).ToString()) + ":" + (Mathf.Floor((PlayerPrefs.GetInt(BestTimeKey) / 60) % 60).ToString()) + ":" + (Mathf.Floor(PlayerPrefs.GetInt(BestTimeKey) % 60).ToString());
        }
        
    }
    public void play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void tutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void endScreen()
    {
        if(Time.timeSinceLevelLoad < PlayerPrefs.GetInt(BestTimeKey)|| PlayerPrefs.GetInt(BestTimeKey)< 0)
        {
            
            PlayerPrefs.SetInt(BestTimeKey,Mathf.FloorToInt(Time.timeSinceLevelLoad));
            PlayerPrefs.Save();
            
        }
        bestTime.text = (Mathf.Floor((PlayerPrefs.GetInt(BestTimeKey) / 60) / 60).ToString()) + ":" + (Mathf.Floor((PlayerPrefs.GetInt(BestTimeKey) / 60) % 60).ToString()) + ":" + (Mathf.Floor(PlayerPrefs.GetInt(BestTimeKey) % 60).ToString());
        thyme.text = (Mathf.Floor((Time.timeSinceLevelLoad / 60) / 60).ToString()) + ":" + (Mathf.Floor((Time.timeSinceLevelLoad / 60) % 60).ToString()) + ":" + (Mathf.Floor(Time.timeSinceLevelLoad % 60).ToString());
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
}
