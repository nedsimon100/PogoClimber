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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
