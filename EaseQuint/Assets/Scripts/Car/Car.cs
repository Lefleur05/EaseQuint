using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] MovementComponent movement;
    [SerializeField] AccelerationComponent acceleration;
    [SerializeField] InputComponent inputs  ;
    // Start is called before the first frame update
    void Start()
    {
        Init();        
    }
    void Init()
    {
        movement = GetComponent<MovementComponent>();
        acceleration = GetComponent<AccelerationComponent>();
        inputs = GetComponent<InputComponent>();

        acceleration.onAcceleration += movement.SetSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
