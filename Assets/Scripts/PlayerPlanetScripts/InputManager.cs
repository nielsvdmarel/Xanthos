using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Input variables")]
    public KeyCode pauseResumeGame = KeyCode.Escape;
    public KeyCode PauseResumeGame { get { return pauseResumeGame; } set {pauseResumeGame = value; }}
    public KeyCode inventory = KeyCode.Tab;
    public KeyCode Inventory { get { return inventory; } set { inventory = value; } }
    public KeyCode crouch = KeyCode.C;
    public KeyCode Crouch { get { return crouch; } set { crouch = value; } }
    public string run = "Fire3";
    public string Run { get { return run; } set { run = value; } }
    public string jump = "Jump";
    public string Jump { get { return jump; } set { jump = value; } }
    public Vector3 direction = Vector3.zero;

    [Header("Keyboard Input")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode ForwardKey { get { return forwardKey; } set { forwardKey = value; } }
    public KeyCode backWardKey = KeyCode.S;
    public KeyCode BackWardKey { get { return backWardKey; } set { backWardKey = value; } }
    public KeyCode leftKey = KeyCode.A;
    public KeyCode LeftKey { get { return leftKey; } set { leftKey = value; } }
    public KeyCode rightKey = KeyCode.D;
    public KeyCode RightKey { get { return rightKey; } set { rightKey = value; } }
    [Header("Controller input")]
    public int xbox_One_Controller = 0;
    public int pS4_Controller = 0;
    public bool ControllerConnected;
    private string[] names;

    private void Start() {
        CheckForController();
        StartCoroutine(ControllerTimer());
    }

    private void Update() {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (direction.magnitude > 1) {
            direction = direction.normalized; // stops diagonal movement from being faster than straight movement
        }
    }

    private void FixedUpdate() {

    }

    void CheckForController() {
        names = Input.GetJoystickNames();
        if (names.Length >= 0)
        {
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 0)
                {
                    pS4_Controller = 0;
                    xbox_One_Controller = 0;
                    ControllerConnected = false;
                }
                if (names[x].Length == 19)
                {
                    //("PS4 CONTROLLER IS CONNECTED");
                    pS4_Controller = 1;
                    xbox_One_Controller = 0;
                    ControllerConnected = true;
                }
                if (names[x].Length == 33)
                {
                    //("XBOX ONE CONTROLLER IS CONNECTED");
                    pS4_Controller = 0;
                    xbox_One_Controller = 1;
                    ControllerConnected = true;
                }
            }
            Debug.Log("Controller connected = " + ControllerConnected);
        }
    }

    private IEnumerator ControllerTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            CheckForController();
        }
    }
}
