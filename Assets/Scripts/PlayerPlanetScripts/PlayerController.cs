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
    public float jumpSpeed = 5f;
    private Vector3 direction = Vector3.zero;
    public Rigidbody rb;
    public RaycastHit hit;
    public GameObject DistanceToGroundObject;
    public CapsuleCollider MainPlayerCollider;
    [Header("Planet")]
    [SerializeField]
    private Transform PlanetTransform;
    public FauxGravityAttractor _FauxGravityAttractor; // Calls the attractor script
    [Header("Movement Variables")]
    public float distToGround;
    public float currentSpeed;
    public bool IsGrounded;
    public enum Direction { Forward, BackWard, Left, Right, ForwardLeft, ForwardRight, BackwardLeft, BackwardRight, still };
    public Direction currentMoveDirection;
    public enum SpeedMovement { Idle, Walking, Running}
    public SpeedMovement CurrentSpeedMovement;
    [Header("Player rotation")]
    public GameObject PlayerModel;
    public float RotationDegree;
    public Quaternion TargetRotation;
    public Vector3 TargetRotVec;
    public Vector3 PlayerVec;
    public float RotationSpeed;
    [Header("Player animation")]
    public AnimController PlayerAnimController;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerAnimController = PlayerModel.GetComponent<AnimController>();
        currentMoveDirection = Direction.still;
        CurrentSpeedMovement = SpeedMovement.Idle;
        rb.useGravity = false; // Disables Gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        PlanetTransform = transform;
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentSpeed = Mathf.Lerp(currentSpeed, TargetSpeed, Time.deltaTime * SpeedTime);
        if (direction.magnitude > 1) {
            direction = direction.normalized; // stops diagonal movement from being faster than straight movement
        }
        /*
        if (Physics.Raycast(DistanceToGroundObject.transform.position, -transform.up, out hit))
        {
            distToGround = hit.distance;
            Debug.DrawLine(DistanceToGroundObject.transform.position, hit.point, Color.cyan);
        }
        */

        if (Input.GetButton("Jump") && IsGrounded) { 
            rb.AddRelativeForce(Vector3.up * jumpSpeed * 1);
            IsGrounded = true;
        }
        if(currentMoveDirection != Direction.still)
        {
            RotationUpdate();
        }
        CheckDirection();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(direction) * currentSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
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
        else {
            TargetSpeed = 0;
            CurrentSpeedMovement = SpeedMovement.Idle;
        }
    }

    void CheckDirection()
    {
        if (direction.x != 0 || direction.z != 0)
        {
            if (direction.x > 0)
            {
                currentMoveDirection = Direction.Right;
                FixedPlayerModelRotation(90);
            }

            if (direction.x < 0)
            {
                currentMoveDirection = Direction.Left;
                FixedPlayerModelRotation(-90);
            }

            if (direction.z > 0)
            {
                currentMoveDirection = Direction.Forward;
                FixedPlayerModelRotation(0);
            }

            if (direction.z < 0)
            {
                currentMoveDirection = Direction.BackWard;
                FixedPlayerModelRotation(180);
            }

            if (direction.x > 0 && direction.z > 0)
            {
                currentMoveDirection = Direction.ForwardRight;
                FixedPlayerModelRotation(25);
            }

            if (direction.x < 0 && direction.z > 0)
            {
                currentMoveDirection = Direction.ForwardLeft;
                FixedPlayerModelRotation(-25);
            }

            if (direction.x > 0 && direction.z < 0)
            {
                currentMoveDirection = Direction.BackwardRight;
                FixedPlayerModelRotation(115);

            }

            if (direction.x < 0 && direction.z < 0)
            {
                currentMoveDirection = Direction.BackwardLeft;
                FixedPlayerModelRotation(-115);

            }
        }
        else if (currentMoveDirection != Direction.still)
        {
            currentMoveDirection = Direction.still;
        }

    }

    void FixedPlayerModelRotation(int RotY)
    {
        TargetRotation.y = RotY;
        TargetRotVec.y = RotY;
        RotationDegree = RotY;
        //PlayerModel.transform.localRotation = Quaternion.Euler(0, RotY, 0);
    }

    void RotationUpdate()
    {
        PlayerModel.transform.localRotation = Quaternion.Slerp(PlayerModel.transform.localRotation, Quaternion.LookRotation(direction.normalized), RotationSpeed);
    }

}

