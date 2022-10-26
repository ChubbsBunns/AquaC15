using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public GameObject questWindow;

    public void OpenQuestWindow() {
        questWindow.SetActive(true);
        
    }
}
