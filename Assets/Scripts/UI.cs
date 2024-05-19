using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovementScript;

    Label kmh;
    Label km;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        kmh = root.Q<Label>("kmh");
        km = root.Q<Label>("km");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        kmh.text = (playerMovementScript.currentSpeed * 18 / 5).ToString("F0");
        km.text = playerMovementScript.distanceTravaled.ToString("F2");
    }
}
