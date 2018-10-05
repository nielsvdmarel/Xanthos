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
    private Vector3 direction = Vector3.zero;
    public Rigidbody rb;
    public RaycastHit hit;
    public GameObject DistanceToGroundObject;
    public CapsuleCollider MainPlayerCollider;
    [Header("Planet")]
    [SerializeField]
    private Transform PlanetTransform;
    [Header("Planet gravity settings")]
    [SerializeField]
    private FauxGravityBody CurrentGravityBody;
    [SerializeField]
    private FauxGravityAttractor CurrentGravityAttractor;
    [Header("Movement Variables")]
    public float distToGround;
    public float currentSpeed;
    public bool IsGrounded;
    public bool IsJumping;
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
    [Header("Controller input")]
    private string[] names;
    public int Xbox_One_Controller = 0;
    public int PS4_Controller = 0;
    public bool ControllerConnected = false;

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
        CheckForController();
    }

    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentSpeed = Mathf.Lerp(currentSpeed, TargetSpeed, Time.deltaTime * SpeedTime);
        if (direction.magnitude > 1) {
            direction = direction.normalized; // stops diagonal movement from being faster than straight movement
        }
        
        if (Physics.Raycast(DistanceToGroundObject.transform.position, -transform.up, out hit))
        {
            distToGround = hit.distance;
            Debug.DrawLine(DistanceToGroundObject.transform.position, hit.point, Color.cyan);
        }
        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift)) { }
                if(!IsJumping)
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

        if (currentMoveDirection != Direction.still && IsGrounded)
        {
            RotationUpdate();
        }
        CheckDirection();
    }

    void FixedUpdate()
    {
        Movement();
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
    }

    void RotationUpdate()
    {
        PlayerModel.transform.localRotation = Quaternion.Slerp(PlayerModel.transform.localRotation, Quaternion.LookRotation(direction.normalized), RotationSpeed);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Planet") {
            IsGrounded = true;
            IsJumping = false;
            SetGravityLower(-10); // weg halen als er andere gravtiy's worden veranderd, zoals een + gravity bij damage etc. overide nu alles
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Planet") {
            IsGrounded = false;
            SetGravityLower(-4); // weg halen als er andere gravtiy's worden veranderd, zoals een + gravity bij damage etc. overide nu alles
        }
    }

    public void SetGravityLower(int gravity) {
        CurrentGravityAttractor.gravity = gravity;
    }

    public IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(.2f);
        rb.AddForce(PlayerModel.transform.up * JumpHeight);
        rb.AddForce(PlayerModel.transform.forward * JumpPower);
    }

    void CheckForController()
    {
        names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
            }
            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = 0;
                Xbox_One_Controller = 1;

            }

            if(names[x].Length == 0)
            {
                PS4_Controller = 0;
                Xbox_One_Controller = 0;
            }
        }


        if (Xbox_One_Controller == 1)
        {
            ControllerConnected = true;
        }
        else if (PS4_Controller == 1)
        {
            ControllerConnected = true;
        }
        else
        {
            ControllerConnected = false;
        }
    }

    public void Movement()
    {
        if (!ControllerConnected)
        {
            if (!IsJumping)
            {
                rb.MovePosition(rb.position + transform.TransformDirection(direction) * currentSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetButton("Fire3"))
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

        if (ControllerConnected)
        {
            if (!IsJumping)
            {
                rb.MovePosition(rb.position + transform.TransformDirection(direction) * currentSpeed * Time.deltaTime);
            }

            if(direction != new Vector3(0, 0, 0))
            {
                if (Input.GetButton("Fire3"))
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
}

