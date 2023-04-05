using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterSOLibrarySO", menuName = "CharacterSOLibrarySO", order = 0)]
    public class CharacterSOLibrarySO : ScriptableObject
    {
        public List<BirdData> BirdsData;
        [Serializable]
        public class BirdData
        {
            public CharacterSO birdSo;
            public int Quantity;    //  This value could be set by fetching from user's data, coins, etc.
        }
    }
}