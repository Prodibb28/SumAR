﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebPHP : MonoBehaviour
{

    public InputField inputFieldstudent;
    public InputField inputFieldActivity;
   void Start()
    {

       
    }
 
    public void Entrar()
    {
        if(!string.IsNullOrEmpty(inputFieldstudent.text ))
         {
          StartCoroutine(GetRequest("http://localhost/UnityTesis/carga.php"));
          StartCoroutine(GetRequest("https://error.html"));
          StartCoroutine(login(inputFieldstudent.text,inputFieldActivity.text));
          
         }  
          if(!string.IsNullOrEmpty(inputFieldActivity.text))
         {
              StartCoroutine(GetActID(inputFieldActivity.text));
         }    
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator login(string studentcode, string activitycode)
    {
        WWWForm form = new WWWForm();
        form.AddField("code_estudiante", studentcode);
        form.AddField("code_actividad", activitycode);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTesis/carga.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                Debug.Log("Error Login");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                Debug.Log("Login Succes");
            }
        }
    }

     IEnumerator GetActID(string activitycode)
    {
        WWWForm form = new WWWForm();
        form.AddField("code_actividad", activitycode);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityTesis/GetActIDs.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
               
            }
        }
    }
}
