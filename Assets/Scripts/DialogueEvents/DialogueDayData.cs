using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DialogueEvents;

[CreateAssetMenu]
public class DialogueDayData : ScriptableObject
{

    // TODO: Add other parameters needed for day here
    [SerializeField]
    List<DialogueEvent> events;

    [SerializeField]
    private string dayName;

    public string DayName { get; private set; }



    public IEnumerable<DialogueEvent> GetEligibleEvents() 
    {
        foreach (DialogueEvent ev in events) 
        {
            if (ev.CheckTriggers()) 
            {
                yield return ev;
            }
        }
    }
}
