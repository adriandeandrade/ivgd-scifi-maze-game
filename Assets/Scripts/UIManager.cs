using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject pressButtonUI;

    private Dictionary<string, GameObject> uiObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        PopulateList();
    }

    private void PopulateList()
    {
        uiObjects.Add("pressButton", pressButtonUI);
    }

    public void ActivateUI(string panelName)
    {
        GameObject panel;
        if (uiObjects.TryGetValue(panelName, out panel))
            panel.SetActive(true);
    }

    public void DeactivateUI(string panelName)
    {
        GameObject panel;
        if (uiObjects.TryGetValue(panelName, out panel))
            panel.SetActive(false);
    }
}
