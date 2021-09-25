using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Intro_Then_Normal_Music : MonoBehaviour
{
    private Coroutine SequentialMusic;
    public AudioSource MusicPlayer;
    public AudioClip IntroMusic;
    public AudioClip NormalMusic;

    // Start is called before the first frame update
    void Start()
    {
        SequentialMusic = StartCoroutine(IntroThenNormalMusic());
    }

    // Update is called once per frame
    void Update()
    {  

    }

    IEnumerator IntroThenNormalMusic()
    {
        yield return null;

        MusicPlayer.clip = IntroMusic;
        MusicPlayer.Play();

        while (MusicPlayer.isPlaying)
            yield return null;

        MusicPlayer.clip = NormalMusic;
        MusicPlayer.Play();
        MusicPlayer.loop = true;
    }
}
