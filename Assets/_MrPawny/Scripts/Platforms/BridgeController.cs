using System.Collections;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private HingeJoint2D joint;
    private float targetAngle;
    private const float treshold = 0.5f; //how much roughly equal
    [SerializeField] private GameObject lights;

    void Start()
    {
        GameEvents.Instance.onBridgeTriggerEnter += OnBridgeOpen;               // Subscribe on event
        joint = GetComponent<HingeJoint2D>();
        targetAngle = joint.limits.min;
    }

    private void FixedUpdate() {
        if(RoughlyEqual(joint.jointAngle, targetAngle))
        {
            // targetAngle = targetAngle == joint.limits.min ? joint.limits.max : joint.limits.min; // uncomment if need multiple controls <- need additions
            joint.connectedBody.bodyType = RigidbodyType2D.Static; // uncomment to make bridge unmovable
            StartCoroutine(LigthsOn());
            this.enabled = false;
        }
    }

    private IEnumerator LigthsOn() {
        yield return new WaitForSeconds(0.4f);
        lights.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        lights.SetActive(false);
        yield return new WaitForSeconds(0.09f);
        lights.SetActive(true);
        yield return new WaitForSeconds(0.08f);
        lights.SetActive(false);
        yield return new WaitForSeconds(0.13f);
        lights.SetActive(true);
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
        GameEvents.Instance.onBridgeTriggerEnter -= OnBridgeOpen;               // Unsubscribe from event
    }

    // private void OnDestroy() {
    // }
}
