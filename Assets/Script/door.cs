using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool activated;
    public float ymax;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == true)
        {
            if( transform.position.y < ymax){
                transform.Translate(Vector3.up * 0.6f * Time.deltaTime);
            }
        }
    }
}
