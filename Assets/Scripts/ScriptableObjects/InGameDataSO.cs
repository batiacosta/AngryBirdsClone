using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
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
            Debug.Log($"Quedan solo {_remainingEnemies}");
            OnEnemyQuantityChanged?.Invoke(this, _remainingEnemies);
        }

        public int GetEnemies()
        {
            return _remainingEnemies;
        }

        public void SetRemainingEnemies(int enemies)
        {
            _remainingEnemies = enemies;
        }
        

        public void SetHearts(int heartsValue)
        {
            hearts = heartsValue;
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
    }
}