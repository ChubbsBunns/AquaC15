using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemachine_Editor : MonoBehaviour
{
    public CinemachineVirtualCamera vcam_main;
    public Player_Controller_1 player;
    public float y_offset;
    // Start is called before the first frame update
    private void Awake()
    {
        vcam_main = GetComponent<CinemachineVirtualCamera>();
        if (vcam_main == null)
        {
            Debug.Log("No vcam found");
        }
        player = FindObjectOfType<Player_Controller_1>();
        if (player == null)
        {
            Debug.Log("No Player found");
        }
        vcam_main.Follow = player.transform;
    }

    public void Follow_Player()
    {
        player = FindObjectOfType<Player_Controller_1>();
        if (vcam_main == null)
        {
            Debug.LogError("vcam is null");
        }
        else
        {
            Debug.Log("vcam is ok");
        }

        if (player == null)
        {
            Debug.LogError("Player is null");
        }
        else
        {
            Debug.Log("Player is fine");
        }
        vcam_main.Follow = player.transform;

    }
}
