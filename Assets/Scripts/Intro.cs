using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject shakeTarget;

    [Header("Colors")]
    [SerializeField] private Color greenColor;
    [SerializeField] private Color emissionColor;

    [Header("Variables")]
    [SerializeField] private float amount;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioData;
    [SerializeField] private AudioClip explosionSound;

    [Header("UI")]
    [SerializeField] private GameObject shootButtonUI;

    private Vector3 shakeVec;
    private bool isPlaying;
    private bool canOpenDoor;
    private Renderer buttonRend;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        buttonRend = button.GetComponent<Renderer>();
        Invoke("IntroStart", 10f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canOpenDoor)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.GetComponent<Button>())
                {
                    Invoke("ChangeScene", 1.5f);
                }
            }
        }
    }

    private void IntroStart()
    {
        shakeVec = new Vector3(amount, amount, amount);
        iTween.ShakePosition(shakeTarget, iTween.Hash("name", "CameraShake", "amount", shakeVec, "time", amount * 2, "islocal", true));
        //audioData.PlayOneShot(explosionSound);
        Invoke("ChangeButtonColor", 10f);
        
    }

    private void ChangeButtonColor()
    {
        canOpenDoor = true;
        buttonRend.material.SetColor("_EmissionColor", emissionColor);
        buttonRend.material.SetColor("_Color", greenColor);
        UIManager.instance.ActivateUI("shootButton");
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
}
