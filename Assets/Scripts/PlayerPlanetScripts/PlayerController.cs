using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Basic Needed variables")]
    public float SpeedTime = .5f;
    [SerializeField]
    private float MaxRunningSpeed;
    [SerializeField]
    private float MaxWalkingSpeed;
    [SerializeField]
    private float TargetSpeed;
    public float JumpHeight = 5f;
    [SerializeField]
    private float JumpPower;
    public Rigidbody rb;
    public RaycastHit hit;
    public GameObject DistanceToGroundObject;
    public CapsuleCollider MainPlayerCollider;
    [Header("Movement Variables")]
    public float distToGround;
    public float currentSpeed;
    public bool IsGrounded;
    public bool IsJumping;
    public enum Direction { Forward, BackWard, Left, Right, ForwardLeft, ForwardRight, BackwardLeft, BackwardRight, still };
    public Direction currentMoveDirection;
    public enum SpeedMovement { Idle, Walking, Running}
    public SpeedMovement CurrentSpeedMovement;
    public bool HasMoved = false;
    [Header("Player rotation")]
    public GameObject PlayerModel;
    public float RotationDegree;
    public Quaternion TargetRotation;
    public Vector3 TargetRotVec;
    public Vector3 PlayerVec;
    public float RotationSpeed;
    [Header("Controllers")]
    public AnimController PlayerAnimController;
    public InputManager inputManager;
    [SerializeField] private FauxGravityBody CurrentGravityBody;
    [SerializeField] private FauxGravityAttractor CurrentGravityAttractor;
    [Header("Planet")]
    [SerializeField] private Transform PlanetTransform;
    [Header("Isreally movement section")]
    //
    [SerializeField]
    private bool ismoving;
    public bool isStoppedByCollider;
    void Start()
    {
        CurrentGravityBody = this.GetComponent<FauxGravityBody>();
        CurrentGravityAttractor = CurrentGravityBody.attractor;
        rb = this.GetComponent<Rigidbody>();
        PlayerAnimController = PlayerModel.GetComponent<AnimController>();
        currentMoveDirection = Direction.still;
        CurrentSpeedMovement = SpeedMovement.Idle;
        rb.useGravity = false; // Disables Gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        // controller sided;
    }

    void Update()
    {
       MovementUpdate();
       CheckTrueMovement();
    }

    void FixedUpdate()
    {
        MovementFixedUpdate();
    }

    void CheckDirection() {
        if (inputManager.direction.x != 0 || inputManager.direction.z != 0) {
            if (inputManager.direction.x > 0) {
                currentMoveDirection = Direction.Right;
                FixedPlayerModelRotation(90);
            }

            if (inputManager.direction.x < 0) {
                currentMoveDirection = Direction.Left;
                FixedPlayerModelRotation(-90);
            }

            if (inputManager.direction.z > 0) {
                currentMoveDirection = Direction.Forward;
                FixedPlayerModelRotation(0);
            }

            if (inputManager.direction.z < 0) {
                currentMoveDirection = Direction.BackWard;
                FixedPlayerModelRotation(180);
            }

            if (inputManager.direction.x > 0 && inputManager.direction.z > 0) {
                currentMoveDirection = Direction.ForwardRight;
                FixedPlayerModelRotation(25);
            }

            if (inputManager.direction.x < 0 && inputManager.direction.z > 0) {
                currentMoveDirection = Direction.ForwardLeft;
                FixedPlayerModelRotation(-25);
            }

            if (inputManager.direction.x > 0 && inputManager.direction.z < 0) {
                currentMoveDirection = Direction.BackwardRight;
                FixedPlayerModelRotation(115);
            }

            if (inputManager.direction.x < 0 && inputManager.direction.z < 0) {
                currentMoveDirection = Direction.BackwardLeft;
                FixedPlayerModelRotation(-115);
            }
        }
        else if (currentMoveDirection != Direction.still) {
            currentMoveDirection = Direction.still;
        }
    }

    void FixedPlayerModelRotation(int RotY) {
        TargetRotation.y = RotY;
        TargetRotVec.y = RotY;
        RotationDegree = RotY;
    }

    void RotationUpdate() {
        PlayerModel.transform.localRotation = Quaternion.Slerp(PlayerModel.transform.localRotation, Quaternion.LookRotation(inputManager.direction.normalized), RotationSpeed);
    }

    public void SetGravityLower(int gravity) {
        CurrentGravityAttractor.gravity = gravity;
    }

    public IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(.2f);
        Jump();
    }

    private void Jump() {
        rb.AddForce(PlayerModel.transform.up * JumpHeight);
        rb.AddForce(PlayerModel.transform.forward * JumpPower);
    }

    public void MovementUpdate() {
        currentSpeed = Mathf.Lerp(currentSpeed, TargetSpeed, Time.deltaTime * SpeedTime);

        if (Physics.Raycast(DistanceToGroundObject.transform.position, -transform.up, out hit)) {
            distToGround = hit.distance;
            Debug.DrawLine(DistanceToGroundObject.transform.position, hit.point, Color.cyan);
        }
        if (IsGrounded) {
            if (Input.GetButtonDown(inputManager.jump)) {
                if (Input.GetButton(inputManager.run)) {
                    if (!IsJumping) {
                        IsJumping = true;
                        //rb.AddForce(direction.normalized * JumpPower);
                        //rb.AddForce(Vector3.up * JumpHeight);// lol basic dash
                        /////rb.AddForce(PlayerModel.transform.up * JumpHeight);
                        /////rb.AddForce(PlayerModel.transform.forward * JumpPower);
                        StartCoroutine(JumpTimer());
                        //rb.AddForce(direction * JumpPower); // lol basic dash
                    }
                }
                if (!Input.GetButton(inputManager.run))
                {
                    if (!IsJumping)
                    {
                        IsJumping = true;
                        //rb.AddForce(direction.normalized * JumpPower);
                        //rb.AddForce(Vector3.up * JumpHeight);// lol basic dash
                        /////rb.AddForce(PlayerModel.transform.up * JumpHeight);
                        /////rb.AddForce(PlayerModel.transform.forward * JumpPower);
                        StartCoroutine(JumpTimer());
                        //rb.AddForce(direction * JumpPower); // lol basic dash
                    }
                }
            }
        }

        if (currentMoveDirection != Direction.still && IsGrounded) {
            RotationUpdate();
        }
        CheckDirection();
    }

    public void MovementFixedUpdate()
    {
        if (!inputManager.ControllerConnected)
        {
            if (!IsJumping)
            {
                rb.MovePosition(rb.position + transform.TransformDirection(inputManager.direction) * currentSpeed * Time.deltaTime);
            }
            if (inputManager.direction != Vector3.zero)
            {
                if (Input.GetButton(inputManager.run))
                {
                    TargetSpeed = MaxRunningSpeed;
                    CurrentSpeedMovement = SpeedMovement.Running;
                }

                else
                {
                    TargetSpeed = MaxWalkingSpeed;
                    CurrentSpeedMovement = SpeedMovement.Walking;
                }
            }

            else
            {
                TargetSpeed = 0;
                CurrentSpeedMovement = SpeedMovement.Idle;
            }
        }

        if (inputManager.ControllerConnected)
        {
            if (!IsJumping)
            {
                rb.MovePosition(rb.position + transform.TransformDirection(inputManager.direction) * currentSpeed * Time.deltaTime);
            }

            if(inputManager.direction != Vector3.zero)
            {
                if (Input.GetButton(inputManager.run))
                {
                    TargetSpeed = MaxRunningSpeed;
                    CurrentSpeedMovement = SpeedMovement.Running;
                }

                else
                {
                    TargetSpeed = MaxWalkingSpeed;
                    CurrentSpeedMovement = SpeedMovement.Walking;
                }
            }
            else
            {
                TargetSpeed = 0;
                CurrentSpeedMovement = SpeedMovement.Idle;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Planet")
        {
            IsGrounded = true;
            IsJumping = false;
            SetGravityLower(-10); // weg halen als er andere gravtiy's worden veranderd, zoals een + gravity bij damage etc. overide nu alles
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isStoppedByCollider = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Planet")
        {
            IsGrounded = false;
            SetGravityLower(-4); // weg halen als er andere gravtiy's worden veranderd, zoals een + gravity bij damage etc. overide nu alles
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            isStoppedByCollider = false;
        }
    }

    void CheckTrueMovement()
    {
        // replace with degrees from planet to player check
        float forwardTest;
        forwardTest = Vector3.Dot(-inputManager.direction.normalized, transform.position.normalized);
        //Debug.Log(forwardTest);
        if (forwardTest != 0)
        {
            ismoving = true;
        }

        else
        {
            ismoving = false;
        }
    }
}

