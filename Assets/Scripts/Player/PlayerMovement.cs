using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float maxSpeed = 15f; // Metros por segundo
    [SerializeField]
    float startSpeed = 2.5f; // Metros por segundo
    [SerializeField]
    float steerSpeed = 150f;
    [SerializeField]
    float maxRotationAngle = 30f;

    float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startSpeed; // Aumentar até max speed gradualmente
    }

    // Update is called once per frame
    void Update()
    {
        Steer();
    }

    void FixedUpdate()
    {
        // Verifica se crashou para destruir o carro caso contrario moveforward
        MoveForward();
    }


    void Steer() // Função responsável pela direção do carro.
    {
        float yRotation = transform.eulerAngles.y;
 
        /*  
            Se o ângulo do eixo Y for maior que o máximo permitido para a esquerda (360º - angulo máximo) OU 
            inferior ao ângulo máximo permitido para a direita (angulo máximo)
            atualiza yRotation com input. (Está dentro do espectrum de ângulos permitidos).
        */   
        if (yRotation <= maxRotationAngle || yRotation >= 360f - maxRotationAngle)
        {
            if (Input.GetKey("a"))
                yRotation += -steerSpeed * Time.deltaTime;

            if (Input.GetKey("d"))
                yRotation += steerSpeed * Time.deltaTime;
        }
        /*
            Se o ângulo atual superar o máximo direita e for inferior a 180º
            manter valor dentro do espetrum permitido ao lado direito.
        */
        else if (yRotation > maxRotationAngle && yRotation <180f)
            yRotation = maxRotationAngle - 0.1f;
        // Caso superior a 180º e inferior ao máximo esquerda então manter valor máximo lado ESQUERDO.
        else if (yRotation > 180f && yRotation < 360f - maxRotationAngle)
            yRotation = 360f - maxRotationAngle + 0.1f;

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f); // Atualizar a rotação.
    }

    void MoveForward()
    {
        rb.velocity = transform.forward * currentSpeed;
    }
}
