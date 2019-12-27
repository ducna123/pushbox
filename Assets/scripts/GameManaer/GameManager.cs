using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AudioSource audio;
    public AudioClip open,click;
	// Use this for initialization
	void Start () {
        audio.PlayOneShot(click);
        //audio.playOnAwake = true;
        audio.PlayOneShot(open);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void _play()
    {
        //audio.PlayOneShot(click);
        SceneManager.LoadScene("level");
    }

}
