
using UnityEngine;

namespace Birds
{
    public class SubBlue: Bird
    {
        public void InitialSetUp(Vector2 velocity)
        {
            State = BirdState.Released;
            BirdRigidbody2D.velocity = velocity;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var birdComponent = other.gameObject.GetComponent<Bird>();
            if (birdComponent != null)
            {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }
}