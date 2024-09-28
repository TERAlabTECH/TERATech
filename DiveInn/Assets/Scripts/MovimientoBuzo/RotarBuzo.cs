using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotarBuzo : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actions;

    public InputActionAsset actions
    {
        get => _actions;
        set => _actions = value;
    }

    protected InputAction leftClickPressedInputAction { get; set; }
    protected InputAction mouseLookInputAction { get; set; }

    protected InputAction positionInputAction {get; set;}

    private bool _rotateAllowed;
    private bool _isTouchingScreen;

    private Camera _camera;

    public Animator swimming;

    
    private float directionAngle;
    private float objectAngle;

    [SerializeField] private float _RotationSpeed=50f;
    [SerializeField] private bool _inverted;
    [SerializeField] private float _moveSpeed = 5f; // Speed of forward movement

    private void Awake()
    {
        InitializeInputSystem();
    }

    private void Start()
    {
        swimming=GetComponent<Animator>();
        
        _camera = Camera.main;
    }

    private void InitializeInputSystem()
    {
        leftClickPressedInputAction = actions.FindAction("Left Click");
        if (leftClickPressedInputAction != null)
        {
            leftClickPressedInputAction.started += OnLeftClickPressed;
            leftClickPressedInputAction.performed += OnLeftClickPressed;
            leftClickPressedInputAction.canceled += OnLeftClickPressed;
        }

        mouseLookInputAction = actions.FindAction("Mouse Look");

        positionInputAction = actions.FindAction("Position");

        
        actions.Enable();
    }

    protected virtual void OnLeftClickPressed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            _rotateAllowed = true;
        }
        else if (context.canceled)
        {
            _rotateAllowed = false;
        }
    }

    protected virtual Vector2 GetMouseLookInput()
    {
        if (mouseLookInputAction != null)
            return mouseLookInputAction.ReadValue<Vector2>();

        return Vector2.zero;
    }
    protected virtual Vector2 GetPointerVector(){
        if(positionInputAction != null){
            Vector2 adjustScreen= new Vector2(Screen.width, Screen.height)/2;
            return positionInputAction.ReadValue<Vector2>()-adjustScreen;
        }
        return Vector2.zero;
    }

    private void Update()
    {
        // Rotate if allowed
        if (_rotateAllowed)
        {
            swimming.Play("DiverSwiming");
            Debug.Log($"New MouseDELta {GetPointerVector()}");

            //subtract 90 cause sprite is rotated
            directionAngle= Mathf.Atan2(GetPointerVector().y, GetPointerVector().x)*180/Mathf.PI-90;
            //euler angles are in degrees
            objectAngle = transform.eulerAngles.z%360;


            float angleDifference = Mathf.DeltaAngle(objectAngle, directionAngle);

            float rotationAmount = Mathf.Clamp(angleDifference, -_RotationSpeed * Time.deltaTime, _RotationSpeed * Time.deltaTime);

            
            


            // Rotate the diver on the Z axis (forward direction)
            transform.Rotate(Vector3.forward, rotationAmount);

             MoveForward();
        }else{
            swimming.Play("DiverIdle");
        }


        
        

        
    }

    private void MoveForward()
    {
        // Move the diver forward based on the direction it is facing
        transform.Translate(transform.up * _moveSpeed/100 * Time.deltaTime, Space.World); 
    }
    private bool RotateCounterClockwise(float objectAngle, float otherAngle){
        if(Mathf.Abs(objectAngle-otherAngle)>180){
            return false;
        }else{
            return true;
        }
    }
}
