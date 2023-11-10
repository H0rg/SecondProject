using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupContoller : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private void Awake()
    {
        
        Messenger<int,int>.AddListener(StartupEvent.MANAGERS_PROGRESS,OnManagerProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED,OnManagerStarted);
        
    }

    private void OnDestroy()
    {
        Messenger<int,int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS,OnManagerProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED,OnManagerStarted);
    }

    private void OnManagerProgress(int numReady,int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }
    private void OnManagerStarted()
    {
        Manager.Mission.GoToNext();
    }
}
