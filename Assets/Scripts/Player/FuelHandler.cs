using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelHandler : MonoBehaviour
{
    [SerializeField]
    ExplodeHandler explodeHandlerScript;

    [SerializeField]
    float lossAmountPerSecond = 10f;

    [SerializeField]
    float restoreAmount = 50f;

    float currentFuel;
    
    // Start is called before the first frame update
    void Start()
    {
        currentFuel = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentFuel);
        currentFuel -= lossAmountPerSecond * Time.deltaTime;

        if (currentFuel <= 0 && explodeHandlerScript.exploded == false)
            explodeHandlerScript.Explode(2.5f);

    }

    public void AddFuel(float amount)
    {
        Debug.Log(currentFuel);
        currentFuel += amount;
        Debug.Log(currentFuel);

        if (currentFuel > 100)
            currentFuel = 100;
    }

}
