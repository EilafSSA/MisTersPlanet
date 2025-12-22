using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetRotator : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private PlayerInputs inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputActions.PlayerInput.Enable();
    }

    private void OnDisable()
    {
        inputActions.PlayerInput.Disable();
    }

    private void Update()
    {
        if (inputActions.PlayerInput.Left.IsPressed())
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (inputActions.PlayerInput.Right.IsPressed())
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }
    }
}