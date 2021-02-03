using UnityEngine;
using System;

public class Sight: Sense
{
    protected int FieldOfView { get; private set; }
    protected float ViewDistance { get; private set; }

    [SerializeField] private LayerMask whatCanBeSeen;
    [SerializeField] private Transform sightSourcePoint;
    private Transform[] playerParts = new Transform[3];
    private Vector2 rayDirection;
    private Color colorOfSight = Color.yellow;
    [HideInInspector] public Vector3 lastSeenPlayerPosition = Vector3.zero;

    protected override void Initialize()
    {
        base.Initialize();

        sightSourcePoint    = sightSourcePoint ?? transform;
        FieldOfView         = manager.FieldOfView; 
        DetectionRate       = manager.DetectionRate;
        ViewDistance        = manager.ViewDistance;

        playerParts[0]      = GameObject.FindGameObjectWithTag("Player").transform;
        playerParts[1]      = playerParts[0].Find("GroundCheck");
        playerParts[2]      = playerParts[0].Find("CeilingCheck");
    }

    protected override void UpdateSense()
    {
        base.UpdateSense();

        #if UNITY_EDITOR
        FieldOfView     = manager.FieldOfView;
        ViewDistance    = manager.ViewDistance;
        #endif

        if(elapsedTime >= DetectionRate)
        {
            elapsedTime = 0;
            DetectAspect();
        }
    }

    void DetectAspect()
    {
        colorOfSight = Color.yellow;
        
        foreach (Transform playerTransform in playerParts)
        {
            rayDirection = playerTransform.position - sightSourcePoint.position;

            if(Vector2.Angle(rayDirection, sightSourcePoint.right * transform.root.localScale.x) < FieldOfView * 0.5f)
            {
                RaycastHit2D hit = Physics2D.Raycast(sightSourcePoint.position, rayDirection, ViewDistance, whatCanBeSeen);
                if(hit.collider != null)
                {
                    Aspect aspect = hit.collider.GetComponent<Aspect>();

                    if(aspect != null && aspect.aspectType != aspectName)
                    {
                        // TODO: add actual actions
                        colorOfSight = Color.red;
                        lastSeenPlayerPosition = hit.collider.transform.position;
                        Debug.Log($"Detected: {lastSeenPlayerPosition}");
                    }
                }
            }
        }
    }

    #region OtherDetectionAlgorythm
    // void DetectAspect()
    // {
    //     if(sightSourcePoint == null)
    //     {
    //         return;
    //     }
    //     colorOfSight = Color.yellow;

    //     for(int i = 0; i < 3; i++)
    //     {
    //         rayDirection = sightSourcePoint.right * transform.localScale.x;
    //         switch (i)
    //         {
    //             case 1:
    //                 rayDirection = ((rayDirection * ViewDistance).RotatePointAroundPivotByZAxis(sightSourcePoint.position , FieldOfView * 0.5f * transform.localScale.x)).normalized;
    //                 break;
    //             case 2:
    //                 rayDirection = ((rayDirection * ViewDistance).RotatePointAroundPivotByZAxis(sightSourcePoint.position , -FieldOfView * 0.5f * transform.localScale.x)).normalized;
    //                 break;
    //             default:
    //                 break;
    //         }
            

    //         RaycastHit2D hit = Physics2D.Raycast(sightSourcePoint.position, rayDirection, ViewDistance, whatCanBeSeen);
    //         if(hit.collider != null)
    //         {
    //             Aspect aspect = hit.collider.GetComponent<Aspect>();

    //             if(aspect != null && aspect.aspectType != aspectName)
    //             {
    //                 // TODO: add actual actions
    //                 colorOfSight = Color.red;
    //                 Debug.Log("Detected");
    //             }
    //         }
            
    //     }        
    // }
    #endregion

    void OnDrawGizmos()
    {
        if (playerParts[0] == null)
        {
            return;
        }

        foreach (Transform playerTransform in playerParts)
        {
            Debug.DrawLine(sightSourcePoint.position, playerTransform.position, Color.green);
        }

        Vector2 frontRayPoint = sightSourcePoint.position + (sightSourcePoint.right * transform.root.localScale.x * ViewDistance);
        
        //Approximate perspective visualization
        Vector2 upRayPoint = frontRayPoint.RotatePointAroundPivotByZAxis(sightSourcePoint.position , FieldOfView * 0.5f * transform.root.localScale.x);
        Vector2 downRayPoint = frontRayPoint.RotatePointAroundPivotByZAxis(sightSourcePoint.position , -FieldOfView * 0.5f * transform.root.localScale.x);

        Debug.DrawLine(sightSourcePoint.position, frontRayPoint, colorOfSight);
        Debug.DrawLine(sightSourcePoint.position, upRayPoint, colorOfSight);
        Debug.DrawLine(sightSourcePoint.position, downRayPoint, colorOfSight);
    }
}
