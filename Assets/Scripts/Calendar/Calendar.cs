using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DialogueEvents;

public class Calendar : MonoBehaviour
{

    // Use this for initialization
    MonthScript currentMonth;
    DialogueDayData currentDay;

    private int dayCounter;
    void Start()
    {
        // TODO: change setting day logic
        currentDay = currentMonth.dayList[0];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetNextDay() { }


    public IEnumerable<DialogueEvent> GetCurrentEvents() 
    {
        return currentDay.GetEligibleEvents();
    }


}
