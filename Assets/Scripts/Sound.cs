using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;

    public bool playOnAwake;
    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
