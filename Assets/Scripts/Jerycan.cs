using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerycan : MonoBehaviour
{
    

    [SerializeField]
    float restoreAmount = 50f;

    FuelHandler fuelHandler;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        fuelHandler = player.GetComponentInChildren<FuelHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "carro")
        {
            fuelHandler.AddFuel(restoreAmount);
        }
    }
}
