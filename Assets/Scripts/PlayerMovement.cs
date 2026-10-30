using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private Vector2 inputVector;
    private Vector2 lookInput; //mouse stuff
    private Vector3 velocity;
    private float xRotation = 0f;

    [Header("Movement & Physics")]
    public float speed = 12f;
    public float gravity =-9.81f;
    [Header("Look Settings")]
 //"Note: To be edited in a future menu"
    public float mouseSensitivity = 100f; //mouse
    public Transform cameraPivot;
    //public float minPitch = -30f; raycast therefore not needed
    //public float maxPitch = 60f;
    public bool mouseLookEnabled = true;
    //[Header("TEMP Movement Rotation")] Not even used!
    //public bool rotateWithMovement = true;
   //public float movementRotationSpeed = 10f;

    [Header("Surface Alignment")]
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float time;
    [SerializeField] private Transform ModelHolder;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //I suppose this locks the mouse
        //removed laserline
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        //removed laserthing
        //no more handlemovementrotation
        SurfaceAlignment();
    }

    void HandleRotation()
    {
        if (!mouseLookEnabled)
        return; //guess this checks if the mouse has been locked

        //takes the mouse input of x and mouse input of y then uses it to rotate the player
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch); [might have been for the raycast laser thing]

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //Makes the camera move with the player on the x axis i think
        } 
    }

    void HandleMovement()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //fall shenanigans here, terminal velocity???
        }

        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
        Vector3 move = right * inputVector.x + forward * inputVector.y;

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = move * speed + Vector3.up * velocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    private void SurfaceAlignment()
    {
        if (ModelHolder == null) return; //Checks if ModelHolder is connected

        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, -transform.up);
        RaycastHit info;

        if (Physics.Raycast(ray, out info, 2f, WhatIsGround))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, info.normal) * transform.rotation;
            ModelHolder.rotation = Quaternion.Lerp(ModelHolder.rotation, targetRotation, animCurve.Evaluate(time) * Time.deltaTime);
        }
        else
        {
            ModelHolder.localRotation = Quaternion.Slerp(ModelHolder.localRotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }
}

