using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcBehaviour : MonoBehaviour
{
    GameObject parentRoadSection;
    Vector3 startPosition;
    bool canMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        parentRoadSection = transform.parent.transform.parent.gameObject;
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.position += new Vector3(0, 0, 0.5f) * Time.deltaTime;

        if (!parentRoadSection.activeInHierarchy)
        {
            transform.position = startPosition;
            canMove = false;
        } 
        else
        {
            canMove = true;
        }
    }
}
