using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "InGameDataSO", menuName = "GameSettingUpSO/InGameDataSO", order = 0)]
    public class InGameDataSO : ScriptableObject
    {
        
        [SerializeField] private BirdSOLibrarySO birdSoLibrarySO;
        [SerializeField] private SelectionDataSO selectionDataSO;
        
        public EventHandler<BirdSO> OnCurrentBirdSOChanged;
        public EventHandler<List<BirdSOLibrarySO.BirdData>> OnBirdQuantityChange;

        public BirdSO CurrentBirdSO
        {
            get => _currentBirdSO;
        }

        private BirdSO _currentBirdSO;
        private List<BirdSOLibrarySO.BirdData> _selectedBirdsData = new List<BirdSOLibrarySO.BirdData>();
        public void SetNewBird(BirdSO birdSO)
        {
            _currentBirdSO = birdSO;
            OnCurrentBirdSOChanged?.Invoke(this, _currentBirdSO);
        }
        
        public List<BirdSOLibrarySO.BirdData> GetBirdSOList()
        {
            var indexes = selectionDataSO.GetIndexesList();
            foreach (var index in indexes)
            {
                _selectedBirdsData.Add(birdSoLibrarySO.BirdsData[index]);
            }

            SetNewBird(_selectedBirdsData[0].BirdSO);

            return _selectedBirdsData;
        }

        public void SetBirdQuantityInGame(int index, bool isAdditive)
        {
            var libraryIndex = _selectedBirdsData[index].BirdSO.index;
            if (isAdditive)
            {
                _selectedBirdsData[index].Quantity++;
                birdSoLibrarySO.BirdsData[libraryIndex].Quantity++;
            }
            else
            {
                _selectedBirdsData[index].Quantity--;
                birdSoLibrarySO.BirdsData[libraryIndex].Quantity--;
            }
            OnBirdQuantityChange?.Invoke(this, _selectedBirdsData);
        }
    }
}