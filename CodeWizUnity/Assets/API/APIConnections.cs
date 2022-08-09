using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIConnections
{
    public static Level studentLevel;
    public static Student currentStudent;
    public static long loginResCode;
    public static IEnumerator FetchLevel()
    {
        string Url = "https://bugsquashers1.herokuapp.com/students/testStudent1/1";
        using (UnityWebRequest request = UnityWebRequest.Get(Url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (!request.downloadHandler.text.Equals("null"))
                {
                    /*Debug.Log(request.downloadHandler.text);*/
                    studentLevel = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                    Debug.Log(studentLevel);
                }
                    
            }
        }
    }
    public static IEnumerator CheckLogin(string bodyJsonString)
    {
        string url = "https://bugsquashers1.herokuapp.com/students/login";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        /*        Debug.Log("Status Code: " + request.responseCode);*/
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (!request.downloadHandler.text.Equals("null"))
            {
                loginResCode = request.responseCode;
                Debug.Log(request.downloadHandler.text);
                currentStudent = JsonUtility.FromJson<Student>(request.downloadHandler.text);
            }
        }
    }

    public static IEnumerator UpdateLevel(string bodyJsonString, string username, string lvl)
    {
        string url = $"https://bugsquashers1.herokuapp.com/students/{username}/{lvl}";
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        /*        Debug.Log("Status Code: " + request.responseCode);*/
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (!request.downloadHandler.text.Equals("null"))
            {
                loginResCode = request.responseCode;
                Debug.Log(request.downloadHandler.text);
                currentStudent = JsonUtility.FromJson<Student>(request.downloadHandler.text);
            }
        }
    }
}
