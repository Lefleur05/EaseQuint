using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{

    IAA_Car controls = null;
    InputAction accelerateAction= null;

    public InputAction AccelerateAction => accelerateAction;
    private void Awake()
    {
        controls=new IAA_Car();
    }

    private void OnEnable()
    {
        accelerateAction = controls.Car.Accelerate;

        accelerateAction.Enable();
    }
}
