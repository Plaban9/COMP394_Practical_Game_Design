using UnityEngine;

namespace Camera
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _followSpeed = 2f;
        [SerializeField] private float _depth = -10f;
        [SerializeField] private Vector3 _offset = Vector3.zero;

        private void Awake()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        void Update()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            var newPosition = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, _depth);
            transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);
        }
    }
}
