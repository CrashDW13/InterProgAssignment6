using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    //Movement vector. 
    Vector3 moveVec;


    
    [SerializeField] private ControlSystem controls; 

    private float gravity;
    [SerializeField] private float jumpStrength = 7;
    private float vspeed = 0;
    [SerializeField] private float hspeed;
    [SerializeField] private Transform cam; 

    private bool isGrounded;

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity; 

    Rigidbody rb;
    CharacterController controller;

    //==================================================================================
    private void Awake()
    {
        controls = new ControlSystem();
        controls.Player.Jump.performed += Jump;
    }


    private void Start()
    {
        gravity = 9.8f * Time.deltaTime;
        rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
    }
    //==================================================================================



    //==================================================================================
    private void OnEnable()
    {
        controls.Enable(); 
    }
    //==================================================================================



    //==================================================================================
    private void Update()
    {
        isGrounded = CheckIfGrounded();

        if (!isGrounded)
        {
            vspeed -= gravity; 
        }
    }
    
    private void FixedUpdate()
    {
        controller.Move(new Vector3(moveVec.x, vspeed, moveVec.z) * Time.fixedDeltaTime);
    }
    //==================================================================================



    //==================================================================================
    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        Vector3 direction = new Vector3(inputVec.x, 0f, inputVec.y);
        direction = cam.transform.forward * direction.z + cam.transform.right * direction.x;
        direction.y = 0f;
        moveVec = direction * hspeed; 


    }



    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            vspeed = jumpStrength;
        }
    }

    private bool CheckIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            return true; 
        }

        else
        {
            return false;
        }

    }
    //==================================================================================
}
