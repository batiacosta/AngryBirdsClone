using UnityEngine;

namespace DefaultNamespace
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] int dotsNumber;
        [SerializeField] Transform dotsParent;
        [SerializeField] Transform dotPrefab;
        [SerializeField] float dotSpacing;
        [SerializeField] [Range (0.01f, 0.3f)] float dotMinScale;
        [SerializeField] [Range (0.3f, 1f)] float dotMaxScale;

        private Transform[] _dotsList;

        private Vector2 _pos;

        private float _timeStamp = 1000f;

        private bool _areHidden = true;


        void Start ()
        {
            Hide();
            PrepareDots();
        }

        void PrepareDots()
        {
            _dotsList = new Transform[dotsNumber];
            dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

            float scale = dotMaxScale;
            float scaleFactor = scale / dotsNumber;

            for (int i = 0; i < dotsNumber; i++) {
                _dotsList [i] = Instantiate (dotPrefab, null).transform;
                _dotsList [i].parent = dotsParent.transform;

                _dotsList [i].localScale = Vector3.one * scale;
                if (scale > dotMinScale)
                    scale -= scaleFactor;
            }
        }

        public void UpdateDotsPosition(Vector3 birdPosition, Vector2 direction)
        {
            if (_areHidden)
            {
                Show();
            }
            _timeStamp = dotSpacing;
            for (int i = 0; i < dotsNumber; i++) {
                float posX = (birdPosition.x + direction.x * _timeStamp);
                float posY = (birdPosition.y + direction.y * _timeStamp) - (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;
                _pos = new Vector2(posX, posY);
                _dotsList [i].position = _pos;
                _timeStamp += dotSpacing;
            }
        }

        public void Show()
        {
            _areHidden = false;
            dotsParent.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _areHidden = true;
            dotsParent.gameObject.SetActive(false);
        }
    }
}