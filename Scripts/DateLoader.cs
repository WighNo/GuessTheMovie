using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DateLoader : MonoBehaviour, ISavableObject
{
    public const int MaximumLevel = 50;
    
    private GuessByFrameLevelBuilder _levelBuilder;

    public static DateLoader Instance { get; private set; }

    private static Dictionary<int, GuessByFrameData> _guessByFrameDatas = new Dictionary<int, GuessByFrameData>();
    private GuessByFrameData[] _dates;

    private string _saveKey = "LevelDate";
    
    public UnityEvent LaodNextLevel { get; private set; } = new UnityEvent();

    private int _dateId;
    public int DateId => _dateId;

    private void Awake()
    {
        Instance = this;

        _levelBuilder = FindObjectOfType<GuessByFrameLevelBuilder>().GetComponent<GuessByFrameLevelBuilder>();
        
        _dates = Resources.LoadAll<GuessByFrameData>("Dates");
        if (_guessByFrameDatas.Count == 0)
        {
            for (int i = 0; i < _dates.Length; i++)
                _guessByFrameDatas.Add(_dates[i].DataId, _dates[i]);
        }
        
        CheckerGuessByFrame.Instance.CompleteLevel.AddListener(CompleteLevel);
    }

    private void Start()
    {
        Load();
        
        LevelCheck();
        
        _levelBuilder.UpdateDate(_guessByFrameDatas[_dateId]);
        _levelBuilder.Build();
    }

    private void LevelCheck()
    {
        if (_dateId >= MaximumLevel)
        {
            _dateId = 0;
            Save();
        }
    }
    
    private void CompleteLevel()
    {
        _dateId++;
        Save();   
    }
    
    public void NextDate()
    {
        _levelBuilder.UpdateDate(_guessByFrameDatas[_dateId]);
        _levelBuilder.Build();
        
        LaodNextLevel?.Invoke();
    }

    public void Save()
    {
        SaveSystem.Save(_saveKey, GetSaveSnapshot());
    }

    public void Load()
    {
        var data = SaveSystem.Load<SavedDates.LevelDate>(_saveKey);
        _dateId = data.DateId;
    }

    public object GetSaveSnapshot()
    {
        object data = new SavedDates.LevelDate()
        {
            DateId = _dateId
        };
        return data;
    }

    private void OnDestroy()
    {
        CheckerGuessByFrame.Instance.CompleteLevel.RemoveListener(CompleteLevel);
    }
}
