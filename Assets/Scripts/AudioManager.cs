using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    // made with help from a brackeys tutorial on unity audio

    public Sound[] sounds;
    public static AudioManager Instance;
    public GameObject cam;
    public Sound theme;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            

            if (s.playOnAwake == true)
            {
                s.source.Play();
            }
        }

        // saving theme song seprately so i can give adaptive audio during gameplay
        theme = Array.Find(sounds, sound => sound.name == "Theme");

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    private void Update()
    {

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            // addaptive audio
            theme.source.pitch = 1 + ((cam.GetComponent<CameraController>().floor - 1)/80);
        }
        
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
