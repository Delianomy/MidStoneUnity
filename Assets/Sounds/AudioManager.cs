using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;

    public AudioClip background;
    public AudioClip death;
    public AudioClip levelWon;
    private void Start()
    {
        music.clip = background;
        music.Play();
    }
    public void PlaySFX(AudioClip clip) { 
        sfx.PlayOneShot(clip);
    }
}
