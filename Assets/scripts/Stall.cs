using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stall : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer stallSpriteRender;
    private bool allowClick = false;
    void Start()
    {
        stallSpriteRender = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        inventory = GameObject.FindWithTag("Inventory");

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (allowClick)
        {
            inventory.GetComponent<Menu>().OpenShop();
        }
    }




    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 0.4f)
        {
            allowClick = true;
            stallSpriteRender.sprite = Resources.Load<Sprite>("Tools/Shop");
        }
        else
        {
            allowClick = false;
            stallSpriteRender.sprite = Resources.Load<Sprite>("Tools/ShopEmpty");
        }

    }
}
