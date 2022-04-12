using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GuessByFrame/Data", order = 0)]
public class GuessByFrameData : ScriptableObject
{
    [SerializeField] private string _russianTitle;
    public string RussianTitle => _russianTitle;

    [Space(15f)]
    [SerializeField] private Sprite _sourceImage;
    public Sprite SourceImage => _sourceImage;
    
    [Space(5f)]
    [SerializeField] private int _dataId;
    public int DataId => _dataId;
    
    [Space(15f)]
    [SerializeField] private GameObject _frameAndFields;
    public GameObject FrameAndFields => _frameAndFields;
    
    private void Reset()
    {
        GuessByFrameData[] guessByFrameDates = Resources.LoadAll<GuessByFrameData>("Dates");

        _dataId = guessByFrameDates.Length;
        name = $"GuessByFrameData_{_dataId}";
    }

    public void SetFrameAndFields(GameObject frameAndFields)
    {
        _frameAndFields = frameAndFields;
    }

    public void SetSourceImage(Sprite sprite)
    {
        _sourceImage = sprite;
    }
    
    [Serializable]
    public class MovieTitles
    {
        [SerializeField] public string RussianTitle;
        [SerializeField] public string EnglishTitle;
    }
}
