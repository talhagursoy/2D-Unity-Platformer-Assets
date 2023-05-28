using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager instance{get; private set;}
    private AudioSource audioSource;
    private void Awake() {
        if(instance!=null &&instance!=this){
			Destroy(gameObject);
			return;
		}
		instance = this;
        audioSource = GetComponent<AudioSource>();
        //DontDestroyOnLoad(gameObject);
    }
    public void playSound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
