using Unity.VisualScripting;
using UnityEngine;

namespace Birds
{
    public class RedBird : Bird
    {
        private const float Forcefactor = 10f;
        public override void SetHit()
        {
            //  Play animations when bird hits something
            //  Play animations when bird hits something
        }

        public override void SetActivated()
        {
            BirdRigidbody2D.AddForce(transform.right * Forcefactor, ForceMode2D.Impulse);
            State = BirdState.Hit;
        }

        public override void SetReleased()
        {
            //  Play animations when released
            //  Play animations when released
        }

        public override void SetShooting()
        {
            //  Play animations when bird is shot
            //  Play animations when bird is shot
        }

        public override void SetPressed()
        {
            //  Play animations when pressed
            //  Play animations when pressed
        }

        public override void SetIdle()
        {
            //  Play animations for idle
            //  Play animations for idle
        }
    }
}