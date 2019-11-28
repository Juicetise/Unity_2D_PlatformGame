using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class playerScript_ex04 : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] exits;
    public GameObject cam;
    public GameObject[] tpout;
    public GameObject[] tpin;
    public GameObject[] doors;
    public GameObject[] switches;
    public GameObject[] floors;
    private int playernum;
    private Rigidbody2D rb2d;
    private float upForce = 300f;
    private int bluejump = 0;
    private int redjump = 0;
    private int yellowjump = 0;
    private float[] speed;
    private bool exited;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").OrderBy(go => go.name).ToArray();
        exits = GameObject.FindGameObjectsWithTag("exit").OrderBy(go => go.name).ToArray();
        tpout = GameObject.FindGameObjectsWithTag("tpout").OrderBy(go => go.name).ToArray();
        tpin = GameObject.FindGameObjectsWithTag("tpin").OrderBy(go => go.name).ToArray();
        doors = GameObject.FindGameObjectsWithTag("door").OrderBy(go => go.name).ToArray();
        switches = GameObject.FindGameObjectsWithTag("switch").OrderBy(go => go.name).ToArray();
        floors = GameObject.FindGameObjectsWithTag("floor").OrderBy(go => go.name).ToArray();
        cam = GameObject.Find("Main Camera");

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
    void OnTriggerExit2D(Collider2D collider2)
    {
        if (collider2.name == switches[0].name)
        {
            switches[0].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == exits[playernum].name)
        {
            Debug.Log(players[playernum].name + " has exited !");
        }
        if (collision.name == switches[0].name){
            doors[playernum].GetComponent<door>().activated = true;
            if (playernum == 0)
                switches[0].GetComponent<SpriteRenderer>().color = Color.blue;
            if (playernum == 1)
                switches[0].GetComponent<SpriteRenderer>().color = Color.red;
            if (playernum == 2)
                switches[0].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (collision.name == switches[playernum + 1].name){
            floors[playernum].layer = 0;
            floors[playernum].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "Player" || collision.collider.tag == "floor")
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
        if (players[0].GetComponent<playerScript_ex04>().exited == true && players[1].GetComponent<playerScript_ex04>().exited == true && players[2].GetComponent<playerScript_ex04>().exited == true)
            SceneManager.LoadScene(5);
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(4);
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