using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0;
    public float jumpForce = 10;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isGrounded; // create a bool for to check if ball is on ground
    private int jumpCount = 0; //intialize jumpcount at zero


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
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
                if (jumpCount < 2) // allow no more then two jumps
                {
                    for(int i = 0; i < 2; i++)
                        {
                            rb.velocity = new Vector3(rb.velocity.x,jumpForce,rb.velocity.z); // velocity required for jump
                            jumpCount++; //iterates jumpcount
                        }
                }
        }

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.0f); // sets the position of the ground relative to the ball

        if (isGrounded) // resets jump count if ball is oon ground
        {
            jumpCount = 0;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        
    }
}
