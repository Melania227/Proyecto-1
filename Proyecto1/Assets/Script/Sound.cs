using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class Sound : MonoBehaviour
{
    public AudioSource fxSound; 
    public AudioClip backMusic;
    public Toggle soundOn;

    // Start is called before the first frame update
    void Start()
    {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fxSound.isPlaying && soundOn.isOn)
        {
            fxSound.Play();
        }
        else if (fxSound.isPlaying && !soundOn.isOn)
        {
            fxSound.Stop();
        }
    }
}
