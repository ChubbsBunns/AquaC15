using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class portal : MonoBehaviour
{
    [Header("Portal Data")]
    public int portal_index;
    public int next_portal_index;
    public Transform transform_portal;

    [Header("Game log Data")]
    public string scene_name_to_load;
    public CinemachineVirtualCamera current_Vcam;
    public Game_Management_Logs game_management_logs;
    // Start is called before the first frame update

    [Header("Misc")]

    [SerializeField] bool doIDeletePlayer = false;

    [Header ("Miscellaneous")]
    [SerializeField] private bool DoIDeletePlayer = false;

    private void Awake()
    {
        game_management_logs = FindObjectOfType<Game_Management_Logs>();
        if (portal_index == game_management_logs.portal_index_to_spawn_at)
        {
            
            
            //game_management_logs.Instantiate_Player_Here(transform_portal); (THIS LOGIC IS FOR IF YOU WANT TO CREATE A NEW PLAYER OBJECT INTO THE SCENE)
            if (game_management_logs == null)
            {
                Debug.LogError("Game management Log is not found");
            }
            else
            {
                Debug.Log("Game Management Log is found");
            }
            Player_Controller_1 playerObject = FindObjectOfType<Player_Controller_1>();
            if (playerObject == null)
            {
                Debug.Log("Portal does not instantiate player");
            }
            else
            {
                playerObject.TransformChangeMoveMeHere(transform_portal.transform);
            }

        }
        else
        {
            Debug.Log("Not Spawning at any particular location as portal is unable to read the game management logs index");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            game_management_logs.portal_index_to_spawn_at = next_portal_index;
            if (doIDeletePlayer)
            {
                Destroy(collision.gameObject);
            }
            //Application.LoadLevelAdditive(scene_name_to_load);
            SceneManager.LoadScene(scene_name_to_load);
        }
    }
}
