using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerycan : MonoBehaviour
{
    [SerializeField]
    float restoreAmount = 50f;

    AudioSource sfx;

    FuelHandler fuelHandler;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        fuelHandler = player.GetComponentInChildren<FuelHandler>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "carro")
        {
            fuelHandler.AddFuel(restoreAmount);
            player.GetComponent<PlayerMovement>().PlayPop();
            GameObject tank = GameObject.FindWithTag("Fuel");
            tank.SetActive(false);
        }
    }
}
