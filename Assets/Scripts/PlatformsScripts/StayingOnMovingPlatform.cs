using System.Collections.Generic;
using UnityEngine;

// TODO: in case if Physics matrix is not enough -> add LayerMask
[RequireComponent(typeof(Collider2D))] 
public class StayingOnMovingPlatform: MovingPlatform
{
    private List<Transform> children = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }
        other.transform.SetParent(transform);
        children.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.isTrigger)
        {
            return;
        }
        children.Remove(other.transform);
        other.transform.SetParent(null);
    }
}