using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;


public class CardsManager : MonoBehaviour
{
    [SerializeField] private List<InGameCard> cards;
    [SerializeField] private InGameDataSO inGameDataSO;
    private List<BirdSOLibrarySO.BirdData> birdListSO;

    private void Start()
    {
        birdListSO = inGameDataSO.GetBirdSOList();
        SetCardsVisuals();
        inGameDataSO.OnBirdQuantityChange += UpdateQuantity;
    }

    private void UpdateQuantity(object sender, List<BirdSOLibrarySO.BirdData> birdsData)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].UpdateQuantity(birdsData[i].Quantity);
        }
    }

    private void SetCardsVisuals()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetInGameCard(birdListSO[i].BirdSO);
            cards[i].UpdateQuantity(birdListSO[i].Quantity);
        }
    }
    
}
