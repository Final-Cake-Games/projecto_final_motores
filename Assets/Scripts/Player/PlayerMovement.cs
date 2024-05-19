using System.Numerics;
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
    float accelTime = 60f; // Segundos até maxSpeed
    [SerializeField]
    float steerSpeed = 150f;
    [SerializeField]
    float maxRotationAngle = 30f;

    bool playedCrash = false;

    float timeElapsed;
    public float currentSpeed;
    public float distanceTravaled = 0f;

    public ExplodeHandler explodeHandlerScript;
    
    [SerializeField]
    AudioClip engine;
    [SerializeField]
    AudioClip crash;
    
    AudioSource sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sfxPlayer = GetComponent<AudioSource>();
        sfxPlayer.clip = engine;
        sfxPlayer.Play();
        

        distanceTravaled = 0;
        currentSpeed = startSpeed; // Aumentar até max speed gradualmente
        explodeHandlerScript = GetComponent<ExplodeHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer();
    }

    void FixedUpdate()
    {
        // Verifica se crashou para destruir o carro caso contrario moveforward
        if (!explodeHandlerScript.exploded)
        {
            MoveForward();
            distanceTravaled += currentSpeed * Time.deltaTime / 1000;
        }
        else
        {
            rb.velocity = UnityEngine.Vector3.zero;
            currentSpeed = 0;
            
            
            sfxPlayer.loop = false;
            sfxPlayer.clip = crash;
            if (!playedCrash)
            {
                sfxPlayer.pitch = 1f;
                sfxPlayer.Play();
                playedCrash = true;
            }
            
        }

        if (currentSpeed < maxSpeed && !explodeHandlerScript.exploded)
            Accelerate(Time.deltaTime);
            if (currentSpeed < maxSpeed)
                sfxPlayer.pitch += 0.01f * Time.deltaTime;
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

        transform.rotation = UnityEngine.Quaternion.Euler(0f, yRotation, 0f); // Atualizar a rotação. 
    }

    void MoveForward()
    {
        // Utiliza a rotação aplicada em Steer() para obter a direção 'em frente' e multiplica essa direção pela velocidade atual
        rb.velocity = transform.forward * currentSpeed; 
    }

    void Accelerate(float deltaTime)
    {
        currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, timeElapsed / accelTime);
        timeElapsed += deltaTime;
    }


    // Eventos

    private void OnCollisionEnter(Collision collision)
    {
        explodeHandlerScript.Explode(currentSpeed);
    }
}
