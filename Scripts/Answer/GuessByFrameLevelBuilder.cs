using System;
using UnityEngine;
using UnityEngine.Events;

public class GuessByFrameLevelBuilder : MonoBehaviour
{
    public static UnityEvent<GuessByFrameData> UpdateQuestion = new UnityEvent<GuessByFrameData>();

    private GameObject _lastDatePrefab;
    private int _dataIndex = 0;
    
    [Space(5f)]
    [SerializeField] private GuessByFrameData _guessByFrameData;


    private void Awake()
    {
        UpdateQuestion.AddListener(SetEnvironment);
    }

    public void Build()
    {
        UpdateQuestion?.Invoke(_guessByFrameData);
    }

    public void UpdateDate(GuessByFrameData newData)
    {
        _guessByFrameData = newData;

        if (_lastDatePrefab is null == false)
            Destroy(_lastDatePrefab);
    }

    private void SetEnvironment(GuessByFrameData data)
    {
        if(data.FrameAndFields is null == true)
            return;

        _lastDatePrefab = Instantiate(_guessByFrameData.FrameAndFields);
    }

    private void OnDestroy()
    {
        UpdateQuestion.RemoveListener(SetEnvironment);
    }
}
