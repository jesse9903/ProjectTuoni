// BUG: Kun menee kiinni viholliseen, k‰velee vihollista p‰in ja hypp‰‰ niin ei pysty hypp‰‰m‰‰n sen j‰lkeen

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class ThirdPersonMovementScript : MonoBehaviour
{
    private enum State
    {
        Normal,
        LightDash,
        MediumDash,
        HeavyDash
    }

    private enum WeightClass
    {
        Light,
        Medium,
        Heavy
    }

    public Rigidbody rb;
    public Animator animator;
    public PlayerCombatScript combatScript;
    public Collider playerCollider;
    public float lightMoveSpeed;
    public float mediumMoveSpeed;
    public float heavyMoveSpeed;
    public float lightAttackMoveSpeed;
    public float mediumAttackMoveSpeed;
    public float heavyAttackMoveSpeed;
    public float jumpForce = 2f;
    public float lightDashSpeed;
    public float mediumDashSpeed;
    public float heavyDashSpeed;
    public float mediumDashDropMultiplier;
    public float mediumDashMin;
    public float dashDelay = 1f;
    public float jumpDelay = 1f;
    public float dashDuration = 1f;
    public float squishTime;

    // 1 = light, 2 = medium, 3 = heavy
    public int armorWeightInt;

    // Test
    public bool jumpAllowed;

    private Vector2 moveInput;
    private bool movementDisabled = false;
    private bool canJump = true;
    private bool canMove = true;
    private bool canDash = true;
    private bool isJumping = false;
    private string moveDirection;
    private SpriteRotationScript spriteRotator;
    private State state;
    private WeightClass weightClass;
    private Vector3 moveDirVector;
    private float dashSpeed;

    private void Start()
    {
        spriteRotator = gameObject.GetComponentInChildren<SpriteRotationScript>();
        CheckArmorClass();
        state = State.Normal;
    }

    void Update()
    {
            switch (state)
        {
            case State.Normal:

                // Character movement
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");
                moveInput.Normalize();

                moveDirVector = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

                // Moves if canMove is true
                if (!movementDisabled)
                {
                    if (!combatScript.isAttacking)
                    {
                        switch (weightClass)
                        {
                            case WeightClass.Light:
                                rb.velocity = new Vector3(moveInput.x * lightMoveSpeed, rb.velocity.y, moveInput.y * lightMoveSpeed);
                                break;

                            case WeightClass.Medium:
                                rb.velocity = new Vector3(moveInput.x * mediumMoveSpeed, rb.velocity.y, moveInput.y * mediumMoveSpeed);
                                break;

                            case WeightClass.Heavy:
                                rb.velocity = new Vector3(moveInput.x * heavyMoveSpeed, rb.velocity.y, moveInput.y * heavyMoveSpeed);
                                break;
                        }
                    }
                    else
                    {
                        switch (weightClass)
                        {
                            case WeightClass.Light:
                                rb.velocity = new Vector3(moveInput.x * lightAttackMoveSpeed, rb.velocity.y, moveInput.y * lightAttackMoveSpeed);
                                break;

                            case WeightClass.Medium:
                                rb.velocity = new Vector3(moveInput.x * mediumAttackMoveSpeed, rb.velocity.y, moveInput.y * mediumAttackMoveSpeed);
                                break;

                            case WeightClass.Heavy:
                                rb.velocity = new Vector3(moveInput.x * heavyAttackMoveSpeed, rb.velocity.y, moveInput.y * heavyAttackMoveSpeed);
                                break;
                        }
                    }

                    // Sets the value of animator speed to movement input
                    animator.SetFloat("Speed X", Mathf.Abs(moveInput.x));
                    animator.SetFloat("Speed Z", moveInput.y);

                    // Checks which direction the player is moving when either W, A, S or D key is down
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
                    {
                        moveDirection = CheckMoveDirection();
                        RotatePlayer();
                    }
                }



                // Jump
                if (Input.GetKeyDown(KeyCode.Space) && canJump && jumpAllowed)
                {
                    Jump();
                }

                // Light Dash
                if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping && canDash && !movementDisabled && (weightClass == WeightClass.Light))
                {
                    LightDash();
                    dashSpeed = lightDashSpeed;
                }
                
                // Medium Dash
                if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping && canDash && !movementDisabled && (weightClass == WeightClass.Medium))
                {
                    MediumDash();
                }

                // Medium Dash
                if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping && canDash && !movementDisabled && (weightClass == WeightClass.Heavy))
                {
                    HeavyDash();
                }

                break;

            case State.MediumDash:

                float rollSpeedDropMultiplier = mediumDashDropMultiplier;
                dashSpeed -= dashSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float rollSpeedMinimum = mediumDashMin;
                if (dashSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                    combatScript.SetCanAttackTrue();
                }
                break;
        }
    }

    public void Jump()
    {
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        isJumping= true;
        canJump = false;
    }

    public void LightDash()
    {
        state = State.LightDash;

        rb.AddForce(moveDirVector * dashSpeed, ForceMode.Impulse);

        canDash = false;

        // Disables attacking
        combatScript.SetCanAttackFalse();

        // Delays the dash
        Invoke("DashDelay", dashDelay);

        // Enables attacking after dash duration
        combatScript.Invoke("SetCanAttackTrue", dashDuration);

        state = State.Normal;
    }

    public void MediumDash()
    {
        state = State.MediumDash;
        dashSpeed = mediumDashSpeed;

        // Disables attacking (enabling happens in fixed update)
        combatScript.SetCanAttackFalse();

        canDash = false;

        Invoke("DashDelay", dashDelay);
    }

    public void HeavyDash()
    {
        state = State.HeavyDash;
        dashSpeed = heavyDashSpeed;

        rb.AddForce(moveDirVector * dashSpeed, ForceMode.Impulse);

        canDash = false;

        Invoke("DashDelay", dashDelay);

        Debug.Log("Heavy");

        state = State.Normal;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                break;

            case State.MediumDash:
                rb.velocity = moveDirVector * mediumDashSpeed;
                break;

            case State.HeavyDash:
                rb.velocity = moveDirVector * heavyDashSpeed;
                break;
        }
    }

    // Changes canDash to true (because Invoke-method needs a method)
    public void DashDelay()
    {
        canDash = true;

        // DEBUG
        Debug.Log("Dash ready");
    }

    // Changes canJump to true
    public void JumpDelay()
    {
        canJump = true;
    }

    // Check the move direction and returns it as a string (N = north, E = east etc.)
    public string CheckMoveDirection()
    {
        bool forward = moveInput.y > 0;
        bool backward = moveInput.y < 0;
        bool right = moveInput.x > 0;
        bool left = moveInput.x < 0;

        switch (forward, backward, right, left)
        {
            case (true, false, true, false):
                return "NE";
            
            case (true, false, false, true):
                return "NW";

            case (false, true, true, false):
                return "SE";

            case (false, true, false, true):
                return "SW";

            case (true, false, false, false):
                return "N";

            case (false, false, true, false):
                return "E";

            case (false, true, false, false):
                return "S";

            case (false, false, false, true):
                return "W";

            default:
                return "Couldn't determine movement direction";
        }
    }

    // Rotates players collider and attack point (doesn't rotate sprite if direction is north or south)
    private void RotatePlayer()
    {
        switch (moveDirection)
        {
            case "NE":
                playerCollider.transform.eulerAngles = new Vector3(0, 135, 0);
                spriteRotator.RotateToEast();
                break;

            case "NW":
                playerCollider.transform.eulerAngles = new Vector3(0, 45, 0);
                spriteRotator.RotateToWest();
                break;

            case "SE":
                playerCollider.transform.eulerAngles = new Vector3(0, -135, 0);
                spriteRotator.RotateToEast();
                break;

            case "SW":
                playerCollider.transform.eulerAngles = new Vector3(0, -45, 0);
                spriteRotator.RotateToWest();
                break;

            case "N":
                playerCollider.transform.eulerAngles = new Vector3(0, 90, 0);
                break;

            case "E":
                playerCollider.transform.eulerAngles = new Vector3(0, 180, 0);
                spriteRotator.RotateToEast();
                break;

            case "S":
                playerCollider.transform.eulerAngles = new Vector3(0, -90, 0);
                break;

            case "W":
                playerCollider.transform.eulerAngles = new Vector3(0, 0, 0);
                spriteRotator.RotateToWest();
                break;

            default:
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Changes canJump to "true" when player touches the ground
        if (collision.gameObject.tag.Equals("Ground"))
        {
            canJump = true;
            isJumping= false;
        }

        // Changes canMove-flag to false if player is in air
        if (!(collision.gameObject.tag.Equals("Ground")) && isJumping)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    // Changes flag to "false" when player doesn't touch ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            canJump = false;
        }
    }

    public void Squish()
    {
        // Disables the movement, enables it after squishTime
        SetMovementDisabledTrue();

        // Stops the player
        rb.velocity = Vector3.zero;

        Invoke("SetMovementDisabledFalse", squishTime);

        // Trigger players squished-animation
        GetComponentInChildren<Animator>().SetBool("Squished", true);
    }

    public void SetMovementDisabledTrue()
    {
        movementDisabled = true;
    }

    public void SetMovementDisabledFalse()
    {
        movementDisabled = false;

        gameObject.GetComponentInChildren<Animator>().SetBool("Squished", false);
    }
    
    // Checks the armor weight class
    public void CheckArmorClass()
    {
        switch (armorWeightInt)
        {
            case 1:
                weightClass = WeightClass.Light;
                break;
            case 2:
                weightClass = WeightClass.Medium;
                break;
            case 3:
                weightClass = WeightClass.Heavy;
                break;
        }
    }
}
