using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TMPro;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BirdSelectionButton : MonoBehaviour
{
    [SerializeField] private Image buttonBirdImage;
    [SerializeField] private Image frame;
    [SerializeField] private TextMeshProUGUI indexNumber;
    [SerializeField] private CharacterSO birdSo;
    [SerializeField] private SelectionDataSO gameSelectionData;

    private bool _isPressed;
    private bool _canBeSelected;
    private int _index;
    private int _indexNumberFrame = 0;

    public void SelectBird()
    {
        if (!_isPressed)
        {
            _canBeSelected = !gameSelectionData.IsFilled();
            if (_canBeSelected)
            {
                gameSelectionData.AddSelectedBird(_index);
                frame.gameObject.SetActive(true);
                _indexNumberFrame = gameSelectionData.GetIndex(_index) + 1;
                indexNumber.text = (_indexNumberFrame).ToString();
            }
            _isPressed = true;
        }
        else
        {
            frame.gameObject.SetActive(false);
            gameSelectionData.RemoveSelectedBird(_index);
            _isPressed = false;
        }
    }
    
    private void Start()
    {
        InitializeButton();
    }

    private void OnEnable()
    {
        InitializeButton();
    }
    private void InitializeButton()
    {
        _isPressed = false;
        frame.gameObject.SetActive(false);
        _index = birdSo.index;
        GetComponent<Image>().sprite = birdSo.characterUIReference;
        gameSelectionData.OnSelectionChanged += UpdateFrameNumber;
    }

    private void UpdateFrameNumber(object sender, bool isFilled)
    {
        if (!isFilled)
        {
            _indexNumberFrame = gameSelectionData.GetIndex(_index) + 1;
            indexNumber.text = (_indexNumberFrame).ToString();
        }
    }
    
}
