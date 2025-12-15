using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class PlanetRotation : MonoBehaviour
{   
    public float RotationSpeed = 50f;
    private bool Isleft;
    private bool Isright;

    void Update()
    {
        Isleft = Input.GetButton("Left");
        Isright = Input.GetButton("Right");
    }

    void FixedUpdate()
    {
         //movDir = move.ReadValue<Vector2>();

        if (Isright)
        {
            transform.Rotate(Vector2.up*RotationSpeed*Time.deltaTime);
            Debug.Log("Right");
        }
        if (Isleft)
        { //vector2.forward
            transform.Rotate((Vector2.up*RotationSpeed*Time.deltaTime)*-1);
            Debug.Log("Left");
        }
        else{ Debug.Log("Yay"); }
    }
}