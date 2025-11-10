using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq.Expressions;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool playerGrounded;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }




    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerGrounded == true)
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f), ForceMode.Impulse);
            playerGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if statement to make any surface tagged "ground" set playerGrounded to true when the player collides with it
        if (collision.gameObject.tag == "Ground")
        {
            playerGrounded = true;
        }

    }
}
