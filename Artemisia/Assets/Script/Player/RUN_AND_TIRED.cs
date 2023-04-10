using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUN_AND_TIRED : MonoBehaviour
{
    private PLAYER_MOVE player_Move;
    private ENDURANCE SP;

    public float run_mutiple;
    public float tired_mutiple;
    public float smooth;
    private float temp_speed;
    void Start()
    {
        player_Move = GetComponent<PLAYER_MOVE>();
        SP = GetComponent<ENDURANCE>();
        temp_speed = player_Move.move_speed;
    }

    void Update()
    {
        
        if (SP.is_run)
        {
            player_Move.move_speed = Mathf.Lerp(player_Move.move_speed, temp_speed * run_mutiple, smooth*Time.deltaTime);
        }
        else if (SP.is_tired)
        {
            player_Move.move_speed = Mathf.Lerp(player_Move.move_speed, temp_speed * tired_mutiple, smooth * Time.deltaTime);
        }
        else
        {
            player_Move.move_speed = Mathf.Lerp(player_Move.move_speed, temp_speed, smooth * Time.deltaTime);
        }
    }
}
