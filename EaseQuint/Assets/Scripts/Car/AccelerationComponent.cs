using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationComponent : MonoBehaviour
{
    public event Action<float> onAcceleration;
    [SerializeField] InputComponent inputRef = null;
    [SerializeField] float currentAcceleration = 0, maxAcceleration = 10,accelerateFactor=0.01f,deccelerateFactor=0.02f, currentSpeed=0;

    [SerializeField] bool inIntQuint = true;

    // Start is called before the first frame update
    void Start()
    {
        Init();    
    }

    void Init()
    {
        inputRef=GetComponent<InputComponent>();
    }
    // Update is called once per frame
    void Update()
    {
        Accelerate();
    }

    void CheckAcceleration(float _dir)
    {
        currentAcceleration = _dir <= 0 ? currentAcceleration - deccelerateFactor : currentAcceleration + accelerateFactor;
        currentAcceleration = currentAcceleration < 0 ? 0 : currentAcceleration > maxAcceleration ? maxAcceleration : currentAcceleration;
    }

    float CalculateCurrentAcceleration(float _currentAcceleration)
    {
        return inIntQuint?EaseInQuint(_currentAcceleration):EaseOutQuint(_currentAcceleration);
    }
    void Accelerate()
    {
        if (!inputRef) return;
        float _dir = inputRef.AccelerateAction.ReadValue<float>();
        Debug.Log(_dir);

        //Check if player hold forward button or not to affect currenSpeed
        CheckAcceleration(_dir);


        //Check if witch curve use to calculate speed 
        currentSpeed = CalculateCurrentAcceleration(currentAcceleration);
       onAcceleration?.Invoke(currentSpeed);

    }

    float EaseInQuint(float _value)
    {
        Debug.Log("In");
        return _value * _value * _value * _value * _value;
    }

    float EaseOutQuint(float _value)
    {
        Debug.Log("Out");
        return 1 - Mathf.Pow(1 - _value, 5);
    }

    
}
