using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Shooting))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Player Variables")]
    [SerializeField] public float health;
    [SerializeField] private GameObject jumpParticleEffect;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private bool useLookAt = false;

    [Header("Movement Options")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float mouseSensitivtyX;
    [SerializeField] private float mouseSensitivtyY;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip shootSound;
    [SerializeField] public AudioClip walkSound;
    [SerializeField] public AudioClip dieSound;

    [HideInInspector] public Vector3 moveDirection;

    [SerializeField] public bool hasObject = false;

    private CharacterController characterController;
    private Shooting shootingController;
    private GameObject currentObject;
    private AudioSource audioData;

    private GameManager gameManager;

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Gets the character controller component
        shootingController = GetComponent<Shooting>();
        gameManager = FindObjectOfType<GameManager>();
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0))
        {
            shootingController.Shoot();
            audioData.PlayOneShot(shootSound);
        }

        if (useLookAt)
        {
            if (!hasObject)
            {
                LookAtObject();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentObject.transform.parent = null;
                    hasObject = false;
                    currentObject = null;
                }
            }
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
                audioData.PlayOneShot(jumpSound);
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

    public void TakeDamage(float amountOfDamage)
    {
        if (health <= 0)
        {
            audioData.PlayOneShot(dieSound);
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
            Destroy(gameObject);

        }

        health -= amountOfDamage;

    }

    private void LookAtObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Key key = hit.collider.GetComponent<Key>();
            if (key != null)
            {
                if (Vector3.Distance(transform.position, key.transform.position) < 15f)
                {
                    UIManager.instance.ActivateUI("pressButton");
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        UIManager.instance.DeactivateUI("pressButton");
                        key.transform.SetParent(grabPoint);
                        currentObject = key.gameObject;
                        hasObject = true;
                    }
                }
            }
            else
            {
                UIManager.instance.DeactivateUI("pressButton");
            }
        }
    }
}
