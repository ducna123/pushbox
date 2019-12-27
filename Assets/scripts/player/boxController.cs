using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour {
    private Rigidbody2D myBody;
    public static boxController instance;
    bool go;
    public bool l, r, down, u;
    public Vector2 posB = new Vector2(0,0);
    int t = 0;
    int a, b, c, d, e, f;
    float tx, ty;
    public bool moveR,moveL,moveU,moveD;
    int time;
    // Use this for initialization

    void Start () {
        moveR = true;
        moveL = true;
        moveU = true;
        moveD = true;
        a = b = c = d = e = f = 0;
        tx = ty = 0;
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
        if (playerController.instance.u == false && playerController.instance.d == false && playerController.instance.l == false && playerController.instance.r == false)
        {
            a = (int)playerController.instance.pos.x;
            b = (int)playerController.instance.pos.y;
        }
        checkWall();
        if (playerController.instance.r == true && a == posB.x && b == posB.y - 1)
        {
            time++;
            if (moveR == true)
            {
                int x = (int)posB.x;
                int y = (int)posB.y;
                tx = Map.instance.Aray[x, y + 1].transform.position.x;
                ty = Map.instance.Aray[x, y + 1].transform.position.y;
            }
            if (time >= 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(tx, ty), Time.deltaTime * 5.5f);
                if(transform.position.x == tx)
                {
                    time = 0;
                    posB = new Vector2(posB.x, posB.y + 1);
                }
                
            }
        }
        else if (playerController.instance.l == true && a == posB.x && b == posB.y + 1)
        {
            time++;
            if (moveL == true)
            {
                int x = (int)posB.x;
                int y = (int)posB.y;
                tx = Map.instance.Aray[x, y - 1].transform.position.x;
                ty = Map.instance.Aray[x, y - 1].transform.position.y;
            }
            if (time >= 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(tx, ty), Time.deltaTime * 5.5f);
                if(transform.position.x == tx)
                {
                    posB = new Vector2(posB.x, posB.y - 1);
                    time = 0;
                }  
            }
        }
        else if (playerController.instance.d == true && a == posB.x-1 && b == posB.y)
        {
            time++;
            if (moveD == true)
            {
                int x = (int)posB.x;
                int y = (int)posB.y;
                tx = Map.instance.Aray[x + 1, y].transform.position.x;
                ty = Map.instance.Aray[x + 1, y].transform.position.y; 
            }
            if (time >= 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(tx, ty), Time.deltaTime * 5.5f);
                if(transform.position.y == ty)
                {
                    posB = new Vector2(posB.x + 1, posB.y);
                    time = 0;
                } 
            }
        }
        else if (playerController.instance.u == true && a == posB.x + 1 && b == posB.y)
        {
            time++;
            if (moveU == true)
            {
                int x = (int)posB.x;
                int y = (int)posB.y;
                tx = Map.instance.Aray[x - 1, y].transform.position.x;
                ty = Map.instance.Aray[x - 1, y].transform.position.y;
            }
            if (time >= 6)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(tx, ty), Time.deltaTime * 5.5f);
                if(transform.position.y == ty)
                {
                    posB = new Vector2(posB.x - 1, posB.y);
                    time = 0;
                }
            }
        }

    }

    public void checkWall()
    {
        if (playerController.instance.r == true && a == posB.x && b == posB.y - 1)
        {
            e = (int)posB.x;
            f = (int)posB.y;
            if (a == e && b == f - 1 && Map.instance.levelAray1[e,f+1] == 2 || a == e && b == f - 1 && Map.instance.levelAray1[e, f + 1] == 6)
            {
                Debug.Log("stop");
                moveR = false;
                playerController.instance.r = false;
                posB = new Vector2(e, f);
                playerController.instance.pos = new Vector2(a,b);
            }
            else
            {
                moveR = true;
            }
        }
        else if (playerController.instance.l == true && a == posB.x && b == posB.y + 1)
        {
            e = (int)posB.x;
            f = (int)posB.y;
            if (a == e && b == f + 1 && Map.instance.levelAray1[e, f - 1] == 2 || a == e && b == f + 1 && Map.instance.levelAray1[e, f - 1] == 6)
            {
                moveL = false;
                playerController.instance.l = false;
                posB = new Vector2(e, f);
                playerController.instance.pos = new Vector2(a, b);
            }
            else
            {
                moveL = true;
            }
        }
        else if (playerController.instance.u == true && a == posB.x+1 && b == posB.y)
        {
            e = (int)posB.x;
            f = (int)posB.y;
            if ((a == e+1 && b == f && Map.instance.levelAray1[e-1,f] == 2) || (a == e + 1 && b == f && Map.instance.levelAray1[e-1, f] == 6))
            {
                Debug.Log("stop");
                moveU = false;
                playerController.instance.u = false;
                posB = new Vector2(e, f);
                playerController.instance.pos = new Vector2(a, b);
            }
            else
            {
                moveU = true;
            }
        }
        else if (playerController.instance.d == true && a == posB.x - 1 && b == posB.y)
        {
            e = (int)posB.x;
            f = (int)posB.y;
            if (a == e - 1 && b == f && Map.instance.levelAray1[e + 1, f] == 2 || a == e - 1 && b == f && Map.instance.levelAray1[e+1, f] == 6)
            {
                Debug.Log("stop");
                moveD = false;
                playerController.instance.d = false;
                posB = new Vector2(e, f);
                playerController.instance.pos = new Vector2(a, b);
            }
            else
            {
                moveD = true;
            }
        }
    }

   

}
