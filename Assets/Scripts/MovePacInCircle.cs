using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePacInCircle : MonoBehaviour
{
    private float x1 = -6.5f;
    private float x2 = -1.5f;
    private float y1 = 6.5f;
    private float y2 = 2.5f;

    public AudioSource MusicPlayer;
    public AudioClip noCollisionSound;

    private float lerpFactor = 0.0f;
    private int counter = 0;
    private Vector3[] locations;

    [SerializeField]
    private GameObject fatMan;

    public float Speed { get; private set; }


    void Start()
    {
        locations = new Vector3[4];
        locations[0] = new Vector3(x1, y1, 0.0f); 
        locations[1] = new Vector3(x2, y1, 0.0f);
        locations[2] = new Vector3(x2, y2, 0.0f);
        locations[3] = new Vector3(x1, y2, 0.0f);
        Speed = 1.0f;
        MusicPlayer.clip = noCollisionSound;
    }

    
    private void FixedUpdate()
    {
        lerpFactor += Time.deltaTime * Speed;

        if (lerpFactor > 1.0)
        {
            while (lerpFactor > 1.0)
            {
                lerpFactor--;
                counter++;
            }

            if (counter + 1 > locations.Length)
            {
                lerpFactor = 0.0f;
                counter = 0;
            }
        }
        if (counter == 3)
        {
            fatMan.transform.position = Vector3.Lerp(locations[3], locations[0], lerpFactor);
            if (MusicPlayer.isPlaying == false)
                MusicPlayer.Play();
        }
        else
        {
            fatMan.transform.position = Vector3.Lerp(locations[counter], locations[counter + 1], lerpFactor);
            if (MusicPlayer.isPlaying == false)
                MusicPlayer.Play();
        }
            
    }
}
