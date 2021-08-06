using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SightSenseConditionSO", menuName = "State Machine/Conditions/Saw The Target")]
public class SightSenseConditionSO : StateConditionSO
{
    [SerializeField] private LayerMask whatCanBeSeen;

    protected override Condition CreateCondition() => new SightSenseCondition(whatCanBeSeen);
}

public class SightSenseCondition : Condition
{
    private int fieldOfView;
    private float viewDistance;
    private float detectionRate;
    private LayerMask whatCanBeSeen;
    private Transform transform;
    private Transform sightSourcePoint;
    private Transform[] playerParts = new Transform[3];
    private StateMachine stateMachine;
    private Vector2 _rayDirection;
    private float _elapsedTime = 0f;
    
    public SightSenseCondition(LayerMask whatCanBeSeen)
    {
        this.whatCanBeSeen = whatCanBeSeen;
    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        fieldOfView     = stateMachine.statsSO.FieldOfView;
        detectionRate   = stateMachine.statsSO.DetectionRate;
        viewDistance    = stateMachine.statsSO.ViewDistance;
        sightSourcePoint= stateMachine.transform.Find("SightPoint");
        transform      = stateMachine.transform;

        playerParts[0]  = GameObject.FindGameObjectWithTag("Player").transform;
        playerParts[1]  = playerParts[0].Find("GroundCheck");
        playerParts[2]  = playerParts[0].Find("CeilingCheck");
    }

    protected override bool Statement() => DetectAspect();

    private bool DetectAspect()
    {
        _elapsedTime += Time.deltaTime;
#if UNITY_EDITOR
        fieldOfView = stateMachine.statsSO.FieldOfView;
        viewDistance = stateMachine.statsSO.ViewDistance;
#endif
        if (_elapsedTime < detectionRate)
        {
            return false;
        } else 
        {
            _elapsedTime = 0;

            foreach (Transform playerTransform in playerParts)
            {
                _rayDirection = playerTransform.position - sightSourcePoint.position;

                if(Vector2.Angle(_rayDirection, sightSourcePoint.right * transform.root.localScale.x) < fieldOfView * 0.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(sightSourcePoint.position, _rayDirection, viewDistance, whatCanBeSeen);
                    if(hit.collider != null)
                    {
                        Aspect aspect = hit.collider.GetComponent<Aspect>();

                        if(aspect != null && aspect.aspectType != stateMachine.aspectName)
                        {
                            stateMachine.targetLastPosition = hit.collider.transform.position;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
