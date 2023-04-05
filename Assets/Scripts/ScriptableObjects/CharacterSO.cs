using System;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class CharacterSO : ScriptableObject
    {
        public int index;
        public Sprite characterImage;
        public Sprite characterUIReference;
        public Transform prefab;
        public String type;
        public InGameDataSO InGameDataSo;
    }
}
