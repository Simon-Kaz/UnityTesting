using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.PlayModeTests
{
    public class PlayModeTests
    {
        [UnityTest]
        [Description("Verify 1 + 2 = 3")]
        [Timeout(10000)] // 10 seconds timeout
        public IEnumerator Verify1Plus2Equals3()
        {
            // Load the scene
            SceneManager.LoadScene("Scenes/SampleScene", LoadSceneMode.Single);

            // Wait for scene to load by checking if the first input field is displayed
            GameObject firstInputField = null;
            yield return new WaitUntil(() => (firstInputField = GameObject.Find("InputFieldA")) != null);

            // Locate all the other elements we need
            var secondInputField = GameObject.Find("InputFieldB");
            var calculateButton = GameObject.Find("A+B");
            var resultGO = GameObject.Find("Result");

            // Set the value in the input fields
            firstInputField.GetComponent<InputField>().text = "1";
            secondInputField.GetComponent<InputField>().text = "2";

            // Trigger the calculation by tapping on the A+B button
            calculateButton.GetComponent<Button>().onClick.Invoke();

            // Verify that the result is displayed correctly
            Assert.AreEqual("3", resultGO.GetComponent<TextMeshProUGUI>().text);
        }
    }
}