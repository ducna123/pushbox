using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anim : MonoBehaviour
{

    public Image a;
    public GameObject b;
    float z;
    // Use this for initialization
    void Start()
    {
        z = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        a.color = new Color(0, 0, 0, z);
        if (z >= 0)
        {
            z -= 0.08f;
        }
        if (z <= 0)
        {
            b.SetActive(false);
        }
    }
}
