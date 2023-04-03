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
        
        [FormerlySerializedAs("birdSoLibrarySO")] [SerializeField] private CharacterSOLibrarySO characterSoLibrarySo;
        [SerializeField] private SelectionDataSO selectionDataSO;
        
        public EventHandler<CharacterSO> OnCurrentBirdSOChanged;
        public EventHandler<List<CharacterSOLibrarySO.BirdData>> OnBirdQuantityChange;
        public EventHandler<int> OnEnemyQuantityChanged;

        public CharacterSO CurrentCharacterSo
        {
            get => _currentCharacterSo;
        }

        private CharacterSO _currentCharacterSo;
        private List<CharacterSOLibrarySO.BirdData> _selectedBirdsData = new List<CharacterSOLibrarySO.BirdData>();
        private int _remainingEnemies = 0;
        public void SetNewBird(CharacterSO birdSo)
        {
            _currentCharacterSo = birdSo;
            OnCurrentBirdSOChanged?.Invoke(this, _currentCharacterSo);
        }
        
        public List<CharacterSOLibrarySO.BirdData> GetBirdSOList()
        {
            var indexes = selectionDataSO.GetIndexesList();
            foreach (var index in indexes)
            {
                _selectedBirdsData.Add(characterSoLibrarySo.BirdsData[index]);
            }

            SetNewBird(_selectedBirdsData[0].birdSo);

            return _selectedBirdsData;
        }

        public void SetBirdQuantityInGame(int index, bool isAdditive)
        {
            var libraryIndex = _selectedBirdsData[index].birdSo.index;
            if (isAdditive)
            {
                _selectedBirdsData[index].Quantity++;
                characterSoLibrarySo.BirdsData[libraryIndex].Quantity++;
            }
            else
            {
                _selectedBirdsData[index].Quantity--;
                characterSoLibrarySo.BirdsData[libraryIndex].Quantity--;
            }
            OnBirdQuantityChange?.Invoke(this, _selectedBirdsData);
        }

        public void DecreaseEnemies()
        {
            _remainingEnemies--;
            OnEnemyQuantityChanged?.Invoke(this, _remainingEnemies);
        }

        public void IncreaseEnemies()
        {
            _remainingEnemies++;
            OnEnemyQuantityChanged?.Invoke(this, _remainingEnemies);
        }
    }
}