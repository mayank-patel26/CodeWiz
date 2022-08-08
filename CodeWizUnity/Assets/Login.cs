using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_Text EmptyFields;
    [SerializeField] TMP_Text NoStudentFound;
    public static Student currentStudent;

    private void Start()
    {
        EmptyFields.gameObject.SetActive(false);
        NoStudentFound.gameObject.SetActive(false);
    }

    public void onButtonClick()
    {
        StartCoroutine(onLoginClick());
    }

    IEnumerator onLoginClick()
    {
        string username = usernameInputField.text.ToString().Trim();
        string password = passwordInputField.text.ToString().Trim();
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            EmptyFields.gameObject.SetActive(true);
        }
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            Student student = new Student
            {
                username = username,
                password = password
            };
            string jsonchecklogin = JsonUtility.ToJson(student);
            yield return StartCoroutine(APIConnections.CheckLogin(jsonchecklogin));
            currentStudent = APIConnections.currentStudent;
            /*Debug.Log(currentStudent.ToString());*/
            /*SceneManager.LoadScene("Maze");*/
        }
    }
}
