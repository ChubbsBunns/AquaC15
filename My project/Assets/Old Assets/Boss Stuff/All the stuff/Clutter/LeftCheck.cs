using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCheck : MonoBehaviour
{
    public Collider2D leftCheck;

    public bool leftPlayerCheck;

    public PlayerAreaCheckMaster playerAreaCheckMaster;

    public int timeToDisable;
    // Start is called before the first frame update
    void Start()
    {
        leftCheck = GetComponent<Collider2D>();
        leftCheck.enabled = false;
        playerAreaCheckMaster = FindObjectOfType<PlayerAreaCheckMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMe()
    {
        leftCheck.enabled = true;
        StartCoroutine(DisableCountdown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            leftPlayerCheck = true;
            playerAreaCheckMaster.leftCheck = true;
        }
        else
        {
            leftPlayerCheck = false;
        }
    }

    public void GiveMasterLeftCheckData()
    {
        if (leftPlayerCheck)
        {
            playerAreaCheckMaster.leftCheck = true;
            StartCoroutine(MakeLeftCheckFalse());
        }
        else
        {
            playerAreaCheckMaster.leftCheck = false;
        }
    }

    IEnumerator DisableCountdown()
    {
        yield return new WaitForSeconds(timeToDisable);
        leftCheck.enabled = false;
        playerAreaCheckMaster.leftCheck = false;
    }

    IEnumerator MakeLeftCheckFalse()
    {
        yield return new WaitForEndOfFrame();
        leftPlayerCheck = false;
    }
}
