using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private InputField inputFieldA;
    [SerializeField] private InputField inputFieldB;

    [SerializeField] private TextMeshProUGUI Result;
    private IService _service;

    public void ButtonClickHandler()
    {
        int a, b;
        Int32.TryParse(inputFieldA.text, out a);
        Int32.TryParse(inputFieldB.text, out b);
        Result.text = _service.Sum(a, b).ToString();
    }
    void Start()
    {
        _service = new CalculatorService();
    }
}