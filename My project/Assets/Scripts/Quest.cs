using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] public string title;
    [SerializeField] public QuestGoal goal;

    public void Complete() {
        isActive = false;
        Debug.Log (title + " was completed");
    }
}
