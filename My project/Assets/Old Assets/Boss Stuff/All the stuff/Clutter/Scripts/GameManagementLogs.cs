using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagementLogs : MonoBehaviour
{
    public PlayerController_2 playerCharacter;
    public int nextActivePortalIndex;
    public Transform currentRevivePoint;
    public CinemachineVirtualCamera vcam;
    public PlayerController_2 playerCharacter_Placeholder;

    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
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

    public PlayerController_2 InstantiateMeHere(Transform myNewPosition)
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
        playerCharacter = FindObjectOfType<PlayerController_2>();
    }

    public void PutMeHere(Transform playerNewPosition)
    {
        playerCharacter.transform.position = playerNewPosition.position;
    }

}
