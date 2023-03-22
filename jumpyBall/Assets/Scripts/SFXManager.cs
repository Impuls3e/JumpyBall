using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;
    public bool sfx = true;

    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    
    void MakeSingleton()
    {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySFX(AudioClip clip, float volume) {

        
        if (sfx)
        
            audioSource.PlayOneShot(clip, volume);
            
        

    }
}
