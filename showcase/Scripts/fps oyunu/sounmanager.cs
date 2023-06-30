using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounmanager : MonoBehaviour
{
    public AudioClip[] sarki;

    AudioSource audio_;

    private void Start()
    {
        audio_ = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(!audio_.isPlaying)
        {
            //audio_.clip = sarki[Random.Range(0, sarki.Length)];
            //audio_.Play();
        }
    }
}
