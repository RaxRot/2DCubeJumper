using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    public static MainMusic Instance;
    
    private AudioSource _audio;

    [SerializeField] private AudioClip jumpSound, landSound, fruitSound, deadSound;

    [SerializeField] private GameObject musicButton, banMusicButton;

    private bool _isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _audio = GetComponent<AudioSource>();
        _audio.Play();
        
    }

    public void PlayJumpSound()
    {
        if (!_isMuted)
        {
            _audio.PlayOneShot(jumpSound);
        }
    }

    public void PlayLandSound()
    {
        if (!_isMuted)
        {
            _audio.PlayOneShot(landSound);
        }
    }

    public void PlayFruitSound()
    {
        if (!_isMuted)
        {
            _audio.PlayOneShot(fruitSound);
        }
    }

    public void PlayDeadSound()
    {
        if (!_isMuted)
        {
            _audio.Stop();
            _audio.PlayOneShot(deadSound);
        }
    }

    public void ToggleSound()
    {
        _isMuted = !_isMuted;
        if (_isMuted)
        {
            _audio.Pause();
        }
        else
        {
            _audio.Play();
        }
    }
}
