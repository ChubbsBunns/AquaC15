using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PipeSceneThing : MonoBehaviour
{
    Scroller Pipe;

    [SerializeField] private string nextSceneToLoad;

    public Scroller[] secondaryPipes;

    public GameObject player;

    public GameObject Nu_Sprite;

    public float lerp = 0.3f;

    public float smallLerp = 0.1f;
    public Transform placeToPlacePlayer;

    public bool exitPlayer;

    public Transform PlacePlayerHere;

    public CinemachineVirtualCamera CMVCAM;

    public Animator PipeSceneAnim;
    // Start is called before the first frame update
    void Start()
    {
        CMVCAM = FindObjectOfType<CinemachineVirtualCamera>();
        Pipe = FindObjectOfType<Scroller>();
        player = FindObjectOfType<Player_Controller_1>().gameObject;
        PipeSceneAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pipe.startAscending)
        {
            if (player.transform.position.y - placeToPlacePlayer.position.y <= 0.2f )
            {
                Debug.Log("not set static liao");
                Rigidbody2D thing = player.GetComponent<Rigidbody2D>();
                thing.gravityScale = 0;
                player.transform.position = new Vector3(placeToPlacePlayer.position.x, player.transform.position.y + lerp*Time.deltaTime, placeToPlacePlayer.position.z);
            }
            else
            {
                Debug.Log("Set static liao");
               
            }

            if(exitPlayer)
            {
                if (player.transform.position.y - PlacePlayerHere.position.y <= 0.2f )
                {
                    CMVCAM.Follow = null;
                    Rigidbody2D thing = player.GetComponent<Rigidbody2D>();
                    thing.gravityScale = 0;
                    player.transform.position = new Vector3( PlacePlayerHere.position.x, (player.transform.position.y + smallLerp*Time.deltaTime), placeToPlacePlayer.position.z);
                }
            }
//            player.transform.position = placeToPlacePlayer.position;
        }
    }

    void ExitThePlayer()
    {
        exitPlayer = true;
    }

    void LoadNextScene()
    {
        Player_Controller_1 player = FindObjectOfType<Player_Controller_1>();
        Destroy(player);
        SceneManager.LoadScene(nextSceneToLoad);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Help");
        if (other.CompareTag("Player"))
        {
            Pipe.startAscending = true;
            PipeSceneAnim.SetBool("StartCredits", true);
            Player_Health p_health  =player.GetComponent<Player_Health>();
            if (p_health == null)
            {
                Debug.Log("Player health is not found");
            }
            else
            {
                Debug.Log("Player health is found");
            }
            p_health.SwitchOffHearts();
        }
    }
}
