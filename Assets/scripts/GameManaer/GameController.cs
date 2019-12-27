using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public Image roll;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void _back()              
    {
        SceneManager.LoadScene("level");
    }

    public void _replay()
    {
        SceneManager.LoadScene("main");
    }

    public void _next()
    {
        levelControler.instance.levelplay++;
        SceneManager.LoadScene("main");
    }

    public void _pause()
    {
        
    }

}
