using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutSceneManager : MonoBehaviour
{
    public CinemachineVirtualCamera CMVCAM_Current;
    public CinemachineFramingTransposer CMVCAM_F_T;

    public float lerpValueForValueChange = 0.1f;

    public int Zoom_Out_FOV;
    public int Zoom_in_FOV;

    public bool ZoomInNow = false;
    public bool ZoomOutNow = false;

    public int Zoom_Out_tracked_object_offset_Y;
    public int Zoom_In_tracked_object_offset_Y;
    // Start is called before the first frame update
    void Start()
    {
        CMVCAM_Current = FindObjectOfType<CinemachineVirtualCamera>();
        CMVCAM_F_T = FindObjectOfType<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ZoomInNow)
        {
            ZoomIn();
        }
        if (ZoomOutNow)
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        //to see the player again
        if (Mathf.Abs(CMVCAM_Current.m_Lens.FieldOfView - Zoom_in_FOV) >= 0.1f)
        {
            CMVCAM_Current.m_Lens.FieldOfView -= lerpValueForValueChange*Time.deltaTime;    
        }
        if (Mathf.Abs(CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_In_tracked_object_offset_Y) >= 0.1f)
        {
            if (CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_In_tracked_object_offset_Y > 0)
            {
                CMVCAM_F_T.m_TrackedObjectOffset.y -= lerpValueForValueChange*Time.deltaTime;
            }
            else
            {
                CMVCAM_F_T.m_TrackedObjectOffset.y += lerpValueForValueChange*Time.deltaTime;
            }
        }
        
        if ((Mathf.Abs(CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_In_tracked_object_offset_Y) <= 0.1f) && (Mathf.Abs(CMVCAM_Current.m_Lens.FieldOfView - Zoom_in_FOV) <= 0.1f))
        {
            ZoomInNow = false;
            gameObject.SetActive(false);
        }

        //CMVCAM_Current.m_Lens.FieldOfView = Zoom_in_FOV;
        //CMVCAM_F_T.m_TrackedObjectOffset.y = Zoom_In_tracked_object_offset_Y;
    }

    public void ZoomOut()
    {
        //iusually used to see the scenery

        Debug.Log("Help 1");
        if (Mathf.Abs(CMVCAM_Current.m_Lens.FieldOfView - Zoom_Out_FOV) >= 0.01f)
        {
            Debug.Log("Help 2");
            CMVCAM_Current.m_Lens.FieldOfView += lerpValueForValueChange*Time.deltaTime;    
        }
        else{
            Debug.Log(Mathf.Abs(CMVCAM_Current.m_Lens.FieldOfView - Zoom_Out_FOV));
        }
        if (Mathf.Abs(CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_Out_tracked_object_offset_Y) >= 0.01f)
        {
            Debug.Log("Help 3");
            if (CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_Out_tracked_object_offset_Y > 0)
            {
                CMVCAM_F_T.m_TrackedObjectOffset.y -= lerpValueForValueChange*Time.deltaTime;
            }
            else
            {
                CMVCAM_F_T.m_TrackedObjectOffset.y += lerpValueForValueChange*Time.deltaTime;
            }
        }
        else
        {
            Debug.Log(Mathf.Abs(CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_Out_tracked_object_offset_Y));
        }

        if ((Mathf.Abs(CMVCAM_Current.m_Lens.FieldOfView - Zoom_Out_FOV) <= 0.1f) && (Mathf.Abs(CMVCAM_F_T.m_TrackedObjectOffset.y - Zoom_Out_tracked_object_offset_Y) <= 0.1f))
        {
            ZoomOutNow = false;
        }

        //CMVCAM_Current.m_Lens.FieldOfView = Zoom_Out_FOV;
        //CMVCAM_F_T.m_TrackedObjectOffset.y = Zoom_Out_tracked_object_offset_Y;
    }
}
