using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI thyme;
    private GameObject player;
    string BestTimeKey = "BestTimeOne";
    string EndlessKey = "BestHeight";
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI bestDailyTime;
    public TextMeshProUGUI dateDisplay;
    public TextMeshProUGUI bestEndless;
    string dailyBestTimeKey;
    public GameObject NormalUI;
    public GameObject PauseUI;
    private Inputs inputControlls;
    private void Start()
    {
        //
        //PlayerPrefs.DeleteAll();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerPrefs.GetInt(BestTimeKey, -1);
        PlayerPrefs.Save();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //inputControlls = new Inputs();
            //inputControlls.UI.Enable();
            if (PlayerPrefs.GetInt(EndlessKey, -1) == -1)
            {
                bestEndless.text = "--";
            }
            else
            {
                bestEndless.text = PlayerPrefs.GetInt(EndlessKey).ToString();
            }
            GetDaily();
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            dailyBestTimeKey = "BestTime" + FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
            if (PlayerPrefs.GetInt(dailyBestTimeKey, -1) == -1)
            {
                bestTime.text = "--:--:--";
            }
            else
            {
                bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(dailyBestTimeKey));
            }
        }
        else
        {
            if (PlayerPrefs.GetInt(BestTimeKey, -1) == -1)
            {
                bestTime.text = "--:--:--";
            }
            else
            {
                bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(BestTimeKey));
            }
        }
        
    }
    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        bestTime.text = "--:--:--";
        bestEndless.text = "--";
        bestDailyTime.text = "--:--:--";
    }

    public void setDaily()
    {
        dailyBestTimeKey = "BestTime" + FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
        if (Time.timeSinceLevelLoad < PlayerPrefs.GetInt(dailyBestTimeKey) || PlayerPrefs.GetInt(dailyBestTimeKey, -1) < 0)
        {

            PlayerPrefs.SetInt(dailyBestTimeKey, Mathf.FloorToInt(Time.timeSinceLevelLoad));
            PlayerPrefs.Save();

        }
        bestTime.text = MakeTimeReadable(PlayerPrefs.GetInt(dailyBestTimeKey));
        thyme.text = MakeTimeReadable(Time.timeSinceLevelLoad);
    }
    public void GetDaily()
    {
        dateDisplay.text = FindAnyObjectByType<ProdGenSeed>().CurrDate.ToString("dd/MM/yyyy");
        dailyBestTimeKey = "BestTime" + FindAnyObjectByType<ProdGenSeed>().Seed.ToString();
        PlayerPrefs.GetInt(dailyBestTimeKey, -1);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt(dailyBestTimeKey, -1) == -1)
        {
            bestDailyTime.text = "--:--:--";
        }
        else
        {
            bestDailyTime.text = MakeTimeReadable(PlayerPrefs.GetInt(dailyBestTimeKey));
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
        int h = Mathf.FloorToInt((score / 60) / 60);
        int m = Mathf.FloorToInt((score / 60) % 60);
        int s = Mathf.FloorToInt(score % 60);
        return (h<10?"0"+h.ToString():h.ToString()) + ":" + (m<10?"0"+m.ToString():m.ToString()) + ":" + (s<10?"0"+s.ToString():s.ToString());
    }

    //in game menu
    bool paused = false;
    Vector2 lastMousePosition;
    bool mouseIsActive = true;
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            mouseIsActive = false;
        }

        if (Mouse.current != null)
        {
            Vector2 currentMouse = Mouse.current.position.ReadValue();
            if (currentMouse != lastMousePosition)
            {
                lastMousePosition = currentMouse;
                mouseIsActive = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        if (!mouseIsActive)
        {
            if (EventSystem.current.currentSelectedGameObject == null ||
                !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
            {
                Selectable first = FindFirstActiveSelectable();
                if (first != null)
                    EventSystem.current.SetSelectedGameObject(first.gameObject);
            }
        }
    }
    Selectable FindFirstActiveSelectable()
    {
        foreach (Selectable s in Selectable.allSelectablesArray)
        {
            if (s.gameObject.activeInHierarchy && s.interactable)
                return s;
        }
        return null;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void pause()
    {
        paused = true;
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
        NormalUI.SetActive(false);
    }
    public void resume()
    {
        paused = false;
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
        NormalUI.SetActive(true);
    }
    public void menuMain()
    {
        FindAnyObjectByType<ProdGenSeed>().Reset();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void prevDay()
    {
        FindAnyObjectByType<ProdGenSeed>().PrevDay();
        GetDaily();
    }
    public void nextDay()
    {
        if (FindAnyObjectByType<ProdGenSeed>().CurrDate < System.DateTime.Today)
        {
            FindAnyObjectByType<ProdGenSeed>().NextDay();
            
            GetDaily();
        }
    }
}
