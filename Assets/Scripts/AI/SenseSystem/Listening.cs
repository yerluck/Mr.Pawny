using UnityEngine;
using System;

public class Listening : Sense
{
    [HideInInspector] public Vector3 lastPlayerPosition = Vector3.zero;
    [SerializeField] private LayerMask whatCanBeHeard;
    private float listenDistance;
    private float detectionRate;

    protected override void Initialize()
    {
        base.Initialize();

        listenDistance      = manager.ListenDistance;
        detectionRate       = manager.DetectionRate;
    }

    protected override void UpdateSense()
    {
        if(!manager.IsListening)
        {
            return;
        }
        base.UpdateSense();

        #if UNITY_EDITOR
        listenDistance = manager.ListenDistance;
        #endif

        if(elapsedTime >= detectionRate)
        {
            elapsedTime = 0;
            DetectAspect();
        }
    }

    private void DetectAspect()
    {
        var hitCollider = Physics2D.OverlapCircle(transform.position, listenDistance, whatCanBeHeard);
        if(!hitCollider || !hitCollider.GetComponent<Protagonist>().isGrounded || hitCollider.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            return;
        }

        Aspect aspect = hitCollider.GetComponent<Aspect>();

        //TODO: add actual action when heard
        if(aspect && aspect.aspectType != aspectName)
        {
            lastPlayerPosition = hitCollider.transform.position;
            Debug.Log($"Player is being listened {lastPlayerPosition}");
        }
    }

    #region Deprecated
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(IsToExit(other))
    //     {
    //         return;
    //     }

    //     Aspect aspect = other.GetComponent<Aspect>();

    //     if(aspect != null && aspect.aspectType != aspectName)
    //     {
    //         lastPlayerPosition = other.transform.position;
    //         Debug.Log($"Player is being listened {lastPlayerPosition}");
    //     }    
    // }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if(IsToExit(other))
    //     {
    //         return;
    //     }

    //     Aspect aspect = other.GetComponent<Aspect>();

    //     if(aspect != null && aspect.aspectType != aspectName)
    //     {
    //         lastPlayerPosition = other.transform.position;
    //         Debug.Log($"Player is being listened: {lastPlayerPosition}");
    //     } 
    // }

    // private bool IsToExit(Collider2D other)
    // {
    //     if(manager?.IsListening == false)
    //     {
    //         return true;
    //     }

    //     if(other.GetComponent<Protagonist>()?.m_Grounded == false)
    //     {
    //         return true;
    //     }

    //     other.TryGetComponent(out Rigidbody2D rb);

    //     if(rb?.velocity == Vector3.zero)
    //     {
    //         return true;
    //     }


    //     return false;
    // }
    #endregion
}