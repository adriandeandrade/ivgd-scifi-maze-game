using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitColorChange : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] private Color newColor;
    private Color originalColor;
    private bool hasChanged;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        hasChanged = false;
    }

    private void Update()
    {

    }

    public void ChangeColor()
    {
        if (hasChanged)
        {
            rend.material.color = originalColor;
            hasChanged = false;
        }
        else
        {
            rend.material.color = newColor;
            hasChanged = true;
        }
    }


}
