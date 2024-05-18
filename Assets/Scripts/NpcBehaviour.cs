using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcBehaviour : MonoBehaviour
{
    Vector3 spawnPosition;
    bool canMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.parent.transform.position;
    }

    void OnEnable()
    {
        spawnPosition = transform.parent.transform.position;
        transform.position = spawnPosition;
        canMove = true;
    }

    void OnDisable()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.position += new Vector3(0, 0, 0.5f) * Time.deltaTime;
    }
}
