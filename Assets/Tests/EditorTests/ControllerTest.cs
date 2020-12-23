using System;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.EditorTests
{
    public class ControllerTest
    {
        private Controller _controller;
        private TextMeshProUGUI _result;
        private InputField _inputFieldA;
        private InputField _inputFieldB;
        private IService _service;

        [OneTimeSetUp]
        public void Setup()
        {
            var rootObject = new GameObject();
            _controller = rootObject.AddComponent<Controller>();
            _result = rootObject.AddComponent<TextMeshProUGUI>();
            _inputFieldA = rootObject.AddComponent<InputField>();
            _inputFieldA.name = "InputFieldA";
            _inputFieldB = new GameObject().AddComponent<InputField>();
            _inputFieldB.name = "InputFieldB";
            _service = Substitute.For<IService>();

            // Reflection ¯\_(ツ)_/¯
            var privateFieldAccessFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Default;
            Type type =  _controller.GetType();
            
            type.GetField("inputFieldA", privateFieldAccessFlags)?.SetValue(_controller, _inputFieldA);
            type.GetField("inputFieldB", privateFieldAccessFlags)?.SetValue(_controller, _inputFieldB);
            type.GetField("Result", privateFieldAccessFlags)?.SetValue(_controller, _result);
            type.GetField("_service", privateFieldAccessFlags)?.SetValue(_controller, _service);
        }

        [Test]
        [Description("Clicking handler should set value to 0 if no values were passed")]
        public void ClickHandlerShouldSetResultTo0IfNoValuePassed()
        {
            _controller.ButtonClickHandler();
            Assert.AreEqual("0", _result.text);
        }

        [Test]
        [Description("Clicking handler should set value to 0 if invalid values were passed")]
        public void ClickHandlerShouldSetResultTo0IfInvalidValuesWerePassed()
        {
            _inputFieldA.text = "A";
            _inputFieldB.text = "B";
            _controller.ButtonClickHandler();
            Assert.AreEqual("0", _result.text);
        }

        [Test]
        [Description("Click handler should call service and set result if valid values are passed")]
        public void ClickHandlerShouldCallServiceAndSetResultIfValidValuesArePassed()
        {
            _inputFieldA.text = "1";
            _inputFieldB.text = "1";
            _service.Sum(1, 1).Returns(2);
            _controller.ButtonClickHandler();
            _service.Received(1).Sum(1, 1);
            Assert.AreEqual("2", _result.text);
        }
    }
}