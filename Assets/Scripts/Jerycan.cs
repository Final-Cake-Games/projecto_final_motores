using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerycan : MonoBehaviour
{
    [SerializeField]
    float restoreAmount = 50f;

    AudioSource sfx;

    FuelHandler fuelHandler;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        fuelHandler = player.GetComponentInChildren<FuelHandler>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "carro")
        {
            GameObject tank = GameObject.FindWithTag("Fuel");
            sfx.Play();
            tank.SetActive(false);
            fuelHandler.AddFuel(restoreAmount);
        }
    }
}
