using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{

    [SerializeField] InputActionAsset actions;

    InputAction move;
    InputAction aim;
    InputAction attack;
    InputAction interact;
    InputAction dash;   
    InputAction pause;

    

    public Vector2 Move => move.ReadValue<Vector2>();
    public Vector2 Aim => aim.ReadValue<Vector2>(); 
    public bool Attack => attack.WasPressedThisFrame(); 
    public bool Interact => interact.WasPressedThisFrame();
    public bool Dash => dash.WasPressedThisFrame();   
    public bool Pause => pause.WasPressedThisFrame();

    



    private void Awake()
    {
        InputActionMap map = actions.FindActionMap("Player");

        move = map["Move"];
        aim = map["Aim"];
        attack = map["Attack"];
        interact = map["Interact"];
        dash = map["Dash"];
        pause = map["Pause"];
        

        
        
    }
    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
}
