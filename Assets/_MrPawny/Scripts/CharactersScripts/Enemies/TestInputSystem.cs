using UnityEngine;

public class TestInputSystem : MonoBehaviour
{
    [SerializeField] private LandEnemy<TestInputSystem> movementController;
    [HideInInspector] public float jumpBufferCounter;
    private float horizontalMove = 0f;
    private bool crouch = false;
    // public bool PlayerManager.Instance.facingRight;
    private Animator anim;
    private float jumpCooldownTime;
    private float jumpCooldown;

    // TODO: add new attacks accordingly
    private enum Attacks
    {
        SwordForward,
        SwordUp,
        SwordDown
    }


    private void Awake() {
        if (movementController == null){
            movementController = GetComponent<LandEnemy<TestInputSystem>>();
        }
        // anim = GetComponent<Animator>();

        #region Initialization
        PlayerManager.Instance.facingRight = transform.localScale.x >= 0 ? true : false;
        jumpCooldownTime = PlayerManager.Instance.hangTime + 0.05f; // TODO: test this out, mb just chanhge at ManagerInstance
        #endregion

        jumpCooldown = jumpCooldownTime;
    }

    void Update()
    {
        // if (PlayerManager.Instance.paralized == true)
        // {
        //     horizontalMove = 0f;
        //     return;
        // }

        horizontalMove = Input.GetAxisRaw("Horizontal");

        // if(horizontalMove != 0) {
        //     anim.SetBool("isMoveInput", true);
        // } else {
        //     anim.SetBool("isMoveInput", false);
        // };

        // if(horizontalMove > 0 && !PlayerManager.Instance.facingRight){
        //     Flip();
        // }
        // if(horizontalMove < 0 && PlayerManager.Instance.facingRight){
        //     Flip();
        // }

        //handle jump buffer
        // if (Input.GetButtonDown("Jump") && jumpBufferCounter < 0 && jumpCooldown <= 0){
        //     jumpCooldown = jumpCooldownTime;
        //     jumpBufferCounter = jumpBufferTime;
        //     anim.SetTrigger("jumpPressed");
        // } else {
        //     jumpBufferCounter -= Time.deltaTime; 
        // };

        // handle jump
        if (Input.GetButtonDown("Jump")) {
            movementController.Jump();
        }

        // if (Input.GetButtonDown("Crouch")){
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch")){
        //     crouch = false;
        // }

        //TODO: Find Better Way for attacks
        // if (Input.GetMouseButtonDown(0)) 
        // {
        //     if (Input.GetAxisRaw("Vertical") < 0 && !movementController.m_Grounded)
        //     {
        //         movementController.Attack((int)Attacks.SwordDown);
        //         return;
        //     }

        //     if (Input.GetAxisRaw("Vertical") > 0)
        //     {
        //         movementController.Attack((int)Attacks.SwordUp);
        //         return;
        //     }

        //     movementController.Attack((int)Attacks.SwordForward);
        // }

    }

    void FixedUpdate() 
    {
        Vector2 move = new Vector2(horizontalMove, 0f);
        movementController.Move(move, crouch);
        jumpCooldown -= Time.fixedDeltaTime;
    }

}
