using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopIntroMusic : MonoBehaviour
{
    private Coroutine SequentialMusic;
    public AudioSource MusicPlayer;
    public AudioClip IntroMusic;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer.clip = IntroMusic; ;
        MusicPlayer.Play();
        MusicPlayer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
