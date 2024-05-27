using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource hornSfx;

    float timer = 3f;
    bool cd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("f") && cd == false)
        {
            hornSfx.Play();
            cd = true;
        }

        if (cd == true && timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            cd = false;
            timer = 3f;
        }
    }
}
