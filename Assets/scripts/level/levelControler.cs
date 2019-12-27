using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelControler : MonoBehaviour {
    
    [System.Serializable]
    public class level
    {
        public string levelText;
        public int Unlocked;
        public bool isPass = false;
    }
    public Text chapText;
    public GameObject chap1;
    public GameObject chap;
    private Animator anim1;
    public GameObject chap2;
    public GameObject chap3;
    public Transform pa;
    public Transform pa2;
    public Transform pa3;
    public Button Button;
    public List<level> lv;
    public List<level> lv2;
    public List<level> lv3;
    public int levelplay;
    public static levelControler instance;
    private Vector2 pos1, pos2;
    private int chapPlay;
    public AudioSource audio;
    public AudioClip click;

    // Use this for initialization
    void Start () {
        audio.PlayOneShot(click);
        //PlayerPrefs.DeleteAll();
        chapPlay = 1;
        FillList();
        _MakeInstance();
        anim1 = chap.GetComponent<Animator>();
	}

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            pos1 = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pos2 = Input.mousePosition;
            if (pos1.x > pos2.x + 0.3f && chapPlay == 1)
            {
                chap2.SetActive(true);
                anim1.SetTrigger("next1");
                //Invoke("setChap2",1f);
                chapText.text = "Chap 2";
                chapPlay = 2;
            }
            else if (pos1.x > pos2.x+0.3f && chapPlay == 2)
            {
                chap3.SetActive(true);
                anim1.SetTrigger("next2");
                //Invoke("setChap3", 1f);
                chapText.text = "Chap 3";
                chapPlay = 3;
            }
            else if (pos1.x + 0.3f < pos2.x && chapPlay == 2)
            {
                chap1.SetActive(true);
                anim1.SetTrigger("back1");
                //Invoke("setChap1",1f);
                chapText.text = "Chap 1";
                chapPlay = 1;
            }
            else if (pos1.x + 0.3f < pos2.x && chapPlay == 3)
            {
                chap2.SetActive(true);
                anim1.SetTrigger("back2");
                //Invoke("setChap2", 1f);
                chapText.text = "Chap 2";
                chapPlay = 2;
            }
        }
    }

    void setChap1()
    {
        chap2.SetActive(false);
        chap3.SetActive(false);
    }

    void setChap2()
    {
        chap1.SetActive(false);
        chap3.SetActive(false);
    }

    void setChap3()
    {
        chap1.SetActive(false);
        chap2.SetActive(false);
    }

    public void FillList()
    {
        //for(int i = 1; i < 85; i++)
        //{
        //PlayerPrefs.SetInt("Level"+i, 1);
        //}
        PlayerPrefs.SetInt("Level1", 1);
        //chap1
        foreach (var leve in lv)
        {
            Button newBT = Instantiate(Button) as Button;
            levelButton bt = newBT.GetComponent<levelButton>();
            bt.levelText.text = leve.levelText;
            //bt.transform.localScale = new Vector3(1, 1, 1);
            if (PlayerPrefs.GetInt("Level" + bt.levelText.text) == 1)
            {
                leve.Unlocked = 1;
                if(PlayerPrefs.GetInt("isPass"+bt.levelText.text) == 1)
                {
                    newBT.image.overrideSprite = bt.unlock;
                }
                bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
                bt.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("main"));
            }
            else
            {
                //bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
            }
            //bt.unlocked = leve.Unlocked;
            newBT.transform.SetParent(pa);
            float x = 0f;
            newBT.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            
        }
        //chap2
        foreach (var leve in lv2)
        {
            Button newBT = Instantiate(Button) as Button;
            levelButton bt = newBT.GetComponent<levelButton>();
            bt.levelText.text = leve.levelText;
            //bt.transform.localScale = new Vector3(1, 1, 1);
            if (PlayerPrefs.GetInt("Level" + bt.levelText.text) == 1)
            {
                leve.Unlocked = 1;
                if (PlayerPrefs.GetInt("isPass" + bt.levelText.text) == 1)
                {
                    newBT.image.overrideSprite = bt.unlock;
                }
                bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
                bt.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("main"));
            }
            else
            {
                //bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
            }
            //bt.unlocked = leve.Unlocked;
            newBT.transform.SetParent(pa2);
            newBT.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        //chap3
        foreach (var leve in lv3)
        {
            Button newBT = Instantiate(Button) as Button;
            levelButton bt = newBT.GetComponent<levelButton>();
            bt.levelText.text = leve.levelText;
            //bt.transform.localScale = new Vector3(1, 1, 1);
            if (PlayerPrefs.GetInt("Level" + bt.levelText.text) == 1)
            {
                leve.Unlocked = 1;
                if (PlayerPrefs.GetInt("isPass" + bt.levelText.text) == 1)
                {
                    newBT.image.overrideSprite = bt.unlock;
                }
                bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
                bt.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("main"));
            }
            else
            {
                //bt.GetComponent<Button>().onClick.AddListener(() => loadLevel(int.Parse(bt.levelText.text.ToString())));
            }
            //bt.unlocked = leve.Unlocked;
            newBT.transform.SetParent(pa3);
            newBT.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        SaveAll();
    }

    void SaveAll()
    {
        {
            GameObject[] allBT = GameObject.FindGameObjectsWithTag("lvBT");
            foreach (GameObject bts in allBT)
            {
                levelButton btn = bts.GetComponent<levelButton>();
                //PlayerPrefs.SetInt("Level" + btn.levelText.text, btn.unlocked);
            }
        }
    }

    public void _back()
    {
        audio.PlayOneShot(click);
        SceneManager.LoadScene("menu");
    }

    void loadLevel(int index)
    {
        if (PlayerPrefs.GetInt("Level" + index) == 1)
        {
            levelplay = index;
        }
        else
        {
        }
    }

}
