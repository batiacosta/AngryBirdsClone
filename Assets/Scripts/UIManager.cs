
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Transform> finalPanels;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InGameDataSO inGameDataSo;
    [SerializeField] private Button useHeartButton;
    [SerializeField] private TextMeshProUGUI heartCounter;
    [SerializeField] private TextMeshProUGUI finalScoreTMP;

    private void Start()
    {
        HideFinalPanels();
        useHeartButton.enabled = true;
    }

    public void HideFinalPanels()
    {
        foreach (var panel in finalPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void ShowWinPanel()
    {
        HideFinalPanels();
        finalPanels[0].gameObject.SetActive(true);
        finalScoreTMP.text = levelManager.GetScore().ToString();
    }

    public void ShowLosePanel()
    {
        HideFinalPanels();
        finalPanels[1].gameObject.SetActive(true);
        var hearts = inGameDataSo.GetHearts();
        heartCounter.text = inGameDataSo.GetHearts().ToString();
        if (hearts == 0)
        {
            DisableHeartButton();
        }
    }

    public void UseHeart()
    {
        levelManager.UseHeart();
    }
    public void DisableHeartButton()
    {
        useHeartButton.enabled = false;
    }
}
