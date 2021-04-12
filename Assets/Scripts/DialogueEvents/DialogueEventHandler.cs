using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DialogueEvents;

public class DialogueEventHandler : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    List<DialogueEvent> currentDialogueEvents;



    [SerializeField]
    private Calendar calendar;


    void Start()
    {
        foreach (var ev in calendar.GetCurrentEvents()) 
        {
            // populate UI?
        }
    }

    // Update is called once per frame
    void Update()
    {

    }




}
