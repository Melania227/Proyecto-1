using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sound : MonoBehaviour
{ 
    AudioSource fxSound; // Emitir sons
    public AudioClip backMusic; // Som de fundo

    // Start is called before the first frame update
    void Start()
    {
        // Audio Source responsavel por emitir os sons
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (!fxSound.isPlaying)
        {
            fxSound.Play();
        }
    }
}
