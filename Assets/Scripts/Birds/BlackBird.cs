
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Birds
{
    public class BlackBird: Bird
    {
        [SerializeField] private float explosionRadious;
        [SerializeField] private float forceFactor;
        public override void SetActivated()
        {
            ShowVFX();
            var explosionCircleObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadious);
            foreach (Collider2D afectedObject in explosionCircleObjects)
            {
                var afectedObjectRigidbody2D = afectedObject.GetComponent<Rigidbody2D>();
                if (afectedObjectRigidbody2D != null)
                {
                    Vector2 distance = afectedObject.transform.position - transform.position;
                    if (distance.magnitude > 0)
                    {
                        float explosionForceMagnitud = forceFactor / distance.magnitude;
                        afectedObjectRigidbody2D.AddForce(distance.normalized * explosionForceMagnitud);
                    }
                }
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position , explosionRadious);
        }
    }
}