using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelButton : MonoBehaviour {

    public Text levelText;
    public GameObject locked;
    public Sprite unlock;
    public GameObject text;
    public bool pass = false;

	// Use this for initialization
	void Start () {
        int x = int.Parse(levelText.text.ToString());
        if(PlayerPrefs.GetInt("Level"+x) == 1)
        {
            text.SetActive(true);
            locked.SetActive(false);
        }
    }

}
