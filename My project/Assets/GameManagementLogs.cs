using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManagementLogs : MonoBehaviour
{
    public Player_Controller_1 playerCharacter;
    public int nextActivePortalIndex;
    public Transform currentRevivePoint;
    public CinemachineVirtualCamera vcam;
    public Player_Controller_1 playerCharacter_Placeholder;

    //unlocks to log
    public int maxNoOfJumpsForPlayer;
    public bool wallSlideUnlocked;
    public bool dashUnlocked;

    //revival logs
    public string sceneNameToLoadOnDeath;
    public bool reviveMe;

    //boss logs
    public bool boss1Dead;
    // Start is called before the first frame update
    void Start()
    {
        reviveMe = false;
        if (FindObjectsOfType<GameManagementLogs>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        ThisIsTheCurrentPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetNextPortalIndex(int nextPortalIndex)
    {
        nextActivePortalIndex = nextPortalIndex;
    }

    public Player_Controller_1 InstantiateMeHere(Transform myNewPosition)
    {
        playerCharacter_Placeholder = Instantiate(playerCharacter, myNewPosition.transform);
        return playerCharacter_Placeholder;
    }

    public void CreateVirtualCamera(Transform vcamPosition)
    {
        Instantiate(vcam, vcamPosition);
    }

    //remember to use this for revival
    public void ThisIsTheCurrentPlayer()
    {
        playerCharacter = FindObjectOfType<Player_Controller_1>();
    }
    public void PutMeHere(Transform playerNewPosition)
    {
        playerCharacter.transform.position = playerNewPosition.position;
    }

    public void RevivePlayer()
    {
        Debug.Log("hmmmmmmmmmmm");
        reviveMe = true;
        SceneManager.LoadScene(sceneNameToLoadOnDeath);
        playerCharacter.health = 6;
    }

}

