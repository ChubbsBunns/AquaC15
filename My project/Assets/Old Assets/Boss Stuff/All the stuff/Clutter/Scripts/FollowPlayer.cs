using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowPlayer : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public PlayerController_2 playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        playerCharacter = FindObjectOfType<PlayerController_2>();
        vcam.Follow = playerCharacter.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        playerCharacter = FindObjectOfType<PlayerController_2>();
        vcam.Follow = playerCharacter.transform;
    }
}
