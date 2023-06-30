using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class envanter_slot_manager : MonoBehaviour
{
    public RawImage raw_image;
    player_controler player_controler;
    

    public Texture2D[] envanter_sprite;

    private void Awake()
    {
        player_controler = GameObject.FindGameObjectWithTag("player").GetComponent<player_controler>();
    }

    private void Update()
    {
        switch (player_controler.envanter_slot_sellec)
        {
            case 0:
                raw_image.texture = envanter_sprite[0];
                break;
            case 1:
                raw_image.texture = envanter_sprite[1];
                break;
            case 2:
                raw_image.texture = envanter_sprite[2];
                break;
            case 3:
                raw_image.texture = envanter_sprite[3];
                break;
            case 4:
                raw_image.texture = envanter_sprite[4];
                break;
            case 5:
                raw_image.texture = envanter_sprite[5];
                break;
        }
    }
}
