using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    // 0 box
    // 1 pass
    // 2 map1
    // 3 map2
    // 4 wall
    [System.Serializable]
    public class point
    {
        public int x;
        public int y;

        public void newPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
    public int[,] levelAray = new int[50, 50];
    public int[,] levelAray1 = new int[50, 50];
    public GameObject[,] Aray = new GameObject[50, 50];
    public List<GameObject> list;
    List<boxController> boxLv = new List<boxController>();
    List<point> winP = new List<point>();
    public Vector2 mapSize;
    public playerController player;
    int levelplay;
    public Camera cam;
    public static Map instance;
    int d = 0;
    public GameObject panelEnd;
    int a, b;
    public Text lvT;
    public AudioSource audio;
    public AudioClip click;

    // Use this for initialization
    void Start() {
        audio.PlayOneShot(click);
        levelplay = levelControler.instance.levelplay;
        lvT.text = "LEVEL " + levelplay;
        panelEnd.SetActive(false);
        _MakeInstance();
        initMap();
    }

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        if (playerController.instance.u == true || playerController.instance.d == true || playerController.instance.l == true || playerController.instance.r == true)
        {
            check2box();
        }   
        checkWin();
	}
    Vector3 po;

    public void initMap()
    {
        initMatix();
        settingCamera();
        int col = 0, row = 0;
        for(int i = (int)mapSize.x-1; i >= 0; i--)
        {
            for(int j = 0; j < (int)mapSize.y; j++)
            {
                if (col == 0)
                {
                    if(row == 0)
                    {
                        po = new Vector3(-mapSize.y / 2 + 0.2f * mapSize.y + j, -mapSize.x / 2 - 0.15f + i, 0);
                    }
                    else if(row != 0)
                    {
                        po = new Vector3(-mapSize.y / 2 + 0.2f * mapSize.y + j, -mapSize.x / 2 - 0.15f + i + 0.3f * (mapSize.x - i -1), 0);
                    }
                    
                }
                else if (col != 0)
                {
                    if(row == 0)
                    {
                        po = new Vector3(-mapSize.y / 2 + 0.2f * mapSize.y + j - 0.3f*j, -mapSize.x / 2 - 0.15f + i, 0);
                    }
                    else if(row != 0)
                    {
                        po = new Vector3(-mapSize.y / 2 + 0.2f * mapSize.y + j - 0.3f*j, -mapSize.x / 2 - 0.15f + i + 0.3f * (mapSize.x - i - 1), 0);
                    }
                }
                int objPositionArray = levelAray[row, col];
                if (levelAray[row, col] == 4 || levelAray[row, col] == 3)
                {
                    Aray[row, col] = Instantiate(list[objPositionArray-1], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    objPositionArray = 0;
                }
                if (levelAray1[row, col] == 100)
                {
                    player.gameObject.transform.position = po;
                    player.pos = new Vector2(row, col);
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 101)
                {
                    player.gameObject.transform.position = po;
                    player.pos = new Vector2(row, col);
                    objPositionArray = 0;
                    Aray[row, col] = Instantiate(list[1], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    point a = new point();
                    a.newPoint(row, col);
                    winP.Add(a);
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 1)
                {
                    Aray[row, col] = Instantiate(list[1], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    point a = new point();
                    a.newPoint(row, col);
                    winP.Add(a);
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 0)
                {
                    GameObject box = Instantiate(list[0], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    box.transform.position = po;
                    boxController bo = box.GetComponent<boxController>();
                    bo.posB = new Vector2(row, col);
                    boxLv.Add(bo);
                    d++;
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 50)
                {
                    Aray[row, col] = Instantiate(list[1], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    point a = new point();
                    a.newPoint(row, col);
                    winP.Add(a);
                    objPositionArray = 0;
                    GameObject box = Instantiate(list[0], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    box.transform.position = po;
                    boxController bo = box.GetComponent<boxController>();
                    bo.posB = new Vector2(row, col);
                    boxLv.Add(bo);
                    d++;
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 2)
                {
                    Aray[row, col] = Instantiate(list[4], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    objPositionArray = 0;
                }
                else if (levelAray1[row, col] == 6)
                {
                    Aray[row, col] = Instantiate(list[5], po, Quaternion.Euler(Vector3.zero)) as GameObject;
                    Aray[row, col].transform.position = po;
                    objPositionArray = 0;
                }
                col++;
            }
            col = 0;
            row++;
        }
    }

    public void settingCamera()
    {
        if (mapSize.x == 4 && mapSize.y == 7)
        {
            cam.orthographicSize = 4.72f;
        }
        else if (mapSize.x == 5 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.27f;
        }
        else if (mapSize.x == 7 && mapSize.y == 6)
        {
            cam.orthographicSize = 4.29f;
        }
        else if (mapSize.x == 6 && mapSize.y == 9)
        {
            cam.orthographicSize = 6.2f;
        }
        else if (mapSize.x == 6 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.52f;
        }
        else if (mapSize.x == 7 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.58f;
        }
        else if (mapSize.x == 8 && mapSize.y == 7)
        {
            cam.orthographicSize = 5.58f;
        }
        else if (mapSize.x == 12 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.58f;
        }
        else if (mapSize.x == 8 && mapSize.y == 9)
        {
            cam.orthographicSize = 6.35f;
        }
        else if (mapSize.x == 9 && mapSize.y == 7)
        {
            cam.orthographicSize = 5.36f;
        }
        else if (mapSize.x == 6 && mapSize.y == 7)
        {
            cam.orthographicSize = 5.02f;
        }
        else if (mapSize.x == 7 && mapSize.y == 9)
        {
            cam.orthographicSize = 6.22f;
        }
        else if (mapSize.x == 8 && mapSize.y == 10)
        {
            cam.orthographicSize = 6.88f;
        }
        else if (mapSize.x == 8 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.43f;
        }
        else if (mapSize.x == 7 && mapSize.y == 7)
        {
            cam.orthographicSize = 4.73f;
        }
        else if (mapSize.x == 8 && mapSize.y == 6)
        {
            cam.orthographicSize = 4.53f;
        }
        else if (mapSize.x == 10 && mapSize.y == 6)
        {
            cam.orthographicSize = 5.72f;
        }
        else if (mapSize.x == 7 && mapSize.y == 10)
        {
            cam.orthographicSize = 6.88f;
        }
        else if (mapSize.x == 9 && mapSize.y == 10)
        {
            cam.orthographicSize = 6.88f;
        }
        else if (mapSize.x == 9 && mapSize.y == 9)
        {
            cam.orthographicSize = 6.14f;
        }
        else if (mapSize.x == 10 && mapSize.y == 8)
        {
            cam.orthographicSize = 6.11f;
        }
        else if (mapSize.x == 9 && mapSize.y == 8)
        {
            cam.orthographicSize = 5.6f;
        }
        else if (mapSize.x == 10 && mapSize.y == 10)
        {
            cam.orthographicSize = 7.00f;
        }
        else if (mapSize.x == 10 && mapSize.y == 9)
        {
            cam.orthographicSize = 6.46f;
        }
        else if (mapSize.x == 6 && mapSize.y == 10)
        {
            cam.orthographicSize = 6.85f;
        }
        else if (mapSize.x == 11 && mapSize.y == 10)
        {
            cam.orthographicSize = 6.79f;
        }
        else if (mapSize.x == 11 && mapSize.y == 7)
        {
            cam.orthographicSize = 6.14f;
        }
    }

    public void initMatix()
    {
        levelplay = levelControler.instance.levelplay;
        //levelplay = 4;
        int[,] newAray = new int[50, 50];
        int[,] newAray1 = new int[50, 50];
        //chap1
        if (levelplay == 1)
        {
            mapSize = new Vector2(4,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,100,0,5,5,1,2},
                {2,5,0,5,5,1,2},
                {2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 2)
        {
            mapSize = new Vector2(5, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,1,5,5,5,5,5,2},
                {2,5,5,0,0,5,1,2},
                {2,100,5,5,5,5,5,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 3)
        {
            mapSize = new Vector2(7,6);
            newAray = new int[,]
            {
                {2,2,2,2,0,0},
                {2,3,4,2,0,0},
                {2,4,3,2,2,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,3,4,2,2,2},
                {2,2,2,2,0,0},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5},
                {2,3,1,2,5,5},
                {2,4,3,2,2,2},
                {2,50,100,3,4,2},
                {2,4,3,0,3,2},
                {2,3,4,2,2,2},
                {2,2,2,2,5,5},
            };
        }
        else if (levelplay == 4)
        {
            mapSize = new Vector2(7, 6);
            newAray = new int[,]
            {
                {2,2,2,2,2,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2},
                {2,5,5,5,5,2},
                {2,5,6,100,5,2},
                {2,5,0,50,5,2},
                {2,5,1,50,5,2},
                {2,5,5,5,5,2},
                {2,2,2,2,2,2},
            };
        }
        else if (levelplay == 5)
        {
            mapSize = new Vector2(6, 9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {2,2,2,3,4,2,2,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,2,4,3,2},
                {2,4,3,4,3,2,3,4,2},
                {2,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {2,2,2,3,4,2,2,2,2},
                {2,4,3,4,3,4,0,4,2},
                {2,3,6,3,4,2,0,3,2},
                {2,4,1,4,1,2,100,4,2},
                {2,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 6)
        {
            mapSize = new Vector2(6, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,2,3,4,3,4,2},
                {2,2,2,2,2,4,3,2},
                {5,5,5,5,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,1,50,50,0,100,2},
                {2,3,2,3,4,3,4,2},
                {2,2,2,2,2,4,3,2},
                {5,5,5,5,2,2,2,2},
            };
        }
        else if (levelplay == 7)
        {
            mapSize = new Vector2(7, 8);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2},
                {5,2,4,3,4,3,4,2},
                {5,2,3,4,3,4,3,2},
                {2,2,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2},
                {5,2,4,3,4,3,4,2},
                {5,2,3,1,0,1,3,2},
                {2,2,4,0,100,0,4,2},
                {2,4,3,1,0,1,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 8)
        {
            mapSize = new Vector2(8,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,1,0,1,4,2},
                {2,3,0,1,0,3,2},
                {2,4,1,0,1,4,2},
                {2,3,0,1,0,3,2},
                {2,4,3,100,3,4,2},
                {2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 9)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5},
                {5,2,2,3,4,2,5},
                {2,2,3,4,3,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,3,2},
                {2,2,2,3,4,3,2},
                {5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5},
                {5,2,2,3,4,2,5},
                {2,2,100,0,1,2,2},
                {2,3,0,0,4,3,2},
                {2,4,1,4,1,3,2},
                {2,2,2,3,4,3,2},
                {5,5,2,2,2,2,2},
            };
        }
        else if (levelplay == 10)
        {
            mapSize = new Vector2(7, 6);
            newAray = new int[,]
            {
                {2,2,2,2,2,2},
                {2,3,4,3,2,2},
                {2,4,3,4,3,2},
                {2,2,4,3,4,2},
                {2,2,2,4,3,2},
                {2,2,2,2,4,2},
                {2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2},
                {2,1,4,3,2,2},
                {2,100,0,0,3,2},
                {2,2,4,3,4,2},
                {2,2,2,4,3,2},
                {2,2,2,2,1,2},
                {2,2,2,2,2,2},
            };
        }
        else if (levelplay == 11)
        {
            mapSize = new Vector2(8,9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,3,4,3,4,2,5},
                {5,5,2,4,3,4,3,2,2},
                {2,2,2,3,4,3,4,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,2,2,2,2,2,2},
                {2,2,2,2,5,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,3,4,3,4,2,5},
                {5,5,2,4,6,6,100,2,2},
                {2,2,2,3,6,3,0,3,2},
                {2,4,1,1,6,4,0,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,2,2,2,2,2,2},
                {2,2,2,2,5,5,5,5,5},
            };
        }
        else if (levelplay == 12)
        {
            mapSize = new Vector2(8, 9);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5,5,5},
                {2,3,4,3,2,2,5,5,5},
                {2,4,3,4,3,2,5,5,5},
                {2,2,4,3,4,2,2,2,2},
                {5,2,2,2,3,4,3,4,2},
                {5,5,2,3,4,3,4,3,2},
                {5,5,2,4,3,4,3,4,2},
                {5,5,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5,5,5},
                {2,3,4,3,2,2,5,5,5},
                {2,4,0,4,3,2,5,5,5},
                {2,2,4,0,4,2,2,2,2},
                {5,2,2,2,100,1,3,4,2},
                {5,5,2,3,4,1,6,3,2},
                {5,5,2,4,3,4,3,4,2},
                {5,5,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 13)
        {
            mapSize = new Vector2(9,7);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,3,4,2,2,5,5},
                {2,4,3,4,2,5,5},
                {2,2,4,3,2,5,5},
                {2,2,3,4,2,2,2},
                {5,2,4,3,4,3,2},
                {5,2,3,4,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,1,4,2,2,5,5},
                {2,1,100,4,2,5,5},
                {2,1,4,0,2,5,5},
                {2,2,0,4,2,2,2},
                {5,2,4,0,4,3,2},
                {5,2,3,4,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 14)
        {
            mapSize = new Vector2(6, 7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,2,2,2},
                {2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,6,4,6,4,2},
                {2,1,4,0,50,100,2},
                {2,4,3,4,2,2,2},
                {2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 15)
        {
            mapSize = new Vector2(7,9);
            newAray = new int[,]
            {
                {5,5,5,5,5,2,2,2,5},
                {2,2,2,2,2,2,4,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,4,3,4,2},
                {5,5,5,5,2,3,4,3,2},
                {5,5,5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,5,2,2,2,5},
                {2,2,2,2,2,2,100,2,2},
                {2,4,3,4,3,1,50,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,0,6,4,2},
                {5,5,5,5,2,3,4,3,2},
                {5,5,5,5,2,2,2,2,2},
            };
        }
        else if (levelplay == 16)
        {
            mapSize = new Vector2(8,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,5,5,5,5,5},
                {5,2,4,2,2,2,2,5,5,5},
                {5,2,3,4,3,4,3,2,2,5},
                {2,2,4,3,4,3,4,3,2,5},
                {2,4,3,4,3,4,3,4,2,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,5,5,5,5,5},
                {5,2,4,2,2,2,2,5,5,5},
                {5,2,3,4,3,4,3,2,2,5},
                {2,2,4,6,6,3,4,3,2,5},
                {2,1,3,1,6,4,100,0,2,2},
                {2,3,4,3,6,3,0,0,4,2},
                {2,4,3,1,6,4,3,4,3,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 17)
        {
            mapSize = new Vector2(7,6);
            newAray = new int[,]
            {
                {2,2,2,2,2,5},
                {2,3,4,3,2,5},
                {2,4,3,4,2,5},
                {2,3,4,3,2,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5},
                {2,3,100,3,2,5},
                {2,1,1,1,2,5},
                {2,0,0,0,2,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
            };
        }
        else if (levelplay == 18)
        {
            mapSize = new Vector2(9,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,2,5,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,2,2},
                {2,4,3,4,3,2,5},
                {2,2,2,3,4,2,5},
                {5,5,2,4,3,2,5},
                {5,5,2,3,4,2,5},
                {5,5,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,2,5,2},
                {2,1,3,1,3,4,2},
                {2,3,6,6,4,2,2},
                {2,4,3,0,3,2,5},
                {2,2,2,0,4,2,5},
                {5,5,2,100,3,2,5},
                {5,5,2,3,4,2,5},
                {5,5,2,2,2,2,5},
            };
        }
        else if (levelplay == 19)
        {
            mapSize = new Vector2(8,8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,2,2,2,4,3,2,2},
                {5,5,5,2,3,4,2,5},
                {5,5,5,2,4,3,2,5},
                {5,5,5,2,3,4,2,5},
                {5,5,5,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,3,4,3,1,1,4,2},
                {2,4,3,100,0,0,3,2},
                {2,2,2,2,6,3,2,2},
                {5,5,5,2,3,4,2,5},
                {5,5,5,2,4,3,2,5},
                {5,5,5,2,3,4,2,5},
                {5,5,5,2,2,2,2,5},
            };
        }
        else if (levelplay == 20)
        {
            mapSize = new Vector2(8,9);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,5,5},
                {2,3,4,3,4,3,2,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,2,2,3,4,3,4,3,2},
                {5,5,2,4,3,4,3,4,2},
                {5,5,2,3,4,2,2,2,2},
                {5,5,2,4,3,2,5,5,5},
                {5,5,2,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,5,5},
                {2,3,4,3,4,3,2,2,2},
                {2,4,3,100,0,0,1,1,2},
                {2,2,2,6,4,6,6,3,2},
                {5,5,2,4,3,4,3,4,2},
                {5,5,2,3,4,2,2,2,2},
                {5,5,2,4,3,2,5,5,5},
                {5,5,2,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 21)
        {
            mapSize = new Vector2(6,7);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,3,4,2,2,2,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,2,3,4,3,4,2},
                {5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,3,4,2,2,2,2},
                {2,4,1,4,1,4,2},
                {2,3,0,0,6,100,2},
                {2,2,3,4,3,4,2},
                {5,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 22)
        {
            mapSize = new Vector2(9, 7);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,3,4,3,2,2,2},
                {2,4,3,4,3,4,2},
                {2,2,4,3,4,3,2},
                {5,2,3,4,3,4,2},
                {5,2,4,3,4,3,2},
                {5,2,3,4,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,3,4,3,2,2,2},
                {2,1,3,1,3,4,2},
                {2,2,4,3,6,3,2},
                {5,2,3,6,3,4,2},
                {5,2,100,0,0,3,2},
                {5,2,3,4,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 23)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,2,4,3,4,2,2},
                {5,2,3,4,3,2,5},
                {5,2,4,3,4,2,5},
                {5,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,50,4,3,2},
                {2,4,3,4,3,4,2},
                {2,2,4,6,4,2,2},
                {5,2,0,100,1,2,5},
                {5,2,4,3,4,2,5},
                {5,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 24)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,2},
                {5,5,2,3,4,3,2},
                {2,2,2,4,3,4,2},
                {2,3,4,3,2,2,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,2},
                {5,5,2,3,4,3,2},
                {2,2,2,0,0,100,2},
                {2,3,4,3,2,2,2},
                {2,4,3,4,3,4,2},
                {2,3,1,3,1,3,2},
                {2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 25)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {5,2,2,2,2,5,5},
                {5,2,4,3,2,2,2},
                {5,2,3,4,3,4,2},
                {2,2,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,2,2,2},
                {2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,5,5},
                {5,2,4,3,2,2,2},
                {5,2,3,0,0,4,2},
                {2,2,1,1,1,3,2},
                {2,4,3,100,0,4,2},
                {2,3,4,3,2,2,2},
                {2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 26)
        {
            mapSize = new Vector2(8,6);
            newAray = new int[,]
            {
                {5,2,2,2,2,2},
                {5,2,4,3,4,2},
                {5,2,3,4,3,2},
                {2,2,2,3,4,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,2,2,4,3,2},
                {5,5,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2},
                {5,2,4,100,4,2},
                {5,2,3,4,3,2},
                {2,2,2,0,4,2},
                {2,4,1,1,1,2},
                {2,3,0,0,4,2},
                {2,2,2,4,3,2},
                {5,5,2,2,2,2},
            };
        }
        else if (levelplay == 27)
        {
            mapSize = new Vector2(7,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,5},
                {2,3,4,3,4,2,5},
                {2,4,3,4,3,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,2,2,2},
                {2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,5},
                {2,3,4,3,1,2,5},
                {2,4,3,4,3,2,2},
                {2,3,4,0,0,100,2},
                {2,4,3,4,3,4,2},
                {2,1,4,3,2,2,2},
                {2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 28)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,3,4,3,2,5,5},
                {2,4,3,4,2,5,5},
                {2,3,4,3,2,2,2},
                {2,2,3,4,3,4,2},
                {5,2,4,3,4,3,2},
                {5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,3,4,3,2,5,5},
                {2,4,100,4,2,5,5},
                {2,3,0,0,2,2,2},
                {2,2,1,4,1,4,2},
                {5,2,4,3,4,3,2},
                {5,2,2,2,2,2,2},
            };
        }
        //chap2
        else if (levelplay == 29)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {5,2,2,2,2,5,5},
                {2,2,4,3,2,2,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,2,2,2},
                {2,2,4,3,2,5,5},
                {5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,5,5},
                {2,2,4,3,2,2,2},
                {2,4,3,4,3,4,2},
                {2,1,50,50,0,100,2},
                {2,4,3,4,2,2,2},
                {2,2,4,3,2,5,5},
                {5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 30)
        {
            mapSize = new Vector2(7, 7);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,3,4,2,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,2,4,3,2},
                {2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2},
                {2,1,4,2,4,3,2},
                {2,4,3,0,3,4,2},
                {2,1,4,0,6,100,2},
                {2,4,3,0,3,4,2},
                {2,1,4,2,4,3,2},
                {2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 31)
        {
            mapSize = new Vector2(6,9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {2,2,2,3,4,2,2,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {2,2,2,3,4,2,2,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,100,0,50,50,50,1,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 32)
        {
            mapSize = new Vector2(10,6);
            newAray = new int[,]
            {
                {5,2,2,2,2,5},
                {2,2,4,3,2,5},
                {2,4,3,4,2,5},
                {2,3,4,3,2,5},
                {2,4,3,4,2,5},
                {2,3,4,3,2,5},
                {2,4,3,4,2,2},
                {2,3,4,3,4,2},
                {2,2,3,4,3,2},
                {5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,5},
                {2,2,4,3,2,5},
                {2,1,3,0,2,5},
                {2,1,0,3,2,5},
                {2,1,0,4,2,5},
                {2,1,0,3,2,5},
                {2,1,3,0,2,2},
                {2,3,4,3,100,2},
                {2,2,3,4,3,2},
                {5,2,2,2,2,2},
            };
        }
        else if (levelplay == 33)
        {
            mapSize = new Vector2(8,9);
            newAray = new int[,]
            {
                {5,5,5,5,5,5,2,2,2},
                {2,2,2,2,2,5,2,4,2},
                {2,3,4,3,2,2,2,3,2},
                {2,4,3,4,3,4,2,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,4,3,4,2},
                {5,5,5,5,2,3,4,3,2},
                {5,5,5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,5,5,2,2,2},
                {2,2,2,2,2,5,2,1,2},
                {2,3,4,3,2,2,2,1,2},
                {2,4,3,4,3,0,2,1,2},
                {2,3,4,0,4,3,0,3,2},
                {2,2,2,2,2,4,100,6,2},
                {5,5,5,5,2,3,4,3,2},
                {5,5,5,5,2,2,2,2,2},
            };
        }
        else if (levelplay == 34)
        {
            mapSize = new Vector2(7,10);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2,2,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,2,2,2,2,4,3,4,3,2},
                {5,5,5,5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2,2,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,6,6,1,6,6,6,4,2},
                {2,4,6,4,0,0,3,1,3,2},
                {2,3,1,3,100,0,6,6,4,2},
                {2,2,2,2,2,4,3,4,3,2},
                {5,5,5,5,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 35)
        {
            mapSize = new Vector2(9, 10);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5,5,5,5},
                {2,4,3,4,2,2,2,2,5,5},
                {2,3,4,3,2,3,4,2,5,5},
                {2,4,3,4,3,4,3,2,2,2},
                {2,2,2,3,2,3,4,3,4,2},
                {2,4,3,4,2,4,3,4,3,2},
                {2,3,4,3,2,2,2,2,2,2},
                {2,4,3,4,2,5,5,5,5,5},
                {2,2,2,2,2,5,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5,5,5,5},
                {2,4,3,4,2,2,2,2,5,5},
                {2,3,6,3,2,3,1,2,5,5},
                {2,4,3,4,3,0,3,2,2,2},
                {2,2,2,3,2,0,1,3,4,2},
                {2,4,3,4,2,100,3,4,3,2},
                {2,3,6,3,2,2,2,2,2,2},
                {2,4,3,4,2,5,5,5,5,5},
                {2,2,2,2,2,5,5,5,5,5},
            };
        }
        else if (levelplay == 36)
        {
            mapSize = new Vector2(6,7);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,5},
                {5,2,3,4,3,2,5},
                {2,2,4,3,4,2,2},
                {2,4,3,4,3,4,3},
                {2,3,4,3,4,3,4},
                {2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,5},
                {5,2,3,4,3,2,5},
                {2,2,4,3,4,2,2},
                {2,4,0,0,0,4,3},
                {2,3,1,101,1,3,4},
                {2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 37)
        {
            mapSize = new Vector2(6,8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,5},
                {2,4,3,4,3,4,2,5},
                {2,3,4,3,4,3,2,2},
                {2,4,3,4,3,4,3,2},
                {2,2,4,3,4,3,2,2},
                {5,2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,5},
                {2,4,3,4,3,4,2,5},
                {2,100,0,0,0,3,2,2},
                {2,4,3,6,1,1,1,2},
                {2,2,4,3,4,3,2,2},
                {5,2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 38)
        {
            mapSize = new Vector2(8,7);
            newAray = new int[,]
            {
                {5,5,5,2,2,2,2},
                {5,5,5,2,3,4,2},
                {5,5,5,2,4,3,2},
                {2,2,2,2,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,2,2},
                {2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,2,2,2,2},
                {5,5,5,2,3,4,2},
                {5,5,5,2,100,3,2},
                {2,2,2,2,0,1,2},
                {2,3,4,3,0,1,2},
                {2,4,6,4,0,1,2},
                {2,3,4,3,4,2,2},
                {2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 39)
        {
            mapSize = new Vector2(9,9);
            newAray = new int[,]
            {
                {5,5,5,5,5,2,2,2,2},
                {5,5,5,5,5,2,3,4,2},
                {5,5,5,5,5,2,4,3,2},
                {2,2,2,2,2,2,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,2,3,4,2},
                {2,3,4,3,4,2,2,2,2},
                {2,2,2,4,3,2,5,5,5},
                {5,5,2,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,5,2,2,2,2},
                {5,5,5,5,5,2,3,100,2},
                {5,5,5,5,5,2,4,3,2},
                {2,2,2,2,2,2,3,1,2},
                {2,3,4,3,0,3,4,1,2},
                {2,4,3,0,0,2,3,1,2},
                {2,3,4,3,4,2,2,2,2},
                {2,2,2,4,3,2,5,5,5},
                {5,5,2,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 40)
        {
            mapSize = new Vector2(7,6);
            newAray = new int[,]
            {
                {2,2,2,2,2,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,2,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2},
                {2,1,1,1,3,2},
                {2,3,4,0,4,2},
                {2,4,6,0,3,2},
                {2,3,4,0,4,2},
                {2,4,3,100,3,2},
                {2,2,2,2,2,2},
            };
        }
        else if (levelplay == 41)
        {
            mapSize = new Vector2(8, 7);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2},
                {2,2,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,2,3,4,3,2,2},
                {5,2,4,3,4,2,5},
                {5,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2},
                {2,2,3,4,3,4,2},
                {2,3,4,6,6,3,2},
                {2,4,6,4,0,4,2},
                {2,3,4,50,4,1,2},
                {2,2,3,6,100,2,2},
                {5,2,4,3,4,2,5},
                {5,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 42)
        {
            mapSize = new Vector2(8, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,3,4,3,2,5,5},
                {2,3,4,3,4,2,2,5},
                {2,4,3,4,3,4,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,2,3,4,3,2},
                {2,2,2,2,4,3,4,2},
                {5,5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,3,100,3,2,5,5},
                {2,3,4,6,4,2,2,5},
                {2,4,1,6,3,4,2,2},
                {2,3,1,0,0,0,4,2},
                {2,4,1,2,3,4,3,2},
                {2,2,2,2,4,3,4,2},
                {5,5,5,2,2,2,2,2},
            };
        }
        else if (levelplay == 43)
        {
            mapSize = new Vector2(10, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,3,4,3,2,5,5},
                {2,3,4,3,4,2,5,5},
                {2,4,3,4,3,2,5,5},
                {2,3,4,3,2,2,5,5},
                {2,2,2,4,2,2,2,2},
                {5,2,4,3,2,3,4,2},
                {5,2,3,4,3,4,3,2},
                {5,2,4,3,4,3,4,2},
                {5,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,100,4,3,2,5,5},
                {2,3,0,6,4,2,5,5},
                {2,4,0,4,3,2,5,5},
                {2,3,0,3,2,2,5,5},
                {2,2,2,4,2,2,2,2},
                {5,2,4,3,2,3,4,2},
                {5,2,1,1,1,4,3,2},
                {5,2,4,3,4,3,4,2},
                {5,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 44)
        {
            mapSize = new Vector2(7,10);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5,5,5,5},
                {2,2,2,4,3,2,2,2,2,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,3,4,3,4,3,4,2},
                {5,5,2,4,3,4,3,4,3,2},
                {5,5,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5,5,5,5},
                {2,2,2,4,3,2,2,2,2,2},
                {2,3,4,0,4,3,100,1,1,2},
                {2,4,0,4,3,4,3,4,3,2},
                {2,2,2,3,4,3,4,3,4,2},
                {5,5,2,4,3,4,3,4,3,2},
                {5,5,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 45)
        {
            mapSize = new Vector2(7,8);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5,5},
                {2,4,3,2,2,2,5,5},
                {2,3,4,3,4,2,2,2},
                {2,4,3,4,3,4,3,2},
                {2,2,2,3,4,3,4,2},
                {5,5,2,4,3,4,3,2},
                {5,5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5,5},
                {2,4,3,2,2,2,5,5},
                {2,3,4,3,4,2,2,2},
                {2,4,3,0,50,100,3,2},
                {2,2,2,3,1,6,4,2},
                {5,5,2,4,3,4,3,2},
                {5,5,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 46)
        {
            mapSize = new Vector2(8,6);
            newAray = new int[,]
            {
                {5,5,2,2,2,2},
                {2,2,2,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,3,4,3,4,2},
                {2,4,3,4,3,2},
                {2,2,2,3,4,2},
                {5,5,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2},
                {2,2,2,4,100,2},
                {2,3,4,0,4,2},
                {2,4,3,50,1,2},
                {2,3,4,50,1,2},
                {2,4,3,0,3,2},
                {2,2,2,3,4,2},
                {5,5,2,2,2,2},
            };
        }
        else if (levelplay == 47)
        {
            mapSize = new Vector2(7,7);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,5},
                {2,2,3,4,3,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {5,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,5},
                {2,2,1,4,1,2,2},
                {2,3,50,3,50,3,2},
                {2,4,3,6,3,4,2},
                {2,3,0,3,0,3,2},
                {2,4,3,100,3,4,2},
                {5,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 48)
        {
            mapSize = new Vector2(8,10);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2,5,5},
                {2,4,3,4,2,4,3,2,5,5},
                {2,3,4,3,4,3,4,2,5,5},
                {2,2,2,2,2,4,3,2,5,5},
                {5,5,5,5,2,3,4,2,2,2},
                {5,5,5,5,2,4,3,4,3,2},
                {5,5,5,5,2,3,4,2,2,2},
                {5,5,5,5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2,5,5},
                {2,4,100,4,2,4,3,2,5,5},
                {2,3,4,3,4,3,4,2,5,5},
                {2,2,2,2,2,0,3,2,5,5},
                {5,5,5,5,2,3,4,2,2,2},
                {5,5,5,5,2,0,3,1,1,2},
                {5,5,5,5,2,3,4,2,2,2},
                {5,5,5,5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 49)
        {
            mapSize = new Vector2(6,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,2,2},
                {2,3,4,3,4,3,2},
                {2,2,3,4,3,4,2},
                {5,2,4,3,4,3,2},
                {5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,2,2},
                {2,3,4,0,4,3,2},
                {2,2,50,4,1,4,2},
                {5,2,4,3,4,100,2},
                {5,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 50)
        {
            mapSize = new Vector2(9,8);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5,5},
                {5,5,2,4,3,2,5,5},
                {5,5,2,3,4,2,5,5},
                {5,5,2,4,3,2,5,5},
                {2,2,2,3,2,2,2,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,4,3,2},
                {5,5,5,5,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5,5},
                {5,5,2,4,3,2,5,5},
                {5,5,2,100,4,2,5,5},
                {5,5,2,4,3,2,5,5},
                {2,2,2,3,2,2,2,2},
                {2,4,3,4,3,50,3,2},
                {2,3,4,0,4,3,4,2},
                {2,2,2,2,2,1,3,2},
                {5,5,5,5,2,2,2,2},
            };
        }
        else if (levelplay == 51)
        {
            mapSize = new Vector2(7,7);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,4,3,2,2,2,2},
                {2,3,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,2,4,3,4,3,2},
                {5,2,3,4,3,2,2},
                {5,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5},
                {2,4,3,2,2,2,2},
                {2,1,50,0,4,3,2},
                {2,4,1,0,6,4,2},
                {2,2,4,100,4,3,2},
                {5,2,3,4,3,2,2},
                {5,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 52)
        {
            mapSize = new Vector2(10,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,2},
                {5,2,3,4,3,4,3,4,3,2},
                {2,2,4,2,2,2,2,2,4,2},
                {2,4,3,2,3,4,3,2,3,2},
                {2,3,4,2,4,3,4,3,4,2},
                {2,4,3,2,2,4,2,2,3,2},
                {2,2,4,2,2,3,4,2,4,2},
                {2,4,3,4,3,4,3,2,3,2},
                {2,3,4,3,2,3,4,2,2,2},
                {2,2,2,2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,2},
                {5,2,3,4,3,4,3,4,3,2},
                {2,2,100,2,2,2,2,2,4,2},
                {2,4,3,2,3,4,3,2,3,2},
                {2,3,4,2,4,3,4,0,1,2},
                {2,4,3,2,2,0,2,2,1,2},
                {2,2,0,2,2,3,4,2,1,2},
                {2,4,3,4,0,4,3,2,1,2},
                {2,3,4,3,2,3,4,2,2,2},
                {2,2,2,2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 53)
        {
            mapSize = new Vector2(10, 9);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2,5},
                {2,4,3,4,3,4,3,2,5},
                {2,3,2,2,2,2,4,2,5},
                {2,4,2,4,3,4,3,2,5},
                {2,3,2,2,2,3,2,2,2},
                {2,4,2,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,2,2,3,4,3,2,2},
                {5,5,5,2,4,2,2,2,5},
                {5,5,5,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2,5},
                {2,4,3,4,3,4,3,2,5},
                {2,3,2,2,2,2,4,2,5},
                {2,4,2,1,1,1,100,2,5},
                {2,3,2,2,2,0,2,2,2},
                {2,4,2,4,3,4,3,4,2},
                {2,3,4,0,0,3,0,3,2},
                {2,2,2,2,3,4,3,2,2},
                {5,5,5,2,1,2,2,2,5},
                {5,5,5,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 54)
        {
            mapSize = new Vector2(9,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,5,5,5},
                {2,2,3,4,3,4,2,5,5,5},
                {2,3,4,3,4,3,2,5,5,5},
                {2,4,3,4,3,4,2,5,5,5},
                {2,2,2,3,4,2,2,2,2,2},
                {5,5,2,2,3,2,3,4,3,2},
                {5,5,5,2,4,3,4,3,4,2},
                {5,5,5,2,3,4,2,2,2,2},
                {5,5,5,2,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,5,5,5},
                {2,2,3,4,3,4,2,5,5,5},
                {2,3,4,3,0,3,2,5,5,5},
                {2,4,3,4,0,4,2,5,5,5},
                {2,2,2,0,1,2,2,2,2,2},
                {5,5,2,2,1,2,3,100,3,2},
                {5,5,5,2,1,3,4,0,4,2},
                {5,5,5,2,1,4,2,2,2,2},
                {5,5,5,2,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 55)
        {
            mapSize = new Vector2(9, 9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,4,3,4,3,2,5},
                {5,5,2,3,4,3,4,2,5},
                {5,2,2,2,2,4,3,2,5},
                {2,2,4,3,4,3,4,2,5},
                {2,4,3,4,3,4,3,2,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,3,4,2,4,3,4,2},
                {5,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,4,3,4,3,2,5},
                {5,5,2,3,4,0,4,2,5},
                {5,2,2,2,2,0,3,2,5},
                {2,2,4,0,4,0,4,2,5},
                {2,1,1,1,1,6,3,2,2},
                {2,3,4,3,4,3,100,3,2},
                {2,2,3,4,2,4,3,4,2},
                {5,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 56)
        {
            mapSize = new Vector2(8,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,2,5},
                {2,3,4,3,4,2,5},
                {2,4,3,4,3,2,2},
                {2,2,4,3,4,3,2},
                {5,2,3,4,3,4,2},
                {5,2,2,3,4,3,2},
                {5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,2,5},
                {2,3,6,3,4,2,5},
                {2,100,0,50,1,2,2},
                {2,2,4,3,1,3,2},
                {5,2,3,0,6,4,2},
                {5,2,2,3,4,3,2},
                {5,5,2,2,2,2,2},
            };
        }
        //chap3
        else if (levelplay == 57)
        {
            mapSize = new Vector2(10, 8);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5,5},
                {2,4,3,2,2,2,2,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,2,4,2,2,3,2,2},
                {2,4,3,4,2,4,2,5},
                {2,3,2,2,2,3,2,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,2,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5,5},
                {2,4,3,2,2,2,2,2},
                {2,3,0,0,4,0,4,2},
                {2,4,3,4,3,4,3,2},
                {2,2,4,2,2,3,2,2},
                {2,1,1,1,2,100,2,5},
                {2,3,2,2,2,3,2,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,2,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 58)
        {
            mapSize = new Vector2(7,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,5,5},
                {2,2,3,4,3,4,3,2,5,5},
                {2,3,4,3,2,2,2,2,2,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,3,4,3,4,3,4,2},
                {5,5,2,2,2,4,3,4,3,2},
                {5,5,5,5,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,5,5},
                {2,2,3,1,1,1,1,2,5,5},
                {2,3,4,3,2,2,2,2,2,2},
                {2,4,3,4,0,4,0,4,100,2},
                {2,2,2,3,4,0,4,0,4,2},
                {5,5,2,2,2,4,3,4,3,2},
                {5,5,5,5,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 59)
        {
            mapSize = new Vector2(6,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,2},
                {5,2,3,4,3,4,2,4,3,2},
                {2,2,4,3,2,3,2,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,4,3,2,3,4,3,4,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,2},
                {5,2,3,4,3,4,2,4,3,2},
                {2,2,4,0,2,0,2,3,4,2},
                {2,4,3,1,0,1,100,4,3,2},
                {2,3,4,1,2,3,4,3,4,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 60)
        {
            mapSize = new Vector2(11, 10);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5,5,5,5},
                {2,4,3,2,2,2,2,2,2,2},
                {2,3,4,3,4,2,2,3,4,2},
                {2,4,3,2,3,4,3,4,3,2},
                {2,2,4,2,2,3,2,3,4,2},
                {5,2,3,4,3,4,2,4,3,2},
                {5,2,2,2,2,3,2,3,4,2},
                {5,5,2,4,3,4,3,2,2,2},
                {5,5,2,3,4,3,4,2,5,5},
                {5,5,2,4,3,4,3,2,5,5},
                {5,5,2,2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5,5,5,5},
                {2,4,3,2,2,2,2,2,2,2},
                {2,3,4,1,4,2,2,3,1,2},
                {2,4,0,2,3,4,3,4,1,2},
                {2,2,4,2,2,3,2,3,1,2},
                {5,2,3,4,3,4,2,4,3,2},
                {5,2,2,2,2,3,2,3,4,2},
                {5,5,2,4,100,0,3,2,2,2},
                {5,5,2,3,0,0,4,2,5,5},
                {5,5,2,4,3,4,3,2,5,5},
                {5,5,2,2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 61)
        {
            mapSize = new Vector2(9, 6);
            newAray = new int[,]
            {
                {5,2,2,2,2,2},
                {5,2,3,4,3,2},
                {5,2,4,3,4,2},
                {2,2,3,4,3,2},
                {2,3,4,3,2,2},
                {2,4,3,4,2,2},
                {2,2,4,3,4,2},
                {5,2,3,4,3,2},
                {5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2},
                {5,2,3,4,3,2},
                {5,2,4,1,4,2},
                {2,2,3,50,3,2},
                {2,3,4,50,2,2},
                {2,4,3,100,2,2},
                {2,2,4,0,4,2},
                {5,2,3,4,3,2},
                {5,2,2,2,2,2},
            };
        }
        else if (levelplay == 62)
        {
            mapSize = new Vector2(8,8);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5,5},
                {2,4,3,4,2,2,2,5},
                {2,3,4,3,4,3,2,2},
                {2,2,3,2,3,4,3,2},
                {2,3,4,2,4,3,4,2},
                {2,4,3,2,2,4,2,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5,5},
                {2,4,3,4,2,2,2,5},
                {2,3,1,3,4,3,2,2},
                {2,2,50,2,0,4,3,2},
                {2,3,1,2,4,0,4,2},
                {2,4,100,2,2,4,2,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 63)
        {
            mapSize = new Vector2(10, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,3,4,3,2,2,5},
                {2,3,4,3,4,3,2,2},
                {2,2,3,4,3,4,3,2},
                {5,2,4,2,4,3,4,2},
                {5,2,3,2,2,4,2,2},
                {5,2,4,3,4,3,4,2},
                {5,2,3,4,3,4,3,2},
                {5,2,4,3,2,2,2,2},
                {5,2,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,5,5},
                {2,4,3,4,3,2,2,5},
                {2,3,0,3,0,3,2,2},
                {2,2,3,0,0,4,3,2},
                {5,2,4,2,4,3,4,2},
                {5,2,3,2,2,4,2,2},
                {5,2,4,3,1,3,1,2},
                {5,2,3,100,1,4,1,2},
                {5,2,4,3,2,2,2,2},
                {5,2,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 64)
        {
            mapSize = new Vector2(8,9);
            newAray = new int[,]
            {
                {5,5,5,5,2,2,2,2,2},
                {5,5,2,2,2,4,3,2,2},
                {5,2,2,3,4,3,4,3,2},
                {2,2,3,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,2,2,2},
                {2,3,4,3,2,2,2,5,5},
                {2,2,2,2,2,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,2,2,2,2,2},
                {5,5,2,2,2,4,3,2,2},
                {5,2,2,3,0,3,4,3,2},
                {2,2,3,0,3,4,6,4,2},
                {2,3,100,6,0,0,4,3,2},
                {2,4,1,1,3,4,2,2,2},
                {2,3,1,1,2,2,2,5,5},
                {2,2,2,2,2,5,5,5,5},
            };
        }
        else if (levelplay == 65)
        {
            mapSize = new Vector2(10,9);
            newAray = new int[,]
            {
                {5,5,5,5,5,2,2,2,2},
                {2,2,2,2,2,2,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,2,4,2,2,2,2,2,2},
                {2,4,3,4,3,2,5,5,5},
                {2,3,4,3,4,2,2,2,5},
                {2,2,3,4,3,4,3,2,5},
                {5,2,2,3,4,3,4,2,5},
                {5,5,2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,5,2,2,2,2},
                {2,2,2,2,2,2,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,1,1,1,3,1,2},
                {2,2,0,2,2,2,2,2,2},
                {2,4,0,4,3,2,5,5,5},
                {2,3,4,3,0,2,2,2,5},
                {2,2,3,4,0,4,3,2,5},
                {5,2,2,3,100,3,4,2,5},
                {5,5,2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 66)
        {
            mapSize = new Vector2(9,10);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2,2,2},
                {2,4,3,4,2,2,3,4,3,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,2,2,2,3,4,3,4,3,2},
                {5,5,5,2,4,3,4,3,2,2},
                {5,5,5,2,3,4,3,4,2,5},
                {5,5,5,2,4,3,4,3,2,5},
                {5,5,5,2,3,4,3,4,2,5},
                {5,5,5,2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2,2,2},
                {2,4,3,4,2,2,3,4,3,2},
                {2,3,0,3,4,0,100,6,4,2},
                {2,2,2,2,3,6,3,0,3,2},
                {5,5,5,2,1,6,4,3,2,2},
                {5,5,5,2,1,6,3,0,2,5},
                {5,5,5,2,1,3,4,3,2,5},
                {5,5,5,2,1,4,3,4,2,5},
                {5,5,5,2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 67)
        {
            mapSize = new Vector2(7, 10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,5},
                {5,2,3,4,3,4,3,4,2,5},
                {5,2,4,3,4,3,4,3,2,5},
                {2,2,2,4,3,4,3,2,2,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,5},
                {5,2,3,4,100,4,3,4,2,5},
                {5,2,4,0,4,3,0,3,2,5},
                {2,2,2,4,6,6,3,2,2,2},
                {2,3,4,0,1,1,0,3,4,2},
                {2,4,3,4,1,1,3,4,3,2},
                {2,2,2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 68)
        {
            mapSize = new Vector2(8, 9);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2,2},
                {2,4,3,4,2,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,2,3,4,3,4,3,2,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,2,4,3,4,2},
                {2,2,2,2,2,2,4,3,2},
                {5,5,5,5,5,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2,2},
                {2,4,100,4,2,4,3,4,2},
                {2,3,0,3,0,3,4,3,2},
                {2,2,0,6,6,6,3,2,2},
                {2,3,4,1,1,1,4,3,2},
                {2,4,3,4,2,4,3,4,2},
                {2,2,2,2,2,2,4,3,2},
                {5,5,5,5,5,2,2,2,2},
            };
        }
        else if (levelplay == 69)
        {
            mapSize = new Vector2(8,8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,100,3,4,3,4,3,2},
                {2,3,1,0,0,1,4,2},
                {2,4,0,1,1,0,3,2},
                {2,3,0,1,1,0,4,2},
                {2,4,1,0,0,1,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 70)
        {
            mapSize = new Vector2(8, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,5},
                {2,4,3,2,3,4,2,5},
                {2,3,4,3,4,3,2,5},
                {2,4,3,2,3,4,2,2},
                {2,3,4,2,4,3,4,2},
                {2,4,3,2,3,4,3,2},
                {2,3,4,2,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,5},
                {2,4,100,2,3,4,2,5},
                {2,1,0,3,4,3,2,5},
                {2,1,3,2,3,0,2,2},
                {2,1,0,2,4,3,4,2},
                {2,1,3,2,3,0,3,2},
                {2,3,4,2,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 71)
        {
            mapSize = new Vector2(8, 8);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,2,5},
                {5,5,2,4,3,4,2,2},
                {2,2,2,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,2,2,2},
                {2,2,4,3,4,2,5,5},
                {5,2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,2,5},
                {5,5,2,4,1,4,2,2},
                {2,2,2,3,0,3,4,2},
                {2,4,1,4,0,6,100,2},
                {2,3,6,0,4,1,4,2},
                {2,4,3,0,3,2,2,2},
                {2,2,4,1,4,2,5,5},
                {5,2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 72)
        {
            mapSize = new Vector2(9,9);
            newAray = new int[,]
            {
                {5,5,5,5,2,2,2,2,2},
                {2,2,2,2,2,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,2,2,3,4,3,4,3,2},
                {5,5,2,4,3,4,3,4,2},
                {5,5,2,2,2,3,4,2,2},
                {5,5,5,5,2,4,3,2,5},
                {5,5,5,5,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,2,2,2,2,2},
                {2,2,2,2,2,4,3,4,2},
                {2,3,4,3,4,0,4,3,2},
                {2,4,3,0,6,0,6,100,2},
                {2,2,2,3,6,3,4,3,2},
                {5,5,2,4,1,1,1,4,2},
                {5,5,2,2,2,3,4,2,2},
                {5,5,5,5,2,4,3,2,5},
                {5,5,5,5,2,2,2,2,5},
            };
        }
        else if (levelplay == 73)
        {
            mapSize = new Vector2(9,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,5},
                {5,2,3,4,3,4,3,4,2,5},
                {5,2,4,3,4,3,4,3,2,5},
                {2,2,3,4,3,4,3,4,2,5},
                {2,3,4,3,4,3,4,2,2,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,2,2,2,4,3,4,2},
                {5,5,5,5,5,2,3,4,3,2},
                {5,5,5,5,5,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,2,5},
                {5,2,3,4,3,4,3,4,2,5},
                {5,2,100,3,4,3,0,3,2,5},
                {2,2,3,6,6,6,0,4,2,5},
                {2,3,1,1,1,1,1,2,2,2},
                {2,4,0,4,0,4,0,4,3,2},
                {2,2,2,2,2,2,4,6,4,2},
                {5,5,5,5,5,2,3,4,3,2},
                {5,5,5,5,5,2,2,2,2,2},
            };
        }
        else if (levelplay == 74)
        {
            mapSize = new Vector2(8, 8);
            newAray = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2},
                {2,2,2,2,2,2,2,2},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,2,2,2},
                {2,100,3,4,3,4,3,2},
                {2,3,0,50,50,50,4,2},
                {2,4,50,4,3,50,3,2},
                {2,3,50,3,4,50,4,2},
                {2,4,50,50,50,1,3,2},
                {2,3,4,3,4,3,100,2},
                {2,2,2,2,2,2,2,2},
            };
        }
        else if (levelplay == 75)
        {
            mapSize = new Vector2(9, 9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {5,5,2,4,3,2,5,5,5},
                {5,5,2,3,4,2,2,2,2},
                {2,2,2,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,2,2,2},
                {2,2,2,2,4,3,2,5,5},
                {5,5,5,2,3,4,2,5,5},
                {5,5,5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,5,5,5},
                {5,5,2,4,3,2,5,5,5},
                {5,5,2,3,0,2,2,2,2},
                {2,2,2,1,3,1,3,4,2},
                {2,3,0,3,6,3,0,3,2},
                {2,4,3,1,3,1,2,2,2},
                {2,2,2,2,0,3,2,5,5},
                {5,5,5,2,3,100,2,5,5},
                {5,5,5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 76)
        {
            mapSize = new Vector2(7,10);
            newAray = new int[,]
            {
                {5,5,5,5,5,2,2,2,2,2},
                {5,5,5,2,2,2,3,4,3,2},
                {2,2,2,2,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,3,2,2},
                {2,2,2,2,2,4,3,4,2,5},
                {5,5,5,5,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,5,5,2,2,2,2,2},
                {5,5,5,2,2,2,3,4,3,2},
                {2,2,2,2,1,1,1,1,1,2},
                {2,4,100,0,0,0,0,0,3,2},
                {2,3,4,3,4,3,6,3,2,2},
                {2,2,2,2,2,4,3,4,2,5},
                {5,5,5,5,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 77)
        {
            mapSize = new Vector2(10,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,5,5,5,5},
                {2,2,3,4,3,2,2,5,5,5},
                {2,3,4,3,4,3,2,2,5,5},
                {2,4,3,4,3,4,3,2,2,5},
                {2,2,2,3,4,3,4,3,2,2},
                {5,5,2,4,3,4,3,4,3,2},
                {5,2,2,3,4,3,4,3,4,2},
                {5,2,3,4,3,4,3,4,2,2},
                {5,2,4,3,4,2,4,3,2,5},
                {5,2,2,2,2,2,2,2,2,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,5,5,5,5},
                {2,2,3,4,3,2,2,5,5,5},
                {2,3,4,0,4,3,2,2,5,5},
                {2,4,0,4,0,4,3,2,2,5},
                {2,2,2,0,6,3,1,3,2,2},
                {5,5,2,4,6,4,1,4,3,2},
                {5,2,2,3,6,6,1,3,4,2},
                {5,2,3,100,3,4,1,4,2,2},
                {5,2,4,3,4,2,4,3,2,5},
                {5,2,2,2,2,2,2,2,2,5},
            };
        }
        else if (levelplay == 78)
        {
            mapSize = new Vector2(10, 9);
            newAray = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,4,3,4,3,2,2},
                {5,2,2,3,4,3,4,3,2},
                {5,2,3,4,3,4,3,4,2},
                {5,2,4,3,4,3,4,3,2},
                {5,2,3,4,3,4,3,4,2},
                {2,2,2,2,4,3,4,3,2},
                {2,4,3,4,3,4,3,2,2},
                {2,3,4,3,4,3,2,2,5},
                {2,2,2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,2,2,2,2,2,2,5},
                {5,5,2,4,3,4,3,2,2},
                {5,2,2,3,6,6,4,3,2},
                {5,2,3,0,0,4,6,4,2},
                {5,2,4,100,0,3,6,3,2},
                {5,2,3,4,3,4,6,4,2},
                {2,2,2,2,4,6,4,3,2},
                {2,4,3,1,1,1,3,2,2},
                {2,3,4,3,4,3,2,2,5},
                {2,2,2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 79)
        {
            mapSize = new Vector2(9,10);
            newAray = new int[,]
            {
                {5,5,5,2,2,2,2,5,5,5},
                {5,2,2,2,3,4,2,2,2,2},
                {5,2,4,3,4,3,4,3,4,2},
                {2,2,3,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,2,2,2},
                {2,4,3,4,3,4,3,4,2,5},
                {2,3,4,3,2,3,4,3,2,5},
                {2,4,3,2,2,2,2,2,2,5},
                {2,2,2,2,5,5,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,5,5,2,2,2,2,5,5,5},
                {5,2,2,2,3,4,2,2,2,2},
                {5,2,4,0,4,0,1,3,4,2},
                {2,2,3,6,3,4,1,0,3,2},
                {2,3,4,3,6,6,1,2,2,2},
                {2,4,3,0,3,4,1,4,2,5},
                {2,3,100,3,2,3,4,3,2,5},
                {2,4,3,2,2,2,2,2,2,5},
                {2,2,2,2,5,5,5,5,5,5},
            };
        }
        else if (levelplay == 80)
        {
            mapSize = new Vector2(11,7);
            newAray = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,5,5},
                {2,3,4,3,2,5,5},
                {2,4,3,4,2,2,5},
                {2,2,4,3,4,3,2},
                {2,4,3,4,3,4,2},
                {2,3,4,3,2,3,2},
                {2,2,3,4,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,3,4,2,5,5},
                {5,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,2,5,5},
                {2,4,3,4,2,5,5},
                {2,3,1,3,2,5,5},
                {2,1,100,1,2,2,5},
                {2,2,1,6,4,3,2},
                {2,4,3,0,3,4,2},
                {2,3,0,3,2,3,2},
                {2,2,0,0,3,4,2},
                {5,2,4,3,2,2,2},
                {5,2,3,4,2,5,5},
                {5,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 81)
        {
            mapSize = new Vector2(8,10);
            newAray = new int[,]
            {
                {2,2,2,2,5,5,5,5,5,5},
                {2,4,3,2,2,2,5,5,5,5},
                {2,3,4,3,4,2,2,2,2,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,4,3,4,3,4,3,4,2},
                {5,2,3,4,3,2,2,4,3,2},
                {5,2,4,3,4,2,2,2,2,2},
                {5,2,2,2,2,2,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {2,2,2,2,5,5,5,5,5,5},
                {2,4,100,2,2,2,5,5,5,5},
                {2,1,50,3,4,2,2,2,2,2},
                {2,1,1,6,0,0,3,0,3,2},
                {2,2,4,3,4,3,4,3,4,2},
                {5,2,3,6,3,2,2,4,3,2},
                {5,2,4,3,4,2,2,2,2,2},
                {5,2,2,2,2,2,5,5,5,5},
            };
        }
        else if (levelplay == 82)
        {
            mapSize = new Vector2(8, 10);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,5,5},
                {5,2,3,4,3,4,3,2,2,2},
                {5,2,4,3,4,3,4,3,4,2},
                {2,2,2,4,3,4,3,4,3,2},
                {2,3,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,2,2,2,4,3,4,2,2,2},
                {5,5,5,2,2,2,2,2,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,5,5},
                {5,2,3,4,1,4,1,2,2,2},
                {5,2,4,1,4,1,4,1,4,2},
                {2,2,2,4,6,6,6,6,3,2},
                {2,3,4,3,100,0,4,0,4,2},
                {2,4,3,4,0,0,3,0,3,2},
                {2,2,2,2,4,3,4,2,2,2},
                {5,5,5,2,2,2,2,2,5,5},
            };
        }
        else if (levelplay == 83)
        {
            mapSize = new Vector2(8,9);
            newAray = new int[,]
            {
                {5,2,2,2,2,2,2,2,5},
                {5,2,3,4,3,4,3,2,5},
                {2,2,4,3,4,3,4,2,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,3,4,3,2},
                {2,4,3,4,3,4,3,4,2},
                {2,3,4,3,4,2,2,2,2},
                {2,2,2,2,2,2,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,2,2,2,5},
                {5,2,3,4,3,4,3,2,5},
                {2,2,4,6,6,6,0,2,2},
                {2,1,0,4,3,4,100,4,2},
                {2,3,1,1,4,6,0,3,2},
                {2,1,6,6,3,4,0,4,2},
                {2,3,4,3,4,2,2,2,2},
                {2,2,2,2,2,2,5,5,5},
            };
        }
        else if (levelplay == 84)
        {
            mapSize = new Vector2(7,10);
            newAray = new int[,]
            {
                {5,2,2,2,2,5,5,5,5,5},
                {5,2,3,4,2,2,2,2,2,2},
                {2,2,4,3,4,3,4,3,4,2},
                {2,4,3,4,3,4,3,4,3,2},
                {2,3,4,3,4,2,2,2,2,2},
                {2,4,3,4,3,2,5,5,5,5},
                {2,2,2,2,2,2,5,5,5,5},
            };
            newAray1 = new int[,]
            {
                {5,2,2,2,2,5,5,5,5,5},
                {5,2,3,4,2,2,2,2,2,2},
                {2,2,4,3,4,3,0,3,4,2},
                {2,4,1,6,3,0,3,4,3,2},
                {2,3,1,6,0,2,2,2,2,2},
                {2,4,1,100,3,2,5,5,5,5},
                {2,2,2,2,2,2,5,5,5,5},
            };
        }
        for (int i = 0; i < (int)mapSize.x; i++)
        {
            for (int j = 0; j < (int)mapSize.y; j++)
            {
                levelAray[i, j] = newAray[i, j];
                levelAray1[i, j] = newAray1[i, j];
            }
        }
    }

    public void checkWin()
    {
        int pass = 0;
        for(int i = 0; i < d; i++)
        {
            for(int j = 0; j < d; j++)
            {
                if (boxLv[i].posB.x == winP[j].x && boxLv[i].posB.y == winP[j].y)
                {
                    pass++;
                }
            }
        }
        if(pass == d)
        {
            Invoke("delayWin", 1);
            int index = levelplay + 1;
            PlayerPrefs.SetInt("isPass" + levelplay, 1);
            PlayerPrefs.SetInt("Level" + index, 1);
        }
    }
    
    public void delayWin()
    {
        panelEnd.SetActive(true);
    }

    public void check2box()
    {
        int dB = 0;
        int x, y, z, t;
        if (playerController.instance.d == true)
        {
            a = (int)player.pos.x;
            b = (int)player.pos.y;
            for (int i = 0; i < d; i++)
            {
                x = (int)boxLv[i].posB.x;
                y = (int)boxLv[i].posB.y;
                if(a == x-1 && b == y)
                {
                    for (int j = 0; j < d; j++)
                    {
                        z = (int)boxLv[j].posB.x;
                        t = (int)boxLv[j].posB.y;
                        if (x == z-1 && t == y)
                        {
                            dB++;
                        }
                    }
                }
                if(dB >= 1)
                {
                    playerController.instance.d = false;
                }
            }
        }
        else if (playerController.instance.l == true)
        {
            a = (int)player.pos.x;
            b = (int)player.pos.y;
            for (int i = 0; i < d; i++)
            {
                x = (int)boxLv[i].posB.x;
                y = (int)boxLv[i].posB.y;
                if (a == x && b == y+1)
                {
                    for (int j = 0; j < d; j++)
                    {
                        z = (int)boxLv[j].posB.x;
                        t = (int)boxLv[j].posB.y;
                        if (x == z && y == t+1)
                        {
                            dB++;
                        }
                    }
                }
                if (dB >= 1)
                {
                    playerController.instance.l = false;
                }
            }
        }
        else if (playerController.instance.r == true)
        {
            a = (int)player.pos.x;
            b = (int)player.pos.y;
            for (int i = 0; i < d; i++)
            {
                x = (int)boxLv[i].posB.x;
                y = (int)boxLv[i].posB.y;
                if (a == x && b == y - 1)
                {
                    for (int j = 0; j < d; j++)
                    {
                        z = (int)boxLv[j].posB.x;
                        t = (int)boxLv[j].posB.y;
                        if (x == z && y == t - 1)
                        {
                            dB++;
                        }
                    }
                }
                if (dB >= 1)
                {
                    playerController.instance.r = false;
                }
            }
        }
        else if (playerController.instance.u == true)
        {
            a = (int)player.pos.x;
            b = (int)player.pos.y;
            for (int i = 0; i < d; i++)
            {
                x = (int)boxLv[i].posB.x;
                y = (int)boxLv[i].posB.y;
                if (a == x+1 && b == y)
                {
                    for (int j = 0; j < d; j++)
                    {
                        z = (int)boxLv[j].posB.x;
                        t = (int)boxLv[j].posB.y;
                        if (x == z+1 && y == t)
                        {
                            dB++;
                        }
                    }
                }
                if (dB >= 1)
                {
                    playerController.instance.u = false;
                }
            }
        }
    }

}
