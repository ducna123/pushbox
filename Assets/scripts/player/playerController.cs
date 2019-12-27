using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public bool l, r, d, u;
    float x = 0;
    float y = 0;
    public int d1, d2, d3, d4;
    Vector3 target;
    public bool move;
    private Rigidbody2D myBody;
    public static playerController instance;
    public Vector2 pos = new Vector2(0,0);
    public Map map;
    int time;
    Vector2 mousePos1;
    Vector2 mousePos2;
    bool tap;
    private bool isDraging = false;
    public AudioSource sound;
    public AudioClip moveS;

    // Use this for initialization
    void Start()
    {
        d1 = d2 = d3 = d4 = 1;
        move = false;
        myBody = GetComponent<Rigidbody2D>();
        _MakeInstance();
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
        movePlayer();
        if(l == true)
        {
            time++;
            if (d1 == 0)
            {
                int a = (int)pos.x;
                int b = (int)pos.y;
                x = map.Aray[a, b - 1].transform.position.x;
                y = map.Aray[a, b - 1].transform.position.y;
                
                d1 = 1;
            }
            if(time >= 7)
            {
                //sound.PlayOneShot(moveS);
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(x, y), Time.deltaTime * 5.5f);
                if (transform.position.x == x)
                {
                    pos = new Vector2(pos.x, pos.y - 1);
                    l = false;
                    time = 0;
                }
            }
            
        }
        else if (r == true)
        {
            time++;
            if (d2 == 0)
            {
                int a = (int)pos.x;
                int b = (int)pos.y;
                x = map.Aray[a, b + 1].transform.position.x;
                y = map.Aray[a, b + 1].transform.position.y;
                d2 = 1;
            }
            if(time >= 7)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(x, y), Time.deltaTime * 5.5f);
                if (transform.position.x == x)
                {
                    pos = new Vector2(pos.x, pos.y + 1);
                    r = false;
                    time = 0;
                }
            }           
        }
        else if (d == true)
        {
            time++;
            if (d3 == 0)
            {
                int a = (int)pos.x;
                int b = (int)pos.y;
                x = map.Aray[a+1, b].transform.position.x;
                y = map.Aray[a+1, b].transform.position.y;
                d3 = 1;
            }
            if(time >= 7)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(x, y), Time.deltaTime * 5.5f);
                if (transform.position.y == y)
                {
                    pos = new Vector2(pos.x+1,pos.y);
                    d = false;
                    time = 0;
                }
            }   
        }
        else if (u == true)
        {
            time++;
            if (d4 == 0)
            {
                int a = (int)pos.x;
                int b = (int)pos.y;
                x = map.Aray[a-1, b].transform.position.x;
                y = map.Aray[a-1, b].transform.position.y;
                
                d4 = 1;
            }
            if(time >= 7)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(x, y), Time.deltaTime * 5.5f);
                if (transform.position.y == y)
                {
                    pos = new Vector2(pos.x - 1, pos.y);
                    u = false;
                    time = 0;
                }
            }  
        }
    }

    public void movePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            tap = true;
            mousePos1 = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                mousePos1 = Input.touches[0].position;
            }
            else
            {
                if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    Reset();
                }
            }
        }

        mousePos2 = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                mousePos2 = Input.touches[0].position - mousePos1;
            }
            else
            {
                mousePos2 = (Vector2)Input.mousePosition - mousePos1;
            }
        }


        if (mousePos2.magnitude > 10)
        {
            float x = mousePos2.x;
            float y = mousePos2.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    _Left();
                }
                else
                {
                    _Right();
                }
            }
            else
            {
                if (y < 0)
                {
                    _Down();
                }
                else
                {
                    _Up();
                }
            }
            Reset();
        }
    }

    private void Reset()
    {
        isDraging = false;
        mousePos1 = mousePos2 = Vector2.zero;
    }

    public void _Up()
    {
        if(u == false && l == false && d == false && r == false)
        {
            int a = (int)pos.x;
            int b = (int)pos.y;
            if (map.levelAray1[a-1, b] != 2 && map.levelAray1[a - 1, b] != 6)
            {
                u = true;
                d4 = 0;
            }
            else
            {
                u = false;
            }
        } 
    }
    public void _Down()
    {
        if(u == false && l == false && d == false && r == false)
        {
            int a = (int)pos.x;
            int b = (int)pos.y;
            if (map.levelAray1[a+1, b] != 2 && map.levelAray1[a + 1, b] != 6)
            {
                d = true;
                d3 = 0;
            }
            else
            {
                d = false;
            }
        }
    }
    public void _Left()
    {
        if(u == false && l == false && d == false && r == false)
        {
            int a = (int)pos.x;
            int b = (int)pos.y;
            if (map.levelAray1[a, b - 1] != 2 && map.levelAray1[a, b - 1] != 6)
            {
                l = true;
                d1 = 0;
            }
            else
            {
                l = false;
            }
        } 
    }
    public void _Right()
    {
        if(u == false && l == false && d == false && r == false)
        {
            int a = (int)pos.x;
            int b = (int)pos.y;
            if(map.levelAray1[a,b+1] != 2 && map.levelAray1[a, b + 1] != 6)
            {
                r = true;
                d2 = 0;
            }
            else
            {
                r = false;
            }
        }
    }

}
