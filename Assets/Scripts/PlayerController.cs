using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Unity.Jobs;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    // Declaring public variables
    public float speed;
    public float speedBoost;
    public float jumpHeight;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool playerGrounded;

    // Declaring private variables
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool boostActive = false;

    // Audio Variables
    private AudioSource source;
    public AudioClip pickupSound;
    public AudioClip playerBoost;
    public AudioClip jumpSound;





    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }




    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }


    void FixedUpdate()
    {
        // Player Movement
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Takes in user input of WASD and applies that to the x-axis and z-axis

        rb.AddForce(movement * speed); // Multiples players movement by the speed variable that is set in the unity inspector
    }

    private void Update()
    {
        // Checks if user is pressing space bar and that the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
        {
            // Jump mechanic 
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f), ForceMode.Impulse); // Adds force of jumpHeight set in unity inspector to the y-axis
            source.PlayOneShot(jumpSound, 1.0f); // Plays sound effect
            playerGrounded = false;

        }

   
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks object player collides which has the tag "PickUp"
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Player Pick Ups
            other.gameObject.SetActive(false); // Deactivate pick up player collided with

            source.PlayOneShot(pickupSound, 1.0f); // Plays sound effect

            count = count + 100; // Adds 100 to players points
            SetCountText(); // Updates text to display points
        }

        if (other.gameObject.CompareTag("MultPickUp"))
        {
            // Player Pick Ups
            other.gameObject.SetActive(false); // Deactivate pick up player collided with

            source.PlayOneShot(pickupSound, 1.0f); // Plays sound effect

            count = count * 2; // Adds 100 to players points
            SetCountText(); // Updates text to display points
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checks object player collides with has the tag "Ground"
        if (collision.gameObject.tag == "Ground")
        {
            playerGrounded = true; // Sets player grounded to true so the player is able to jump
        }

        // Checks object player collides with is named "Boost Pad"
        if (collision.gameObject.name == "Boost Pad")
        {
            // Player Boost
            speed *= speedBoost; // Multiples speed variable by speedBoost
            source.PlayOneShot(playerBoost, 0.5f); // Plays sound effect
            boostActive = true; 
            StartCoroutine(BoostDuration()); // Starts coroutine
        }

        if (collision.gameObject.name == "FinishLine")
        {
            winTextObject.SetActive(true);
        }
    }


    IEnumerator BoostDuration()
    {
        // Coroutine to stop boost
        Debug.Log("Start coroutine"); 
        // Checks that boost is active
        if (boostActive == true) 
        {
            yield return new WaitForSeconds(2.0f); // Waits for 2 seconds
            boostActive = false; 
            speed /= speedBoost; // Divides speed by speedBoost to set speed to base value
        }
        Debug.Log("End coroutine");
        Debug.Log("Current speed is " +speed);
    }
}
