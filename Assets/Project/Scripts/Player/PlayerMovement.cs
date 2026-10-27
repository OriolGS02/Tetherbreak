using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerInputReader playerInputs;
    CharacterController characterController;

    Camera cam;
    Vector3 camTransform;
    
    
    
    [Header("MovementValues")]
    public float speed;
    Vector3 movement;

    [Header("RotationValues")]
    public Vector3 MouseWorldPosition { get; private set; }
    public Vector3 rotationDirection;
    [SerializeField]
    private LayerMask groundLayer;


    Animator animator;
    float animationMovementValueX = 0;
    float animationMovementValueY = 0;

    public Cooldown DashCooldown;
    [SerializeField]
    float dashLenght;

    public Transform temporalCursor;

    void Start()
    {
        playerInputs = GetComponent<PlayerInputReader>();
        characterController = GetComponent<CharacterController>();
        cam= Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Animations();

        temporalCursor.position = MouseWorldPosition;

        characterController.Move(movement*speed*Time.deltaTime);
        Dash();
    }   

    void Movement() 
    {
        movement = playerInputs.Move.x * cam.transform.right + playerInputs.Move.y * cam.transform.forward;
        movement.y = 0;
    }

    private void Rotation()
    {

        Debug.Log(playerInputs.Aim);

        Ray ray = cam.ScreenPointToRay(playerInputs.Aim);

        Debug.DrawRay(cam.transform.position, ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer)) 
        {
            MouseWorldPosition = hit.point;
        }


        rotationDirection =MouseWorldPosition- transform.position;
        rotationDirection.y = 0;
        

        


        //transform.rotation=Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationDirection), 5f);
    }

    void Animations() 
    {
        if (movement == Vector3.zero) 
        {
            animationMovementValueX = 0;
            animationMovementValueY = 0;
        }

        Vector3 localMovement=transform.InverseTransformDirection(movement);
        animationMovementValueX = localMovement.z;
        animationMovementValueY = localMovement.x;
        

        animator.SetFloat("Forward", animationMovementValueX);
        animator.SetFloat("Sideways", animationMovementValueY);
    }

    void Dash() 
    {
        if (playerInputs.Dash && !DashCooldown.InCooldown) 
        {
            characterController.Move(movement*dashLenght);
            DashCooldown.StartCooldown();
        }
    }
}
