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
    [SerializeField] GameObject coding;
    [SerializeField] GameObject main;

    public static Student currentStudent;

    private void Start()
    {
        EmptyFields.gameObject.SetActive(false);
        NoStudentFound.gameObject.SetActive(false);IncorrectPass.gameObject.SetActive(false);
    }

    public void onButtonClick()
    {
        /*StartCoroutine(onLoginClick());*/
        StartCoroutine(updateStudentLevel());
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
            if(APIConnections.loginResCode == 404)
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
                currentStudent = APIConnections.currentStudent;
                login.SetActive(false);
                coding.SetActive(false);
                main.SetActive(true);
            }
            /*Debug.Log(currentStudent.ToString());*/
            /*SceneManager.LoadScene("Maze");*/
        }
    }

    IEnumerator updateStudentLevel()
    {
        string username = "alphacentauri1";
        int l = 3;
        string lvl = l.ToString();
        int[][] time = new int[][]
        {
            new int[]{8,8,8},
            new int[]{8,8,8},
            new int[]{8,8,8}
        };
        int[][] incat = new int[][]
        {
            new int[]{8,8,8},
            new int[]{8,8,8},
            new int[]{8,8,8}
        };
        
        int[] score = new int[3] { 1000, 2000, 3000 };

        Level updatedLevel = new Level();
        updatedLevel.time = time;
        updatedLevel.score = score;
        updatedLevel.incat = incat;
        updatedLevel.badges = "gold";
        updatedLevel.helpReq = true;
        updatedLevel.mentorUser = null;
        string jsonupdatedlevel = JsonConvert.SerializeObject(updatedLevel);
        Debug.Log(jsonupdatedlevel);
        yield return StartCoroutine(APIConnections.UpdateLevel(jsonupdatedlevel, username, lvl));
        currentStudent = APIConnections.currentStudent;
    }
}
