using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    private Outline outline;
    [SerializeField] Transform player;
    public int stopOutlineDistance;
    [SerializeField] TextMeshPro nameText;
    public bool nameActive;
    public bool outlineActive;
    private Color transparent;
    private float newTransparent = 0f;



    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0f;
        outline.enabled = false;

        transparent = new Color(1, 1, 1, 0);
        nameText.color = transparent;

    }

    private void FixedUpdate()
    {
        if(outline.enabled == true) 
        {
            if ((transform.position - player.position).magnitude >= stopOutlineDistance)
            {
                outlineActive = false;
                nameActive = false;
            }
        }
    }

    private void Update()
    {
        if(nameActive == true && transparent.a <1) 
        {
            newTransparent += Time.deltaTime;
            transparent = new Color(1, 1, 1, newTransparent);
            nameText.color = transparent;
        }

        if(nameActive == false && transparent.a > 0) 
        {
            newTransparent -= Time.deltaTime;
            transparent = new Color(1, 1, 1, newTransparent);
            nameText.color = transparent;
            if(transparent.a <= 0)
            {
                nameText.enabled = false;
            }
        }

        if(outlineActive == true && outline.OutlineWidth < 7f) 
        { 
            outline.OutlineWidth += Time.deltaTime * 7;
        }
        if (outlineActive == false && outline.OutlineWidth > 0f)
        {
            outline.OutlineWidth -= Time.deltaTime * 7;
            if(outline.OutlineWidth <= 0f)
            {
                outline.enabled = false;
            }
        }

    }

    private void ActivateOutline()
    {
        outline.enabled = true;
        outlineActive = true;
        if (nameActive == false)
        {
            nameText.enabled = true;
            nameActive = true;
        }

    }

    private void DeactivateOutline(int radius)
    {
        stopOutlineDistance = radius;
    }
    public void removeOutline()
    {
        outlineActive = false;
        nameActive = false;
    }
}
