using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SetTargetToPlayer : MonoBehaviour
{
    public PlayerController_2 playerController;
    public AIDestinationSetter AIDestinationSetter;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController_2>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        SetPlayerAsTarget();
    }

    public void SetPlayerAsTarget()
    {
        AIDestinationSetter.target = playerController.transform;
    }

}
