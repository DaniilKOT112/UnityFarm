
[System.Serializable]
public class Item
{
    public static int TYPEPFOOD = 1;
    public const int TYPEPLOW = 2;

    public string name;
    public string imgUrl;
    public int type;
    public int cnt;
    public int price;
    public int lvlWhenUnlock;
    public float timeToGrow;


    public Item(string name, string imgUrl, int cnt, int type, int price, int lvlWhenUnlock, float timeToGrow)
    {
        this.name = name;
        this.imgUrl = imgUrl;
        this.cnt = cnt;
        this.type = type;
        this.price = price;
        this.lvlWhenUnlock = lvlWhenUnlock;
        this.timeToGrow = timeToGrow;

    }

}
