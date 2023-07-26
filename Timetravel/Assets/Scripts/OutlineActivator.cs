using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class OutlineActivator : MonoBehaviour
{
    public float radius = 6;
    private bool outlining;
    [SerializeField] ParticleSystem viewCircle;
    [SerializeField] GameObject colorVolume;
    public bool fadeIn = false;
    public bool fadeOut = false;
    [SerializeField] private CanvasGroup canvasGroup;    
    [SerializeField] private Volume volume;


    private void Start()
    {
        viewCircle.Pause();
        viewCircle.Clear();
        colorVolume.SetActive(false);
        volume.weight = 0f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && outlining == false)
        {
            outlining = true;
            InvokeRepeating("InvokeOutline",0, 0.02f);
            viewCircle.Play();
            colorVolume.SetActive(true);
            fadeIn = true;
            fadeOut = false;

        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && outlining == true) 
        { 
            stopOutline();
            viewCircle.Pause();
            viewCircle.Clear();
            fadeOut = true;
            fadeIn = false;
        }

        if(fadeIn == true) 
        { 
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if(canvasGroup.alpha >= 1) 
                {
                    fadeIn = false;
                }
            }
            if (volume.weight < 1)
            {
                volume.weight += Time.deltaTime;
            }
        }

        if (fadeOut == true && fadeIn == false)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                    colorVolume.SetActive(false);
                }
            }
            if (volume.weight >= 0)
            {
                volume.weight -= Time.deltaTime;
            }
        }
    }


    void OutlineActivate(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (Collider hit in hitColliders)
        {
            hit.SendMessage("ActivateOutline", SendMessageOptions.DontRequireReceiver);
            hit.SendMessage("DeactivateOutline", radius, SendMessageOptions.DontRequireReceiver);
        }
    }

    void InvokeOutline()
    {
        OutlineActivate(transform.position, radius);
    }

    void stopOutline()
    {
        outlining = false;
        CancelInvoke("InvokeOutline");
        removeOutline(transform.position, radius);
    }

    void removeOutline(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (Collider hit in hitColliders)
        {

            //hit.SendMessage("removeOutline", SendMessageOptions.DontRequireReceiver);
            OutlineObject outlineObject = hit.GetComponent<OutlineObject>();
            if (outlineObject != null)
            {
                outlineObject.removeOutline();
            }
        }
    }
}
