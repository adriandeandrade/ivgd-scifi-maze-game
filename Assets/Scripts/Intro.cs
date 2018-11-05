using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private Vector3 shakeVec;
    [SerializeField] private float amount;
    [SerializeField] private GameObject shakeTarget;

    [SerializeField] private AudioSource audioData;
    [SerializeField] private AudioClip explosionSound;

    [SerializeField] private GameObject button;
    [SerializeField] private Color greenColor;
    

    [SerializeField] private bool canOpenDoor;
    private bool isPlaying;
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
        iTween.ShakePosition(shakeTarget, iTween.Hash("name", "CameraShake", "amount", shakeVec, "time", amount * 2));
        audioData.PlayOneShot(explosionSound);
        Invoke("ChangeButtonColor", 10f);
        
    }

    private void ChangeButtonColor()
    {
        canOpenDoor = true;
        buttonRend.material.SetColor("_Emission", greenColor);
        buttonRend.material.SetColor("_Color", greenColor);
    }

    private void ChangeScene()
    {
        SimpleSceneFader.ChangeSceneWithFade("AdrianTest");
    }
}
