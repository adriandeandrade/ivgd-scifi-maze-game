using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Shooting))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private int health;
    [SerializeField] private GameObject jumpParticleEffect;

    [Header("Movement Options")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float mouseSensitivtyX;
    [SerializeField] private float mouseSensitivtyY;

    [HideInInspector] public Vector3 moveDirection;
    private bool hasChip; // This is becomes true when you pickup the chip to allow you to shoot.

    private CharacterController characterController;
    private Shooting shootingController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Gets the character controller component
        shootingController = GetComponent<Shooting>();
    }

    private void Update()
    {
        Movement();

        if(Input.GetMouseButtonDown(0))
        {
            shootingController.Shoot();
        }
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = (transform.forward * moveVertical) + (transform.right * moveHorizontal);

        if (characterController.isGrounded)
        {
            verticalSpeed = 0;
            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                GameObject jumpEffect = Instantiate(jumpParticleEffect, transform.position, Quaternion.identity);
                Destroy(jumpEffect, 2.5f);
            }
        }
        
        verticalSpeed -= gravity * Time.deltaTime;
        moveDirection.y = verticalSpeed;

        HandleRotation();
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float rotationAmountX = Input.GetAxisRaw("Mouse X") * mouseSensitivtyX * Time.deltaTime;
        transform.Rotate(new Vector3(0f, rotationAmountX, 0f));
        float rotationAmountY = Input.GetAxisRaw("Mouse Y") * mouseSensitivtyY * Time.deltaTime;
        transform.Rotate(new Vector3(-rotationAmountY, 0f, 0f));
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }
}
