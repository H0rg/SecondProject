using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] private float Duration = 3f;
    private Vector3 startPos;
    private Vector3 openPos;
    private Vector3 closePos;
    private Vector3 currentPos;
    public bool requireKey;

    private void Awake()
    {
         startPos = transform.Find("MainDoor").position;
         openPos = startPos + new Vector3(0,2.5f,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (requireKey && Manager.Inventory.equippedItem != "key")
            {
                return;
            }
            if (!isOpen)
            {
                StopAllCoroutines();
                isOpen = true;
                currentPos = transform.Find("MainDoor").position;
                StartCoroutine(Open());
            }
            else
            {
                StopAllCoroutines();
                isOpen = false;
                currentPos = transform.Find("MainDoor").position;
                StartCoroutine(Close());
            }
        }
    }

    public IEnumerator Open()
    {
        float timeElapsed = 0;
        Debug.Log("Openning");
        while (timeElapsed <= Duration)
        {
            transform.Find("MainDoor").position = Vector3.Lerp(currentPos, openPos, timeElapsed / Duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator Close()
    {
        float timeElapsed = 0;
        Debug.Log("Clossing");
        while (timeElapsed < Duration)
        {
            transform.Find("MainDoor").position = (Vector3.Lerp(currentPos, startPos, timeElapsed / Duration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
