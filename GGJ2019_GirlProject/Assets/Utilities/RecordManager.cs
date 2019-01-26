using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 成績データをどこからでも操作できるようにするシングルトン
/// </summary>
public class RecordManager : MonoBehaviour
{
    private static RecordManager _instance;
    public static RecordManager Instance
    {
        get { return _instance; }
    }
    private Dictionary<int, Record> Records;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        Initialize();
    }

    /// <summary>
    /// ステージクリア時に成績を保存する
    /// </summary>
    /// <params name="stageID">ステージのID</params>
    /// <params name="initialYoujoCount">最初にいた幼女の数</params>
    /// <params name="survivedYoujoCount">生き残った幼女の数</params>
    /// <params name="clearedTimeSeconds">クリアまでにかかった秒数</params>
    /// <returns>取得したいステージの成績データ</returns>
    public void SaveRecord(int stageId, int initialYoujoCount, int survivedYoujoCount, float clearedTimeSeconds)
    {
        Record rec = new Record(initialYoujoCount, survivedYoujoCount, clearedTimeSeconds);
        Records[stageId] = rec;
    }

    /// <summary>
    /// 指定したステージIDの成績を取得
    /// </summary>
    /// <params name="stageID">取得したいステージのID</params>
    /// <returns>取得したいステージの成績データ</returns>
    public Record GetRecord(int stageId)
    {
        if(!Records.ContainsKey(stageId))
        {
            return null;
        }

        return Records[stageId];
    }

    /// <summary>
    /// ゲーム開始時、ゲーム全部リトライするときに呼ぶ
    /// </summary>
    public void Initialize()
    {
        Records = new Dictionary<int, Record>();
    }


    /// <summary>
    /// 成績を永続化する
    /// </summary>
    private void SaveRecordPersistently()
    {

    }
}
