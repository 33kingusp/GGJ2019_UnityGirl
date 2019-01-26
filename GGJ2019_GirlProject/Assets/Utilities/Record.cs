/// <summary>
/// ステージクリア時に作成される成績データ
/// </summary>
public class Record
{
    public readonly int InitialYoujoCount;
    public readonly int SurvivedYoujoCount;
    public readonly float ClearedTimeSeconds;
    public Record(int initialYoujoCount, int survivedYoujoCount, float clearedTimeSeconds)
    {
        this.InitialYoujoCount = initialYoujoCount;
        this.SurvivedYoujoCount = survivedYoujoCount;
        this.ClearedTimeSeconds = clearedTimeSeconds;
    }
}
