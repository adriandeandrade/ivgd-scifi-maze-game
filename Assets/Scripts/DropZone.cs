using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropZone : MonoBehaviour
{
    [SerializeField] private float amountOfKeys;

    public GameObject player;

    [SerializeField] private GameObject rocket;
    [SerializeField] private float speed;
    private bool allKeys = false;

    private void Update()
    {
        Collider[] keys = Physics.OverlapSphere(transform.position, 0.5f);
        if(keys.Length == amountOfKeys)
        {
            allKeys = true;
        }

        if(allKeys)
        {
            //Destroy(player);
            LaunchRocket();
        }
    }


    private void LaunchRocket()
    {
        rocket.transform.Translate(Vector3.up * speed * Time.deltaTime);
        Invoke("EndGame", 3f);
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
