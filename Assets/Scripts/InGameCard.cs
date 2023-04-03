using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class InGameCard : MonoBehaviour
{
    private BirdSO _birdSO;
    [SerializeField] private InGameDataSO inGameDataSO;
    [SerializeField] private TextMeshProUGUI quantityTMP;

    private Image _cardImage;

    public void SelectBird()
    {
        inGameDataSO.SetNewBird(_birdSO);
    }

    public void SetInGameCard(BirdSO birdSO)
    {
        _birdSO = birdSO;
        _cardImage = GetComponent<Button>().image;
        _cardImage.sprite = birdSO.birdUIReference;
    }

    public void UpdateQuantity(int newNumber)
    {
        quantityTMP.text = newNumber.ToString();
        GetComponent<Button>().enabled = newNumber != 0;
    }
    
}
