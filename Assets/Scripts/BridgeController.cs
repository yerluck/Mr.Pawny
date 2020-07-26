using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private HingeJoint2D joint;
    private float targetAngle;

    void Start()
    {
        GameEvents.Instance.onBridgeTriggerEnter += OnBridgeOpen;
        joint = GetComponent<HingeJoint2D>();
        targetAngle = joint.limits.min;
        Debug.Log(targetAngle);
    }

    private void FixedUpdate() {
        if(RoughlyEqual(joint.jointAngle, targetAngle))
        {
            Debug.Log("done");
            targetAngle = targetAngle == joint.limits.min ? joint.limits.max : joint.limits.min;
            // joint.connectedBody.bodyType = RigidbodyType2D.Static;
            GameEvents.Instance.onBridgeTriggerEnter -= OnBridgeOpen;
            this.enabled = false;
        }
    }

    static bool RoughlyEqual(float a, float b) {
        float treshold = 2f; //how much roughly
        return (Mathf.Abs(a-b) <= treshold);
    }

    private void OnBridgeOpen()
    {
        joint.connectedBody.bodyType = RigidbodyType2D.Dynamic;
        JointMotor2D m = joint.motor;
        m.motorSpeed *= -1;
        joint.motor= m;
    }

    // private void OnDestroy() {
    // }
}
