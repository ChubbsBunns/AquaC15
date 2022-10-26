using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;


    public bool isReached() {
        return (currentAmount >= requiredAmount); 
    }

    public void EnemyKilled() {
        if (goalType == GoalType.Kill) 
            currentAmount++;
    }

    public void MineMined() {
        if (goalType == GoalType.Mine) 
            currentAmount++;
    }

    public override string ToString() {
        if (isReached()) {
            return "DONE!";
        } else {
            return currentAmount.ToString() + " / " + requiredAmount.ToString();
        }
    }

}

public enum GoalType
{
    Mine,
    Kill
}
