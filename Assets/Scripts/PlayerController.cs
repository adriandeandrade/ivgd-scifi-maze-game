using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Options")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float mouseSensitivtyX;
    [SerializeField] private float mouseSensitivtyY;

    [Header("Transforms")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform shootPoint;

    [Header("GameObjects")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject bulletPrefab;

    [HideInInspector] public Vector3 moveDirection;
    private Vector3 aimPosition;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Gets the character controller component
    }

    private void Update()
    {
        Movement();

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
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
            }
        }

        verticalSpeed -= gravity * Time.deltaTime;
        moveDirection.y = verticalSpeed;

        HandleRotation();
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float rotationAmountX = Input.GetAxis("Mouse X") * mouseSensitivtyX * Time.deltaTime;
        transform.Rotate(new Vector3(0f, rotationAmountX, 0f));
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject bulletInstance = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Destroy(bulletInstance, 5f);
    }
}
