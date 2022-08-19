using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Login : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TMP_InputField passwordInputField;
    [SerializeField] TMP_Text EmptyFields;
    [SerializeField] TMP_Text NoStudentFound;
    [SerializeField] TMP_Text IncorrectPass;
    [SerializeField] GameObject login;
    [SerializeField] GameObject mainmenu;

    //public static Student currentStudent;
    public static Level currentLevel;
    public static string currentUsername;

    private void Start()
    {
        EmptyFields.gameObject.SetActive(false);
        NoStudentFound.gameObject.SetActive(false);IncorrectPass.gameObject.SetActive(false);
        if(APIConnections.currentUsername!="")
            this.gameObject.SetActive(false);
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
        else
        {
            Student student = new Student
            {
                username = username,
                password = password
            };
            string jsonchecklogin = JsonUtility.ToJson(student);
            yield return StartCoroutine(APIConnections.CheckLogin(jsonchecklogin));
            if (APIConnections.loginResCode == 404)
            {
                NoStudentFound.gameObject.SetActive(true);
            }
            else if(APIConnections.loginResCode == 401)
            {
                NoStudentFound.gameObject.SetActive(false);
                IncorrectPass.gameObject.SetActive(true);
            }
            else
            {
                
                //currentUsername = APIConnections.currentUsername;
                yield return StartCoroutine(APIConnections.FetchLevel(1));
                currentLevel = APIConnections.studentLevel;
                Debug.Log(JsonConvert.SerializeObject(currentLevel));
                login.SetActive(false);
                mainmenu.SetActive(true);
            }
        }
    }
}
