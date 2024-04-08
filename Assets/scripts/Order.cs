using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public int id;

    List<Image> productImages;
    List<Text> productCnt;
    List<Item> required = new List<Item>();
    List<Item> allItems = new List<Item>();

    private Text reward;
    private int moneyToRecive;
    private int expToRecive;

    void Start()
    {
        reward = GetComponentsInChildren<Text>()[0];
        productImages = GetComponentsInChildren<Image>().ToList();
        productCnt = GetComponentsInChildren<Text>().ToList();

        GetComponentInChildren<Button>().onClick.AddListener(delegate { completeOrder(); });

       if (Player_menu.lvl > 0) allItems.Add(new Item("carrot", "Food/carrot", 1, Item.TYPEPFOOD, 10, 1, 5f));
       if (Player_menu.lvl > 1) allItems.Add(new Item("beet", "Food/beet", 1, Item.TYPEPFOOD, 10, 2, 5f));
       if (Player_menu.lvl > 2) allItems.Add(new Item("pumpkin", "Food/pumpkin", 1, Item.TYPEPFOOD, 10, 3, 5f));
       if (Player_menu.lvl > 3) allItems.Add(new Item("eggplant", "Food/eggplant", 1, Item.TYPEPFOOD, 10, 4, 5f));

        createOrder();
        
    }


    private void createItemForOrder()
    {
        int productIndex = Random.Range(0, allItems.Count);
        Item product = allItems[productIndex];
        product.cnt = generateAmount();
        allItems.Remove(product);
        required.Add(product);
    }


    private void createOrder()
    {
        if (Player_menu.currentOrders[id].Count == 0)
        {
            for (int i = 0; i < generateCount(); i++)
            {
                createItemForOrder();
            }
        }
        
        else required = Player_menu.currentOrders[id];
            
        for (int i = 0; i < required.Count; i++)
        {
            productImages[i + 1].sprite = Resources.Load<Sprite>(required[i].imgUrl);
            productCnt[i + 2].text = "x" + required[i].cnt.ToString();
        }
        generateReward();
        Player_menu.currentOrders[id] = required;
    }
  private void completeOrder()
    {
        if (Player_menu.isEnoughItems(required))
        {
            for (int i = 0; i < required.Count; i++)
            {
                Player_menu.removeItemWithName(required[i].name, required[i].cnt);
            }
            Player_menu.addExp(expToRecive);
            Player_menu.money += moneyToRecive;
            Player_menu.addExp(1);
            Player_menu.currentOrders[id].Clear();
            DataBase.Save();
            Destroy(gameObject);
        }

    }

    private void generateReward()
    {
        switch (Player_menu.lvl) 
        {
            case 1:
                moneyToRecive = Random.Range(6, 15);
                expToRecive = Random.Range(1, 3);
                break;
            case 2:
                moneyToRecive = Random.Range(9, 21);
                expToRecive = Random.Range(1, 5);
                break;
            default:
                moneyToRecive = Random.Range(20, 35);
                expToRecive = Random.Range(4, 8);
                break;
        }
        reward.text = $"You will recive {expToRecive} exp and {moneyToRecive}$";
    }
    
  
    private int generateCount()
    {
        if (Player_menu.lvl == 1) return 1;
        if (Player_menu.lvl == 2) return Random.Range(1, 3);
        if (Player_menu.lvl == 3) return Random.Range(1, 3);
        if (Player_menu.lvl == 4) return Random.Range(2, 4);

        return 3;
    }

    private int generateAmount()
    {
        if (Player_menu.lvl == 1) return Random.Range(1, 3);
        if (Player_menu.lvl == 2) return Random.Range(1, 3);
        if (Player_menu.lvl == 3) return Random.Range(1, 3);
        if (Player_menu.lvl == 4) return Random.Range(2, 4);

        return 4;
    }
}
