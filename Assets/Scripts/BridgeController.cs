using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private HingeJoint2D joint;
    private float targetAngle;
    private const float treshold = 2f; //how much roughly equal

    void Start()
    {
        GameEvents.Instance.onBridgeTriggerEnter += OnBridgeOpen;
        joint = GetComponent<HingeJoint2D>();
        targetAngle = joint.limits.min;
    }

    private void FixedUpdate() {
        if(RoughlyEqual(joint.jointAngle, targetAngle))
        {
            // targetAngle = targetAngle == joint.limits.min ? joint.limits.max : joint.limits.min; // uncomment if need multiple controls <- need additions
            // joint.connectedBody.bodyType = RigidbodyType2D.Static; // uncomment to make bridge unmovable
            this.enabled = false;
        }
    }

    static bool RoughlyEqual(float a, float b) {
        return (Mathf.Abs(a-b) <= treshold);
    }

    private void OnBridgeOpen()
    {
        joint.connectedBody.bodyType = RigidbodyType2D.Dynamic;
        JointMotor2D m = joint.motor;
        m.motorSpeed *= -1;
        joint.motor= m;
        GameEvents.Instance.onBridgeTriggerEnter -= OnBridgeOpen;
    }

    // private void OnDestroy() {
    // }
}
