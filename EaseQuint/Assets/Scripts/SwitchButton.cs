using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] bool isActivate = false;
    [SerializeField] bool isOnTargetPos = false;
    [SerializeField] Vector3 ActivatePos = Vector3.zero;
    [SerializeField] Vector3 notActivatePos = Vector3.zero;
    [SerializeField] float timer = 0.0f, timeToSwitch = 3.0f;

    [SerializeField] Image colorButton = null;
    [SerializeField] Color activateColor = Color.blue, notActivateColor = Color.magenta;


    [SerializeField] TextMeshProUGUI textMeshPro = null;
    [SerializeField] string activateText = "Activate", notActivateText = "Not Activate";

    public bool IsActivate => isActivate;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isOnTargetPos)
        {
            CheckIsOnTargetPos();
            Switch();
        }
    }

    void Init()
    {
        button = GetComponent<Button>();
        colorButton = GetComponent<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(Execute);
    }

    void Switch()
    {
        if (timer < timeToSwitch)
            timer += Time.deltaTime;

        float _t = Mathf.Clamp01(timer / timeToSwitch);
        float _easedT = EaseInOutQuint(_t);

        SwitchPos(_easedT);
        SwitchColor(_easedT);
        SwitchColorText(_easedT);
    }

    void SwitchPos(float _easedT)
    {
        if (!button) return;

        Vector3 _newPos = button.transform.position;
        if (isActivate)
            _newPos = Vector3.Lerp(notActivatePos, ActivatePos, _easedT);
        else if (!isActivate)
            _newPos = Vector3.Lerp(ActivatePos, notActivatePos, _easedT);
        //Debug.Log("Switch Pos");
        transform.localPosition = _newPos;
    }

    void SwitchColor(float _easedT)
    {
        if (!colorButton) return;

        Color _newColor = colorButton.color;
        if (isActivate)
            _newColor = Color.Lerp(notActivateColor, activateColor, _easedT);
        else if (!isActivate)
            _newColor = Color.Lerp(activateColor, notActivateColor, _easedT);
        //Debug.Log("Swtch Color");
        colorButton.color = _newColor;
    }

    void SwitchText()
    {
        textMeshPro.text = isActivate ? activateText : notActivateText;
    }

    void SwitchColorText(float _easedT)
    {
        if (!textMeshPro || !colorButton) return;

        Color _color = colorButton.color;
        Color.RGBToHSV(_color, out float _h, out float _s, out float _v);
        float _negativeH = (_h + 0.5f) % 1f;
        //Debug.Log("Color" + _negativeH + " / " + _h);
        Color _negativeColor = Color.HSVToRGB(_negativeH, _s, _v);


        Color _newColor = colorButton.color;
        if (isActivate)
            _newColor = Color.Lerp(textMeshPro.color, _negativeColor, _easedT);
        else if (!isActivate)
            _newColor = Color.Lerp(textMeshPro.color, _negativeColor, _easedT);
        //Debug.Log("Switch Text");
        textMeshPro.color = _newColor;
    }

    private float EaseInOutQuint(float _t)
    {
        return _t < 0.5 ? 16 * _t * _t * _t * _t * _t : 1 - Mathf.Pow(-2 * _t + 2, 5) / 2;
    }

    void Execute()
    {
        if (!isOnTargetPos) return;
        isActivate = !isActivate;
        isOnTargetPos = false;
        timer = 0;
        SwitchText();
    }

    void CheckIsOnTargetPos()
    {
        Vector3 _targetPos = isActivate ? ActivatePos : notActivatePos;
        isOnTargetPos = Vector3.Distance(_targetPos, transform.localPosition) <= 0.1f;
        //Debug.Log(Vector3.Distance(_targetPos, transform.localPosition));
    }

}
