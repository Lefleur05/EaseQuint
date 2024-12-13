using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] bool canMove = true;
    [SerializeField] float rotateSpeed = 50,moveSpeed=0,minRotationSpeed=0.4f;
    [SerializeField] InputComponent inputRef = null;
    [SerializeField] AccelerationComponent accelerationRef = null;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        rb = GetComponent<Rigidbody>();
        inputRef=GetComponent<InputComponent>();
        accelerationRef=GetComponent<AccelerationComponent>();
    }
    public void SetSpeed(float _speed)
    {
        moveSpeed = _speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
       rb.MovePosition(transform.position+transform.forward * moveSpeed * Time.deltaTime);
        Turn();
    }

    void Turn()
    {
        if (moveSpeed == 0) return;
        float _way = inputRef.TurnAction.ReadValue<float>() ;
        float _rotateSpeed = Time.deltaTime * rotateSpeed* (1-(accelerationRef.CurrentAcceleration/ accelerationRef.MaxAcceleration)+minRotationSpeed) ;

        Vector3 _dir = transform.right * _way;
        if (_dir == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_dir - transform.forward);


        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, _rotateSpeed);
    }
}
