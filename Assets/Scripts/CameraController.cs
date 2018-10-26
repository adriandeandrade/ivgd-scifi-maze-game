using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform target;

    [Header("Camera Options")]
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float mouseSensitivtyX;
    [SerializeField] private float mouseSensitivtyY;
    [SerializeField] private float viewAngle;

    private void LateUpdate()
    {
        // Set the position of the camera to always be on top of the player.
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed);

        transform.eulerAngles = target.eulerAngles;
    }
}
