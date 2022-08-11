using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIConnections
{
    public static Level studentLevel;
    //public static Student currentStudent;
    public static long loginResCode;
    public static string currentUsername;
    public static IEnumerator FetchLevel(string username, int lvl)
    {
        string level = lvl.ToString();
        string Url = $"https://bugsquashers1.herokuapp.com/students/{username}/{level}";
        Debug.Log(Url);
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
                    studentLevel = JsonConvert.DeserializeObject<Level>(request.downloadHandler.text);
                    Debug.Log(JsonConvert.SerializeObject(studentLevel));
                    int[] n = new int[studentLevel.time[2].Length + 1];
                    for (int i = 0; i < n.Length - 1; i++)
                    {
                        n[i] = studentLevel.time[2][i];
                    }
                    n[n.Length - 1] = 9;
                    studentLevel.time[2] = n;
                }
                if (request.downloadHandler.text.Equals("null"))
                {
                    Debug.Log("NULL RESPONSE");
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
                //Debug.Log(request.downloadHandler.text);
                Student currentStudent = JsonConvert.DeserializeObject<Student>(request.downloadHandler.text);
                currentUsername = currentStudent.ToString();
                Debug.Log(currentUsername);
                Debug.Log(JsonConvert.SerializeObject(currentStudent));
            }
        }
    }
    /// <summary>
    /// make changes to the level object, add a new level and pass it to UpdateLevel
    /// </summary>
    /// <param name="level"> The level object required to be updated</param>
    /// <param name="updatedScore"> new score</param>
    /// <param name="difficulty">current difficulty</param>
    /// <param name="timetaken">time taken by a player in milliseconds</param>
    public static void makeLevelChanges(Level level,int updatedScore, int difficulty, long timetaken)
    {
        //update the level here
        
    }
    public static IEnumerator UpdateLevel(string bodyJsonString, string username, int lvl)
    {
        //string bodyJsonString = JsonConvert.SerializeObject(bodyObj);
        string level = lvl.ToString();
        string url = $"https://bugsquashers1.herokuapp.com/students/{username}"+"/"+level;
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
                //currentStudent = JsonUtility.FromJson<Student>(request.downloadHandler.text);
            }
        }
    }
}
