using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer deskSpriteRender;
    private bool allowClick = false;
    void Start()
    {
        deskSpriteRender = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");
        
    }

    private void OnMouseDown()
    {
        if (allowClick)
        {
            inventory.GetComponent<Menu>().OpenDesk();
        }
    }




    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 0.4f)
        {
            allowClick = true;
            deskSpriteRender.sprite = Resources.Load<Sprite>("Tools/DeskSelected");
        }
        else
        {
            allowClick = false;
            deskSpriteRender.sprite = Resources.Load<Sprite>("Tools/Desk");
        }

    }
}
