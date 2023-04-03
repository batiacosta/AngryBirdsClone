using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


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
