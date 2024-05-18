using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeObject : MonoBehaviour
{
    [SerializeField]
    Vector3 localRotationMin = Vector3.zero;

    [SerializeField]
    Vector3 localRotationMax = Vector3.zero;

    [SerializeField]
    float localScaleMultiplierMin = 0.8f;

    [SerializeField]
    float localScaleMultiplierMax = 1.2f;


    Vector3 defaultScale = Vector3.one;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(Random.Range(localRotationMin.x, localRotationMax.x), Random.Range(localRotationMin.y, localRotationMax.y), Random.Range(localRotationMin.z, localRotationMax.z));
    
        transform.localScale = defaultScale * Random.Range(localScaleMultiplierMin, localScaleMultiplierMax);
    }

}
