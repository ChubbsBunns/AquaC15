using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAreaCheckMaster : MonoBehaviour
{

    public bool rightCheck;
    public bool leftCheck;
    // 1 = left
    // 2 = right
    // I in the DoIGoLeftOrRightIndex refers to the Swordsman. 
    // hence if index is 1 (left), The Swordsman must appear to the left of the player and vice versa if the index is 2
    public int DoIGoLeftOrRightIndex;

    public RightCheck rightCheckObject;
    public LeftCheck leftCheckObject;
    // Start is called before the first frame update
    void Start()
    {
        rightCheckObject = FindObjectOfType<RightCheck>();
        leftCheckObject = FindObjectOfType<LeftCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTheData()
    {
        rightCheckObject.EnableMe();
        StartCoroutine(waitForEndOfFrame());
        leftCheckObject.EnableMe();
        leftCheckObject.GiveMasterLeftCheckData();
        rightCheckObject.GiveMasterRightCheckData();
    }

    IEnumerator waitForEndOfFrame()
    {
        yield return new WaitForSeconds(0.2f);
    }

    public void GetAttackIndex()
    {
        if (rightCheck && leftCheck)
        {
            DoIGoLeftOrRightIndex = Random.Range(1, 3);
        }
        else if (rightCheck == true && leftCheck == false)
        {
            DoIGoLeftOrRightIndex = 1;
        }
        else if (rightCheck == false && leftCheck == true)
        {
            DoIGoLeftOrRightIndex = 2;
        }
        else
        {
            Debug.Log("help la");
        }
    }
}
