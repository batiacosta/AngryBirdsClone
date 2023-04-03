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
    private CharacterSO _birdSo;
    [SerializeField] private InGameDataSO inGameDataSO;
    [SerializeField] private TextMeshProUGUI quantityTMP;

    private Image _cardImage;

    public void SelectBird()
    {
        inGameDataSO.SetNewBird(_birdSo);
    }

    public void SetInGameCard(CharacterSO characterSo)
    {
        _birdSo = characterSo;
        _cardImage = GetComponent<Button>().image;
        _cardImage.sprite = characterSo.characterUIReference;
    }

    public void UpdateQuantity(int newNumber)
    {
        quantityTMP.text = newNumber.ToString();
        GetComponent<Button>().enabled = newNumber != 0;
    }
    
}
