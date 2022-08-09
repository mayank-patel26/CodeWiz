using Newtonsoft.Json;
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
        string Url = "https://bugsquashers1.herokuapp.com/students/alphacentauri2/1";
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
                    //studentLevel = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                    studentLevel = JsonConvert.DeserializeObject<Level>(request.downloadHandler.text);
                    Debug.Log(JsonConvert.SerializeObject(studentLevel));
                    /*int[] n = new int[studentLevel.time[2].Length + 1];
                    for (int i = 0; i < n.Length - 1; i++)
                    {
                        n[i] = studentLevel.time[2][i];
                    }
                    n[n.Length - 1] = 9;
                    studentLevel.time[2] = n;*/
                    
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
                Debug.Log(JsonConvert.SerializeObject(currentStudent));
            }
        }
    }

    public static IEnumerator UpdateLevel(Level bodyObj, string username, string lvl)
    {
        string bodyJsonString = JsonConvert.SerializeObject(bodyObj);
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
