using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int keyID;

    private void OnTriggerEnter(Collider other)
    {
        //transform.SetParent(other.transform);
    }
}
