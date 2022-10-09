using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCheck : MonoBehaviour
{
    public Collider2D rightCheck;

    public bool rightPlayerCheck;

    public PlayerAreaCheckMaster playerAreaCheckMaster;

    public int timeToDisable;
    // Start is called before the first frame update
    void Start()
    {
        rightCheck = GetComponent<Collider2D>();
        rightCheck.enabled = false;
        playerAreaCheckMaster = FindObjectOfType<PlayerAreaCheckMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMe()
    {
        rightCheck.enabled = true;
        StartCoroutine(DisableCountdown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rightPlayerCheck = true;
            playerAreaCheckMaster.rightCheck = true;
        }
    }

    public void GiveMasterRightCheckData()
    {
        if (rightPlayerCheck)
        {
            playerAreaCheckMaster.rightCheck = true;
            StartCoroutine(MakeRightCheckFalse());
        }
        else
        {
            playerAreaCheckMaster.rightCheck = false;
        }
    }

    IEnumerator DisableCountdown()
    {
        yield return new WaitForSeconds(timeToDisable);
        rightCheck.enabled = false;
        playerAreaCheckMaster.rightCheck = false;
    }

    IEnumerator MakeRightCheckFalse()
    {
        yield return new WaitForEndOfFrame();
        rightPlayerCheck = false;
    }
}
