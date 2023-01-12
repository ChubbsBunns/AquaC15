 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroSceneManager : MonoBehaviour
{
    
    public GameObject leafcopter;
    public GameObject firemite;
    public GameObject rockmite;

    public GameObject aquamite;
    public GameObject airmite;
    
    public GameObject[] thingsToLookAt;

    public CinemachineVirtualCamera currCmvcam;

    private Baby_Nu_Controller player;

    public SpeakerManager spkrMng;

    private void Awake() {
        spkrMng = GetComponent<SpeakerManager>();
        currCmvcam = FindObjectOfType<CinemachineVirtualCamera>();
        thingsToLookAt[0] = leafcopter;
        thingsToLookAt[1] = firemite;
        thingsToLookAt[2] = rockmite;
        thingsToLookAt[3] = aquamite;
        thingsToLookAt[4] = airmite;
        player = FindObjectOfType<Baby_Nu_Controller>();
    }
    void followTarget(int which)
    {
        currCmvcam.LookAt = thingsToLookAt[which].transform;
        currCmvcam.Follow = thingsToLookAt[which].transform;
    }

    void playerStopMove()
    {
        player.player_is_controllable = false;
    }

    void playerContinueToMove()
    {
        currCmvcam.LookAt = player.transform;
        currCmvcam.Follow = player.transform;
        player.player_is_controllable = true;
    }

    void speakMessage(int i)
    {
        Debug.Log("Spearking" + i);
        spkrMng.Speak(i);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        spkrMng.Speak(6);
    }
}
