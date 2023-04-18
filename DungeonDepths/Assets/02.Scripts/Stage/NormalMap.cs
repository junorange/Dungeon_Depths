using System.Collections.Generic;
using UnityEngine;

public class NormalMap : Map
{
    [SerializeField]
    private MapCore core;
    public List<Vector3> boxSpawnPoints = new List<Vector3>();    // 보물 상자 스폰 Points
    public List<Vector3> EnemySpawnPoints = new List<Vector3>();  // 몬스터 스폰 Points

    public MapCore Core     //코어
    {
        get;
        private set;
    }

    public List<Vector3> GetWorldSpawnPoints()
    {
        List<Vector3> _pointList = new List<Vector3>();
        foreach (var _point in EnemySpawnPoints)
        {
            _pointList.Add(transform.TransformPoint(_point));
        }

        return _pointList;
    }

    List<Vector3> GetBoxSpawnPoints()
    {
        List<Vector3> _pointList = new List<Vector3>();
        foreach (var _point in boxSpawnPoints)
        {
            _pointList.Add(transform.TransformPoint(_point));
        }

        return _pointList;
    }

    public void SpawnBoxes()
    {
        if (boxSpawnPoints.Count < mapData.TotalBoxNum)
        {
            // 스크립터블 오브젝트에서 _totalBoxNum 수정
            Debug.LogError("TotalBoxNum가 points.Count 초과");
            return;
        }
        int _curBoxNum = 0;
        bool[] _randomCount = new bool[boxSpawnPoints.Count];
        while (mapData.TotalBoxNum > _curBoxNum)
        {
            int _index = Random.Range(0, boxSpawnPoints.Count);
            if (!_randomCount[_index])
            {
                _randomCount[_index] = true;
                PoolManager.Instance.Instantiate("Chest", GetBoxSpawnPoints()[_index], Quaternion.identity);
                _curBoxNum++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //editor
        for (int i = 0; i < EnemySpawnPoints.Count; i++) // 적 스폰 포인트 기즈모
        {
            Gizmos.DrawSphere(transform.TransformPoint(EnemySpawnPoints[i]), 0.5f);
        }

        Gizmos.color = Color.yellow;
        for (int i = 0; i < boxSpawnPoints.Count; i++) // 보물상자 기즈모
        {
            Gizmos.DrawSphere(transform.TransformPoint(boxSpawnPoints[i]), 0.5f);
        }
    }
    public override void Awake()
    {
        Debug.Log("NormalMap Awake()");

        base.Awake();
        core = GetComponentInChildren<MapCore>();
        /* TODO 주은
         * 상자, 몬스터 스폰 위치 설정 및 파싱
         * 시작 위치 설정 및 파싱
         * core 파괴 이벤트 설정
        */
        //core.OnEvent += () => { IsClear = true; Debug.Log("Map 이벤트 호출 : " + IsClear); };
    }
    // TODO 주은 상자 스폰 함수 구현?

}
