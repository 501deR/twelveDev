using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //controlCharacter
    public CharacterController controller;
    public float speed = 0f;
    
    //gravity
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    //animation
    public Animator animator;

    //movementMultipliers
    public float crouchHeight = 1.4f;
    public float standHeight = 2f;
    public float crouchSpeed = 2f;
    public float sprintSpeed = 8f;
    public float walkSpeed = 4f;
    public bool isCrouching = false;
    public bool isSprinting = false;

    //crouching
    public float height = 2f;
    CapsuleCollider playerCollider;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent <CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //groundCheck
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //movementCheck
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //sprintCheck
        if (Input.GetKey("left shift"))
        {
            isSprinting = true;
            speed = sprintSpeed;
        }
         else
        {
            isSprinting = false;
            speed = walkSpeed;
        }

        //crouchCheck
        if (Input.GetKey("left ctrl"))
        {
            isCrouching = true;
            speed = crouchSpeed;
            playerCollider.height = crouchHeight;
        }
        else
        {
            isCrouching = false;
            playerCollider.height = standHeight;
        }



        //movementCalculation
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
    }
}
