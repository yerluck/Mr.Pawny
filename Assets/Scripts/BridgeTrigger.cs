using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    //TODO: add actual action
    private void OnTriggerEnter2D(Collider2D other) {
        GameEvents.Instance.BridgeTriggerEnter();    
    }
}
