using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnGUI : MonoBehaviour
{
    [SerializeField] int posX = Screen.width / 2;
    [SerializeField] int posY = 30;
    [SerializeField] int width = 100;
    [SerializeField] int height = 100;
    private static string fpsText = "world Hello world";
    private GUIContent con = new GUIContent(fpsText);

    private float fpsCounter = 0f;
    private float elapsedTime = 0f;
    private float oneSecond = 1f;
    private void OnGUI()
    {
        GUI.Label(new Rect(posX,posY,width,height), new GUIContent(fpsText));
        fpsCounter++;
        if (elapsedTime > oneSecond)
        {
            fpsText = fpsCounter.ToString();
            fpsCounter = 0;
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
    }
   
}
