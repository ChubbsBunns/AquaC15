using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestWindow : MonoBehaviour
{
    [SerializeField] Text textTitle1;
    [SerializeField] Text textGoal1;
    [SerializeField] Text textTitle2;
    [SerializeField] Text textGoal2;

    [SerializeField] Quest quest1;
    [SerializeField] Quest quest2;

    public static QuestWindow instance;

    private void Start()
    {
        if(instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            textTitle1.text = quest1.title + " :";
            textGoal1.text = quest1.goal.ToString();
            textTitle2.text = quest2.title + " :";
            textGoal2.text = quest2.goal.ToString();
        }
    }

    public void Update() {
        textGoal1.text = quest1.goal.ToString();
        textGoal2.text = quest2.goal.ToString();
    }
}

