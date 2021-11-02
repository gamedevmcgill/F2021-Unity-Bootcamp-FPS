using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Player input controller
 * Note: We are using the new InputSystem package which first needs to be installed.
 * Relevant documentation: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html
 * https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
 */
public class PlayerController : MonoBehaviour
{
    public Weapon weapon;

    public GameObject cameraSubobject;

    private InputActions inputMap;
    private CharacterController charController;

    // Camera values
    public float lookSensitivity = 50f;
    private float pitch = 0f;
    private float yaw = 0f;

    // Movement values
    public float movementSpeed = 10f;

    // gravity
    public float gravity = -9.81f;
    public float playerHeight = 0.2f;
    private bool isGrounded = false;
    public LayerMask groundLayer;

    private Vector3 velocity;

    // jump values
    public float jumpHeight = 10f;

    //ground detection bool
    public GameObject groundCheck;

    private void OnJump()
    {
        Debug.Log("Jumping!");
    }

    private void OnShoot()
    {
        weapon.Shoot();
        Debug.Log("Shoot");
    }
    
    private void OnEnable()
    {
        if (inputMap == null)
        {
            inputMap = new InputActions();
        }
        inputMap.Enable();

        // Register the input action callbacks
        // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html#creating-actions
        inputMap.Player.Shoot.performed += eventCtx => OnShoot();
        inputMap.Player.Jump.performed += eventCtx => OnJump();
    }

    private void OnDisable()
    {
        //inputMap.Disable();

        // Deregister the input action callbacks
        //inputMap.Player.Shoot.performed -= eventCtx => OnShoot();
        //inputMap.Player.Jump.performed -= eventCtx => OnJump();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        charController = GetComponent<CharacterController>();
        if (charController == null)
        {
            Debug.LogError("Missing character controller on player!");
        }
    }

    private void Update()
    {
        // Rotate the camera with the look axis values
        Vector2 lookDelta = inputMap.Player.Look.ReadValue<Vector2>();
        // if there is any new look
        if (lookDelta.magnitude != 0)
        {
            lookDelta *= lookSensitivity * Time.deltaTime;

            pitch -= lookDelta.y;
            yaw += lookDelta.x;

            pitch = Mathf.Clamp(pitch, -89f, 89f);

            cameraSubobject.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
            transform.Rotate(Vector3.up * lookDelta.x);
        }

        // Transform the player with the movement delta
        Vector2 moveDelta = inputMap.Player.Movement.ReadValue<Vector2>();
        // if there is any new movement
        Vector3 translationDelta = new Vector3();
        if (moveDelta.magnitude != 0)
        {
            translationDelta = (transform.right * moveDelta.x) + (transform.forward * moveDelta.y);
        }

        // Increment our downwards velocity
        velocity.y += 2.5f * gravity * Time.deltaTime;

        // If there is an object within some sphere around us and it is part of the ground layer mask (a way to group objects)
        // reset our downward velocity to 0
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        bool isJumping = inputMap.Player.Jump.triggered;
        if (isGrounded)
        {
            Debug.Log("Grounded");
        }
        if (isJumping && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        charController.Move(translationDelta * movementSpeed * Time.deltaTime + velocity * Time.deltaTime);
    }
}
