using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour
{
    public GameObject[] players;
    private int playernum;
    private float speed;
    private Rigidbody2D rb2d;
    private float upForce = 300f;
    private int bluejump = 0;
    private int redjump = 0;
    private int yellowjump = 0;
    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
        GameObject.Find("Main Camera").GetComponent<camscript>().player = players[0];
        rb2d = players[0].GetComponent<Rigidbody2D>();
        playernum = 0;
        speed = 0.4f;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "blue")
            bluejump = 1;
        if (collision.otherCollider.name == "red")
            redjump = 1;
        if (collision.otherCollider.name == "yellow")
            yellowjump = 1;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.name == "blue")
            bluejump = 0;
        if (collision.otherCollider.name == "red")
            redjump = 0;
        if (collision.otherCollider.name == "yellow")
            yellowjump = 0;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "PlatformWhiteSprite" && maxjump == 0)
        {
            Debug.Log("issou");
            maxjump++;
            Debug.Log(maxjump);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKey("1"))
        {
            playernum = 0;
            GameObject.Find("Main Camera").GetComponent<camscript>().player = players[playernum];
            rb2d = players[0].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("2"))
        {
            playernum = 1;
            GameObject.Find("Main Camera").GetComponent<camscript>().player = players[playernum];
            rb2d = players[1].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("3"))
        {
            playernum = 2;
            GameObject.Find("Main Camera").GetComponent<camscript>().player = players[playernum];
            rb2d = players[2].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("right"))
            players[playernum].transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey("left"))
            players[playernum].transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (Input.GetKeyDown("space") && playernum == 0 && bluejump == 1)
        {
            rb2d.AddForce(new Vector2(0, upForce));
        }
        if (Input.GetKeyDown("space") && playernum == 1 && redjump == 1)
        {
            rb2d.AddForce(new Vector2(0, upForce));
        }
        if (Input.GetKeyDown("space") && playernum == 2 && yellowjump == 1)
        {
            rb2d.AddForce(new Vector2(0, upForce));
        }
        //Debug.Log(playernum);
        //Debug.Log(bluejump);
    }
}
