using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Game_Management_Logs : MonoBehaviour
{
    [Header ("Game Objects to Hold")]
    public CinemachineVirtualCamera cm_vcam;
    public Player_Controller_1 player_character;

    public Player_Controller_1 placeholder_player_character;

    public CinemachineVirtualCamera current_cm_vcam;
    public Cinemachine_Editor cinemachine_editor;

    [Header("Unlocks")]
    public bool Able_To_Double_Jump;
    public bool Able_to_wall_jump;
    public bool Able_to_dash;

    [Header("Loading New Scene + Loading Save State Data")]
    public string SceneNameSaved;
    public int portal_index_to_spawn_at;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<Game_Management_Logs>().Length > 1)
        {
            //Destroy(gameObject);
            Debug.LogError("There are multiple game_management_logs in this scene");
            Debug.LogError(transform.position);
            Destroy(gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Get_Next_Portal_Index(int next_portal_index)
    {
        portal_index_to_spawn_at = next_portal_index;
    }

    public Player_Controller_1 This_Is_The_Current_Player()
    {
        player_character = FindObjectOfType<Player_Controller_1>();
        return player_character;
    }
    
    public void Instantiate_Player_Here(Transform portal_location)
    {

        placeholder_player_character = Instantiate(player_character, portal_location.position, Quaternion.identity);
        if (placeholder_player_character == null)
        {
            Debug.LogError("Player Character is NUll");
        }
        else
        {
            Debug.Log("Player Character exists in new scene");
        }
        current_cm_vcam = Instantiate(cm_vcam, portal_location.position, Quaternion.identity);
        if (current_cm_vcam == null)
        {
            Debug.LogError("Vcam is NULL");
        }
        else
        {
            Debug.Log("Vcam exists in new scene");
        }
        cinemachine_editor = current_cm_vcam.GetComponent<Cinemachine_Editor>();
        if (cinemachine_editor == null)
        {
            Debug.LogError("cinemachine editor is null");
        }
        else
        {
            Debug.Log("cinemachine editor exists");
        }
        cinemachine_editor.Follow_Player();
    }
}
