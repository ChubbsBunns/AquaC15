using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCanvas : MonoBehaviour
{
    [SerializeField] Text moneyNumText;
    int money = 0;

    public static MoneyCanvas instance;

    private void Start()
    {
        if(instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            moneyNumText.text = money.ToString();
        }
    }

    public void IncreaseMoney(int num)
    {
        money += num;
        moneyNumText.text = money.ToString();
        //Currently only does this in this scene dont know how he wants to do across scenes
    }
}
