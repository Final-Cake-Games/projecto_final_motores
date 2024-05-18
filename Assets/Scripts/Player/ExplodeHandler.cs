using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeHandler : MonoBehaviour
{

    [SerializeField]
    GameObject originalObject;

    [SerializeField]
    GameObject model;

    Rigidbody[] rigidbodies;

    public bool exploded = false;


    void Awake()
    {
        rigidbodies = model.GetComponentsInChildren<Rigidbody>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Explode(Vector3.forward);
        //exploded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(float externalForce)
    {

        originalObject.SetActive(false);

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.transform.parent = null;

            rb.GetComponent<MeshCollider>().enabled = true;

            rb.gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            
            rb.AddForce(Vector3.forward * externalForce * 100, ForceMode.Force);
            rb.AddTorque(Random.insideUnitSphere * 50f, ForceMode.Impulse);

            exploded = true;
        }

    }
}
