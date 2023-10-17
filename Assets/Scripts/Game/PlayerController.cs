using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour, TouchAction.IGameActionActions
{
    TouchAction touchAction;
    InputAction positionAction;
    FruitGenerator fruitGenerator;
    Camera mainCamera;

    bool bCanMove;

    private void Awake() 
    {
        touchAction = new TouchAction();
        touchAction.GameAction.SetCallbacks(this);
        positionAction = touchAction.FindAction("TouchPosition");
    }

    private void Start() 
    {
        fruitGenerator = FindFirstObjectByType<FruitGenerator>();
        mainCamera = Camera.main;
        GameManager.GetManager().OnGameStart += () => enabled = true;
        GameManager.GetManager().OnGameEnd += () => enabled = false;
        GameManager.GetManager().OnFruitDropped += () => bCanMove = true;
        GameManager.GetManager().OnPause += () => enabled = false;
        GameManager.GetManager().OnResume += () => enabled = true;
        GameManager.GetManager().OnGameStart += () => bCanMove = true;
        enabled = false;
        bCanMove = true;
    }

    private void OnEnable() 
    {
        touchAction.Enable();
    }

    private void OnDisable() 
    {
        touchAction.Disable();
    }

    public void MoveFruit(Vector2 destination)
    {
        Fruit fruit = fruitGenerator.GetCurrentFruit();
        Vector3 ctxToWorld = mainCamera.ScreenToWorldPoint(destination);
        Vector3 fruitPosition = fruit.transform.position;
        fruitPosition.x = ctxToWorld.x;
        fruit.Move(fruitPosition);
        
    }

    public void OnTouchInput(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            var touchPositon = positionAction.ReadValue<Vector2>();
            if (bCanMove && touchPositon.y < Screen.height * 0.8f)
            {
                bCanMove = false;
                //Drop Fruit
                MoveFruit(positionAction.ReadValue<Vector2>());
                fruitGenerator.GetCurrentFruit().EnableGravity();
            }
        }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        if(context.performed && bCanMove)
        {
            MoveFruit(context.ReadValue<Vector2>());
        }
    }
}
