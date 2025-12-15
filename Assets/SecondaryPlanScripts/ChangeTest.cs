using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTest : MonoBehaviour
{


    private PlayerInput input;
    private InputAction interact;
    public bool IsSecondPrompt = false;


    void Start()
    {
        input = GetComponent<PlayerInput>();
        interact = input.actions["Interact"];
        interact.started += Interact;
        //GetComponent(SpriteRenderer).sprite = spriteImage;
    }
    
    
    void Update()
    {
        if (IsSecondPrompt)
        {
            Debug.Log("SecondPrompt!!!!");
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        IsSecondPrompt = true;
    }

}