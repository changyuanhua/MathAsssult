﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill_multishot_weapon_controller : MonoBehaviour, skill
{
    private void Start()
    {
        is_ready = true;
        shot_reload_time = reload_delta;
    }

    private void Update()
    {
        SkillReload();

        if (ISCasting())
        {
            CastSkill();
        }
    }

    public void SkillReload()
    {
        if (!is_ready)
        {
            if (shot_reload_time >= reload_delta)
            {
                shot_reload_time = reload_delta;
                is_ready = true;
            }
            else
            {
                shot_reload_time += Time.deltaTime;
            }
        }
    }

    public void CastSkill()
    {
        if (is_ready)
        {
            for (int i = 0; i < 5; ++i)
            {
                Transform clone = Instantiate(shot,
                                              shot_spawn.position,
                                              shot_spawn.rotation);
                clone.transform.Rotate(Vector3.up * (-degree60 + degree30 * i));
                clone.GetComponent<player_bullet_controller>().controller
                    = controller;
            }

            is_ready = false;
            shot_reload_time = 0.0f;
            Debug.Log("CastSkill: Multishot");
        }
    }

    private bool ISCasting()
    {
        return (Input.GetButton("Fire2"));
    }

    public game_controller controller;

    public Transform shot;
    public Transform shot_spawn;
    private bool is_ready = true;
    public float reload_delta = 6.0f;

    private float _shot_reload_time;
    public float shot_reload_time
    {
        get { return _shot_reload_time; }
        protected set { _shot_reload_time = value; }
    }

    private const float degree60 = 60.0f;
    private const float degree30 = 30.0f;
}