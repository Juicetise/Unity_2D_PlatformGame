using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class playerScript_ex02 : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] exits;
    public GameObject cam;
    private int playernum;
    //private float speed;
    private Rigidbody2D rb2d;
    private float upForce = 300f;
    private int bluejump = 0;
    private int redjump = 0;
    private int yellowjump = 0;
    private float[] speed;
    private bool exited;
    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
        exits = GameObject.FindGameObjectsWithTag("exit").OrderBy(go => go.name).ToArray();
        cam = GameObject.Find("Main Camera");
        //GameObject.Find("Main Camera").GetComponent<camscript>().player = players[0];
        cam.GetComponent<camscript>().player = players[0];
        exited = false;

        rb2d = players[0].GetComponent<Rigidbody2D>();
        playernum = 0;
        speed = new float[3];
        speed[0] = 0.2f;
        speed[1] = 0.4f;
        speed[2] = 0.6f;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name == exits[playernum].name)
            exited = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == exits[playernum].name)
        {
            Debug.Log(players[playernum].name + " has exited !");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "Player")
        {
            if (collision.otherCollider.name == "blue")
                bluejump = 1;
            if (collision.otherCollider.name == "red")
                redjump = 1;
            if (collision.otherCollider.name == "yellow")
                yellowjump = 1;
        }
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

    // Update is called once per frame
    void Update()
    {
        if (players[0].GetComponent<playerScript_ex02>().exited == true && players[1].GetComponent<playerScript_ex02>().exited == true && players[2].GetComponent<playerScript_ex02>().exited == true)
            SceneManager.LoadScene(3);
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKey("1"))
        {
            playernum = 0;
            cam.GetComponent<camscript>().player = players[playernum];
            rb2d = players[0].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("2"))
        {
            playernum = 1;
            cam.GetComponent<camscript>().player = players[playernum];
            rb2d = players[1].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("3"))
        {
            playernum = 2;
            cam.GetComponent<camscript>().player = players[playernum];
            rb2d = players[2].GetComponent<Rigidbody2D>();
        }
        if (Input.GetKey("right"))
            players[playernum].transform.Translate(Vector3.right * speed[playernum] * Time.deltaTime);
        if (Input.GetKey("left"))
            players[playernum].transform.Translate(Vector3.left * speed[playernum] * Time.deltaTime);
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
    }
}