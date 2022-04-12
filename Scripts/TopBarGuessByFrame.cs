using TMPro;
using UnityEngine;

public class TopBarGuessByFrame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumber;
    private int _levelCount;
    
    private void Awake()
    {
        _levelCount = Resources.LoadAll("Dates").Length;
        GuessByFrameLevelBuilder.UpdateQuestion.AddListener(UpdateLevelNumber);
        
    }
    
    private void UpdateLevelNumber(GuessByFrameData data)
    {
        _levelNumber.text = $"{data.DataId + 1}/{_levelCount}";
    }
}
