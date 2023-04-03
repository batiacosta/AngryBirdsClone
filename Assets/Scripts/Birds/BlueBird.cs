using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Birds
{
    public class BlueBird: Bird
    {
        [SerializeField] private float speedFactor;
        [SerializeField] private CharacterSO subBlueBirdSo;
        public override void SetActivated()
        {
            var currentVelocity = GetComponent<Rigidbody2D>().velocity;
            var sub1 = Instantiate(subBlueBirdSo.prefab, transform.position, Quaternion.identity);
            sub1.GetComponent<SubBlue>().InitialSetUp(new Vector2(currentVelocity.x, currentVelocity.y+speedFactor));
            var sub2 = Instantiate(subBlueBirdSo.prefab, transform.position, Quaternion.identity);
            sub2.GetComponent<SubBlue>().InitialSetUp(currentVelocity);
            var sub3 = Instantiate(subBlueBirdSo.prefab, transform.position, Quaternion.identity);
            sub3.GetComponent<SubBlue>().InitialSetUp(new Vector2(currentVelocity.x, currentVelocity.y-speedFactor));
            Destroy(this.gameObject);
        }
    }
}