using UnityEngine;
using System;

public class Listening : Sense
{
    [HideInInspector] public Vector3 lastPlayerPosition = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(IsToExit(other))
        {
            return;
        }

        Aspect aspect = other.GetComponent<Aspect>();

        if(aspect != null && aspect.aspectType != aspectName)
        {
            lastPlayerPosition = other.transform.position;
            Debug.Log($"Player is being listened {lastPlayerPosition}");
        }    
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(IsToExit(other))
        {
            return;
        }

        Aspect aspect = other.GetComponent<Aspect>();

        if(aspect != null && aspect.aspectType != aspectName)
        {
            lastPlayerPosition = other.transform.position;
            Debug.Log($"Player is being listened: {lastPlayerPosition}");
        } 
    }

    private bool IsToExit(Collider2D other)
    {
        if(manager?.IsListening == false)
        {
            return true;
        }

        if(other.GetComponent<Protagonist>()?.m_Grounded == false)
        {
            return true;
        }

        other.TryGetComponent(out Rigidbody2D rb);

        if(rb?.velocity == Vector3.zero)
        {
            return true;
        }


        return false;
    }
}