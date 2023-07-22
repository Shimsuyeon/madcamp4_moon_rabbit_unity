using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic; // Add this line to include the Dictionary<,> class

[System.Serializable]
public class UserData
{
    public string username;
    public string password;
}

public class RegistrationController : MonoBehaviour
{
    private const string apiUrl = "http://34.64.73.79:3000/api/join"; // Replace with your server URL

    public string username;
    public string password;

    public void RegisterUser()
    {
        StartCoroutine(SendRegistrationRequest());
    }

    IEnumerator SendRegistrationRequest()
    {
        // Create a UserData object containing the user data
        UserData userData = new UserData();
        userData.username = username;
        userData.password = password;

        // Convert the UserData object to a JSON string
        string jsonData = JsonUtility.ToJson(userData);

        // Set up the request headers
        var headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        // Create a UnityWebRequest to send the POST request
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonData))
        {
            foreach (var header in headers)
            {
                www.SetRequestHeader(header.Key, header.Value);
            }

            // Wait for the response
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending registration request: " + www.error);
            }
            else
            {
                // Registration successful
                Debug.Log("Registration successful!");
                Debug.Log("Response: " + www.downloadHandler.text);
            }
        }
    }
}
