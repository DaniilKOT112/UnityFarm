using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private int STEP_EMPTY = 0;
    private int STEP_GROWS = 1;
    private int STEP_READY = 2;
    private int STEP_PLOW = 3;
    
    private SpriteRenderer seedSpriteRenderer;
    private SpriteRenderer productSpriteRenderer;
    private SpriteRenderer cropSpriteRenderer;

    private string timeGrowStarted = "";
    private Item cropItem;
    private int step = 0;

    private GameObject player;
    private bool readyForAction;
    
    void Start()
    {
        cropSpriteRenderer = GetComponent<SpriteRenderer>();
        seedSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        productSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[2];
        player = GameObject.FindWithTag("Player");
    }


    private void OnMouseDown()
    {
        Item item = Player_menu.getHandItem();

        if (readyForAction)
        {
            if (step == STEP_EMPTY)
            {
                if (item.type == Item.TYPEPFOOD)
                {
                    Player_menu.removeItem();
                    step = STEP_GROWS;
                    cropItem = item;
                    cropItem.cnt = 2;
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");
                    timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                    StartCoroutine(grow());
                }
            }
            else if (step == STEP_READY)
            {
                productSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
                seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/extraDirt");
                Player_menu.checkIfItemExists(cropItem);

                step = STEP_PLOW;
            }
            else if (step == STEP_PLOW)
            {
                if (item.type == Item.TYPEPLOW)
                {
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
                    step = STEP_EMPTY;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (step != STEP_GROWS)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < 0.2f)
            {
                readyForAction = true;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/cropSelected");
            }
            else
            {
                readyForAction = false;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/crop");
            }
        }
        else
        {
            cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/crop");
        }
    }

    public Item Save()
    {
        if (step == STEP_GROWS)
        {
            var currentTime = System.DateTime.Now;
            var lastSavedTimeConverted = System.Convert.ToInt64(timeGrowStarted);
            System.DateTime oldTime = System.DateTime.FromBinary(lastSavedTimeConverted);
            System.TimeSpan difference = currentTime.Subtract(oldTime);

            cropItem.timeToGrow = (long)difference.TotalSeconds;
            cropItem.type = STEP_GROWS;
            return cropItem;
        }
        else if (step == STEP_READY)
        {
            cropItem.type = STEP_READY;
            return cropItem;
        }
        else if (step == STEP_PLOW)
        {
            var saveItem = Player_menu.getEmptyItem();
            saveItem.type = STEP_PLOW;
            return saveItem;
        }
        else return Player_menu.getEmptyItem();
    }

    public void LaunchInitProcess(long offlineTime, Item item)
    {
        StartCoroutine(UpdateWithSavedData(offlineTime, item));
    }

    public IEnumerator UpdateWithSavedData (long offlineTime, Item item)
    {
        yield return new WaitForSeconds(1f);

        if (item.type == STEP_GROWS)
        {
            item.type = Item.TYPEPFOOD;
            cropItem = item;

            if (offlineTime > item.timeToGrow)
            {
                OnStepReady();
            }
            else
            {
                cropItem.timeToGrow = item.timeToGrow - offlineTime;
                seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");
                timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                StartCoroutine(grow());
            }
        }
        else if (item.type == STEP_READY)
        {
            item.type = Item.TYPEPFOOD;
            cropItem = item;
            OnStepReady();
        }
        else if (item.type == STEP_PLOW)
        {
            productSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
            seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/extraDirt");
            step = STEP_PLOW;
        }
    }

    private void OnStepReady()
    {
        seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
        productSpriteRenderer.sprite = Resources.Load<Sprite>(cropItem.imgUrl);
        step = STEP_READY;
    }

    private IEnumerator grow()
    {
        yield return new WaitForSeconds(cropItem.timeToGrow);
        OnStepReady();
    }
}
