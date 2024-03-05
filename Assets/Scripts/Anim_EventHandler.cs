using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_EventHandler : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void attack_Over()
    {
        player.Attacking_Anim_EventHandler();
    }
}
