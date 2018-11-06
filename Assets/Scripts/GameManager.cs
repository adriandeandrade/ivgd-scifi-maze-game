using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnLocation;
    [SerializeField] private Vector3 cameraPos;

    [SerializeField] private GameObject playerPrefab;
    private GameObject cam;

    private void Start()
    {
        playerPrefab = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        CheckBounds();
    }

    public void Respawn()
    {
        GameObject player = Instantiate(playerPrefab, respawnLocation.position, Quaternion.identity);
        player.GetComponent<PlayerController>().health = 10f;
    }

    private void CheckBounds()
    {
        if(playerPrefab.transform.position.y < -200f)
        {
            Scene loadedScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedScene.buildIndex);
        }
    }
}
