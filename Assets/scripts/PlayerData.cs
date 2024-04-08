[System.Serializable]
public class PlayerData
{
    public int money;
    public int lvl;
    public float lvlProgress;
    public string lastSavedTime;
    public long timeOffLine;

    public PlayerData (int money, int lvl, float lvlProgress, string lastSavedTime)
    {
        this.money = money;
        this.lvl = lvl;
        this.lvlProgress = lvlProgress;
        this.lastSavedTime = lastSavedTime;
    }
}
