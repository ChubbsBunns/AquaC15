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
    public Player_Controller_1 player_current;
    public Game_Management_Logs game_management_logs;
    // Start is called before the first frame update

    private void Awake()
    {
        game_management_logs = FindObjectOfType<Game_Management_Logs>();
        if (portal_index == game_management_logs.portal_index_to_spawn_at)
        {
            game_management_logs.Instantiate_Player_Here(transform_portal);
            if (game_management_logs == null)
            {
                Debug.LogError("Game management Log is not found");
            }
            else
            {
                Debug.Log("Game Management Log is found");
            }
            player_current = FindObjectOfType<Player_Controller_1>();
            if (player_current == null)
            {
                Debug.Log("Portal does not instantiate player");
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
            SceneManager.LoadScene(scene_name_to_load);
        }
    }
}
