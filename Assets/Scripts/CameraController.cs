using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float cameraSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
