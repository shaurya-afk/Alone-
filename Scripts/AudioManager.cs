using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip pointGain, grunt, jump, bomb;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        pointGain = Resources.Load<AudioClip>("PointGain");
        grunt = Resources.Load<AudioClip>("grunt");
        jump = Resources.Load<AudioClip>("jump");
        bomb = Resources.Load<AudioClip>("Bomb");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayAudio(string clip)
    {
        switch (clip)
        {
            case "point":
                audioSrc.PlayOneShot(pointGain);
                break;
            case "ugh":
                audioSrc.PlayOneShot(grunt);
                break;
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "bomb":
                audioSrc.PlayOneShot(bomb);
                break;
            default:
                break;
        }
    }
}
