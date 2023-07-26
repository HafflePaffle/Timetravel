using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas UI;

    void Start()
    {
        UI.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        if(UI.gameObject.activeSelf)
        {
            UI.gameObject.SetActive(false);
        }

    }

    void OnDisable()
    {
        if (!UI.gameObject.activeSelf)
        {
            UI.gameObject.SetActive(true);
        }

    }

    
}
