using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SightSenseConditionSO", menuName = "State Machine/Conditions/Was Player Seen")]
public class SightSenseConditionSO : StateConditionSO
{
    [SerializeField] private LayerMask _whatCanBeSeen;

    protected override Condition CreateCondition() => new SightSenseCondition(_whatCanBeSeen);
}

public class SightSenseCondition : Condition
{
    private int fieldOfView;
    private float viewDistance;
    private float detectionRate;
    private float elapsedTime = 0f;
    private LayerMask whatCanBeSeen;
    private Transform _transform;
    private Transform sightSourcePoint;
    private Transform[] playerParts = new Transform[3];
    private Vector2 rayDirection;
    private StateMachine stateMachine;

    public SightSenseCondition(LayerMask whatCanBeSeen)
    {
        this.whatCanBeSeen = whatCanBeSeen;
    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        fieldOfView     = stateMachine._manager.FieldOfView;
        detectionRate   = stateMachine._manager.DetectionRate;
        viewDistance    = stateMachine._manager.ViewDistance;
        sightSourcePoint= stateMachine.transform.Find("SightPoint");
        _transform      = stateMachine.transform;

        playerParts[0]  = GameObject.FindGameObjectWithTag("Player").transform;
        playerParts[1]  = playerParts[0].Find("GroundCheck");
        playerParts[2]  = playerParts[0].Find("CeilingCheck");
    }

    protected override bool Statement() => DetectAspect();

    private bool DetectAspect()
    {
        elapsedTime += Time.deltaTime;
#if UNITY_EDITOR
        fieldOfView = stateMachine._manager.FieldOfView;
        viewDistance = stateMachine._manager.ViewDistance;
#endif
        if (elapsedTime < detectionRate)
        {
            return false;
        } else 
        {
            elapsedTime = 0;

            foreach (Transform playerTransform in playerParts)
            {
                rayDirection = playerTransform.position - sightSourcePoint.position;

                if(Vector2.Angle(rayDirection, sightSourcePoint.right * _transform.root.localScale.x) < fieldOfView * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(sightSourcePoint.position, rayDirection, viewDistance, whatCanBeSeen);
                    if(hit.collider != null)
                    {
                        Aspect aspect = hit.collider.GetComponent<Aspect>();

                        if(aspect != null && aspect.aspectType != stateMachine._aspectName)
                        {
                            stateMachine._targetLastPosition = hit.collider.transform.position;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
