using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] public Transform target;

    [Header("Camera Options")]
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float damping = 5.0f;
    [SerializeField] private float mouseSensitivtyX;
    [SerializeField] private float mouseSensitivtyY;
    [SerializeField] private float viewAngle;
    [SerializeField] private Vector3 bumperRayOffset;
    [SerializeField] private float bumperDistanceCheck = 2.5f;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float height = 1.0f;
    [SerializeField] private float bumperCameraHeight = 1.0f;

    private void Update()
    {
        if (target != null)
        {
            transform.eulerAngles = target.eulerAngles;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Set the position of the camera to always be on top of the player.
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed);
        }
    }
}
