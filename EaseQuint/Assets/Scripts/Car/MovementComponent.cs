using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] bool canMove = true;
    [SerializeField] float rotateSpeed = 50,moveSpeed=0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        rb = GetComponent<Rigidbody>();
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
    }
}
