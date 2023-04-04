using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthManagerUI : MonoBehaviour
{
    [SerializeField] private List<Image> hearts;
    [SerializeField] private Sprite[] heartState;

    public void SetHearts(int heartQuantity)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i<heartQuantity)
            {
                hearts[i].sprite = heartState[0];
            }
            else
            {
                hearts[i].sprite = heartState[1];
            }
        }
    }
}
