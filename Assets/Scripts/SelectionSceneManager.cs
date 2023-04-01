using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSceneManager : MonoBehaviour
{
    [SerializeField] private SelectionDataSO gameSelectionData;
    [SerializeField] private Button playButton;
    void Start()
    {
        gameSelectionData.OnSelectionChanged += UpdatePlayButton;
        playButton.gameObject.SetActive(false);
    }

    private void UpdatePlayButton(object sender, bool isFilled)
    {
        playButton.gameObject.SetActive(isFilled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
