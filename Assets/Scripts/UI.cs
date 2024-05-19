using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovementScript;

    [SerializeField]
    FuelHandler fuelHandlerScript;

    Label kmh;
    Label km;
    VisualElement gameOverScreen;

    Button restartButton;
    Button exitButton;

    ProgressBar fuel;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        kmh = root.Q<Label>("kmh");
        km = root.Q<Label>("km");
        gameOverScreen = root.Q<VisualElement>("GameOver");
        restartButton = root.Q<Button>("retry");
        exitButton = root.Q<Button>("exit");

        fuel = root.Q<ProgressBar>("fuel");
        fuel.visible = true;

        restartButton.clicked += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        exitButton.clicked += () => Application.Quit();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        kmh.text = (playerMovementScript.currentSpeed * 18 / 5).ToString("F0");
        km.text = playerMovementScript.distanceTravaled.ToString("F2");
        fuel.value = fuelHandlerScript.currentFuel;

        if (playerMovementScript.explodeHandlerScript.exploded)
        {
            fuel.visible = false;
            gameOverScreen.visible = true;
        }
    }
}
