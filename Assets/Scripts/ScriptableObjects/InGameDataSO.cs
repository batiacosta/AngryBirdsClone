using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "InGameDataSO", menuName = "GameSettingUpSO/InGameDataSO", order = 0)]
    public class InGameDataSO : ScriptableObject
    {
        
        [FormerlySerializedAs("birdSoLibrarySO")] [SerializeField] private CharacterSOLibrarySO characterSoLibrarySo;
        [SerializeField] private SelectionDataSO selectionDataSO;
        [SerializeField] private int hearts;

        public EventHandler<CharacterSO> OnCurrentBirdSOChanged;
        public EventHandler<List<CharacterSOLibrarySO.BirdData>> OnBirdQuantityChange;
        public EventHandler<int> OnEnemyQuantityChanged;
        public EventHandler<int> OnHeartsChanged;

        public CharacterSO CurrentCharacterSo
        {
            get => _currentCharacterSo;
        }

        private CharacterSO _currentCharacterSo;
        private List<CharacterSOLibrarySO.BirdData> _selectedBirdsData;
        private int _remainingEnemies;
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

        public void SetBirdQuantityInGame(CharacterSO birdSo, bool isAdditive)
        {
            int index = GetIndexFromSelectedBirds(birdSo);
            var libraryIndex = _selectedBirdsData[index].birdSo.index;
            if (isAdditive)
            {
                characterSoLibrarySo.BirdsData[libraryIndex].Quantity++;
            }
            else
            {
                if (_selectedBirdsData[index].Quantity > 0)
                {
                    characterSoLibrarySo.BirdsData[libraryIndex].Quantity--;
                }
            }
            OnBirdQuantityChange?.Invoke(this, _selectedBirdsData);
        }

        private int GetIndexFromSelectedBirds(CharacterSO birdSo)
        {
            for (int i = 0; i < _selectedBirdsData.Count; i++)
            {
                if (_selectedBirdsData[i].birdSo.type == birdSo.type)
                {
                    return i;
                }
            }

            return _selectedBirdsData.Count + 1;
        }

        public void DecreaseEnemies()
        {
            _remainingEnemies--;

            OnEnemyQuantityChanged?.Invoke(this, _remainingEnemies);
        }

        public void SetRemainingEnemies(int enemies)
        {
            _remainingEnemies = enemies;
        }
        

        public void SetHearts(int heartsValue)
        {
            if (heartsValue <= 0)
            {
                hearts = 0;
            }
            else
            {
                hearts = heartsValue;
            }
            OnHeartsChanged?.Invoke(this, hearts);
        }

        public int GetHearts()
        {
            return hearts;
        }

        public void InitializeInGameData()
        {
            _remainingEnemies = 0;
            _selectedBirdsData.Clear();
        }

        public void RefillBirds()
        {
            for (int i = 0; i < characterSoLibrarySo.BirdsData.Count; i++)
            {
                characterSoLibrarySo.BirdsData[i].Quantity += 2;
            }
            OnBirdQuantityChange?.Invoke(this, _selectedBirdsData);
        }
    }
}