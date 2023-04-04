using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSceneManager : MonoBehaviour
{
    [SerializeField] private SelectionDataSO gameSelectionData;
    [SerializeField] private GameObject playButton;
    void Start()
    {
        gameSelectionData.OnSelectionChanged += UpdatePlayButton;
        playButton.gameObject.SetActive(false);
        gameSelectionData.ResetSelectedCards();
    }

    private void OnDestroy()
    {
        gameSelectionData.OnSelectionChanged -= UpdatePlayButton;
    }

    private void UpdatePlayButton(object sender, bool isFilled)
    {
        Debug.Log($"El boton se llama {playButton.gameObject.name}");
        playButton.gameObject.SetActive(isFilled);
    }
}
