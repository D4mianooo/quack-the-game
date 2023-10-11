using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GunAudio : MonoBehaviour {
    private AudioSource _audioSource;
    private static Random _random;
    private void OnEnable() {
        _random = new Random();
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayRandomSound(List<AudioClip> gunSoundClips) {
        int randomIndex = _random.Next(gunSoundClips.Count);
        AudioClip clip = gunSoundClips[randomIndex];
        _audioSource.PlayOneShot(clip);
    }
}
