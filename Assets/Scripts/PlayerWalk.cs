using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{
    // Player's movement attibutes
    public int playerSpeed;
    public float jumpSpeed;
    // Player's audio attibutes
    public AudioSource audioSource;
    // Control attributes
    public float speed = 10f;
    private float gravity = 10f;
    private CharacterController controller;
    // Grab attributes
    private int count;
    public Text countText;
    public Text winText;
    // Game timmer attributes
    float currCountdownValue = 90f;
    bool looseGame = false;
    public Text timeCount;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        count = 0;
        winText.text = "";
        SetCountText();
        controller = GetComponent<CharacterController>();
        speed = 20f;
        gravity = 150f;
        playerSpeed = 10;
        jumpSpeed = 100;
        currCountdownValue = 90f;
        looseGame = false;
        timeCount.text = currCountdownValue.ToString();


        StartCoroutine(StartCountdown());

    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Devuelve verdadero mientras buttonNamese mantiene presionado (Fire1 es para mouse).
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("PRESIONADO_FIRE_1");
            // Transforma la posicion, con la camara hacia adelante, velocidad del jugador y tiempo.
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        }
        else
        {
            // Audio de pasos.
            audioSource.Play();
        }
        PlayerMovement();
        ifGameLoose();
    }

    /// <summary>
    /// Get the game's object and collect it to add in the counter
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puntos"))
        {
            Destroy(other.gameObject);
            count++;
            SetCountText();
        }
    }

    /// <summary>
    /// Get the actual total score accumulated
    /// </summary>
    public void SetCountText()
    {
        countText.text = "Points: " + count.ToString();
        if (count >= 10 && !looseGame)
        {
            winText.text = "You Win";
            timeOut(50f);
            SceneManager.LoadScene("Menu");
        }

    }

    /// <summary>
    /// Gamepad's movement to make the player move accross the map
    /// </summary>
    private void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpSpeed;
            controller.Move(velocity);
        }
        audioSource.Play();
    }

    public IEnumerator StartCountdown(float countdownValue = 90)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > -1)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            timeCount.text = currCountdownValue.ToString();
            if (currCountdownValue == 0 && count < 10)
            {
                looseGame = true;
            }
        }
    }

    private void ifGameLoose()
    {
        if (looseGame)
        {
            float countdown = 60f;
            winText.text = "You loose";
            timeOut(100f);
            SceneManager.LoadScene("Menu");
        }
    }

    private void timeOut(float time)
    {
        float countdown = time;
        do
        {
            Debug.Log("RETURNING" + countdown);
            countdown -= Time.deltaTime;
        } while (countdown > 0);
    }

}