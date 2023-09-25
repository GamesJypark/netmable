using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    public GameObject GameMode;
    public AudioClip clip_Loby;
    public AudioClip clip_Fight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void bgmChange()
    {
        if(GameMode.activeSelf == false)
        {
            GetComponent<AudioSource>().clip = clip_Loby;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().clip = clip_Fight;
            GetComponent<AudioSource>().Play();
        }
    }
}
