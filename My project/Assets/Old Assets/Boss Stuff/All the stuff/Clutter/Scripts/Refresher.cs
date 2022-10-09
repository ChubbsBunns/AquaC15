using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Refresher : MonoBehaviour
{

    public GrapplingHook player;
    public GameObject lineForGrapple;
    public CinemachineVirtualCamera vCam;
    // Start is called before the first frame update
    void Awake ()
    {
        player = FindObjectOfType<GrapplingHook>();
        player.FindMyCamera();
        lineForGrapple = Instantiate(lineForGrapple, transform);
        player.line = FindObjectOfType<LineRenderer>();
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
