using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIConnections
{
    public static User newUser;
    public static IEnumerator FetchData(string id)
    {
        string Url = "webhost.ntsipl.net:3001/api/basicUser/";
        //string id = "Anonymous160";
        using (UnityWebRequest request = UnityWebRequest.Get(Url + id))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
               
                if(!request.downloadHandler.text.Equals("null"))
                    newUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                
            }
        }
    }
}
