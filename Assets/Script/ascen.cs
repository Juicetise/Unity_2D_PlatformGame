using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ascen : MonoBehaviour
{
    public float speed;
    public float ymax;
    public float ymin;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        if (transform.position.y > ymax || transform.position.y < ymin)
        {
            dir = new Vector3(dir.x, -dir.y, dir.z);
        }
    }
}
