using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectionDataSO", menuName = "GameSettingUpSO/GameSelectionDataSO")]
    public class SelectionDataSO : ScriptableObject
    {
        private List<int> _selectedBirdIndexes;
        public EventHandler<bool> OnSelectionChanged;

        private bool _isSelectionFilled = false;
        private const int MaxAllowedBirds = 2;
        public void AddSelectedBird(int index)
        {
            if (_selectedBirdIndexes.Count < MaxAllowedBirds)
            {
                _selectedBirdIndexes.Add(index);
                OnSelectionChanged?.Invoke(this, IsFilled());
            }
            OnSelectionChanged?.Invoke(this, IsFilled());
        }

        public void RemoveSelectedBird(int index)
        {
            if (_selectedBirdIndexes.Contains(index))
            {
                _selectedBirdIndexes.Remove(index);
            }
            OnSelectionChanged?.Invoke(this, IsFilled());
        }

        public int GetIndex(int askingIndex)
        { 
            for (int i = 0; i < _selectedBirdIndexes.Count; i++)
            {
                if (askingIndex == _selectedBirdIndexes[i])
                {
                    return i;
                }
            }
            
            return MaxAllowedBirds + 1;
        }

        public bool IsFilled()
        {
            _isSelectionFilled = _selectedBirdIndexes.Count == MaxAllowedBirds;
            return _isSelectionFilled;
        }
    }
}