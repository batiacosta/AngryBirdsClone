
using System;
using ScriptableObjects;
using TMPro;

using UnityEngine;

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
    private Animator _animator;
    private static readonly int IsSelected = Animator.StringToHash("IsSelected");

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
        _animator.SetBool(IsSelected, _isPressed);
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
        _animator = GetComponent<Animator>();
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

    private void OnDestroy()
    {
        gameSelectionData.OnSelectionChanged -= UpdateFrameNumber;
    }
}
