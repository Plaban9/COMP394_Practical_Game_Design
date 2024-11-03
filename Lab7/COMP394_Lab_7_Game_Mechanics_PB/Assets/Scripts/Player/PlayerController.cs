using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Basic Movement")]
        [SerializeField] private float _speed;
        [SerializeField] private int _horizontal = 0;
        [SerializeField] private int _vertical = 0;
        [SerializeField] private Vector3 _movementVector;
        [SerializeField] private bool _constraintZ;

        [Header("JUMP")]
        [SerializeField] private bool _isGrounded;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _raycastDistance = 1.05f;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _additionalFallingForce = 2f;
        [SerializeField] private int _jumpIndex = 0;
        [SerializeField] private int _maxJumps = 0;

        private void Awake()
        {
            _jumpIndex = 0;

            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleKeyStrokes();
            Move();
            Jump();
        }

        #region Movment
        private void HandleKeyStrokes()
        {
            HandlePressedDown();
            HandlePressedUp();

            _movementVector = new Vector3(_horizontal, 0f, _vertical);
        }

        private void HandlePressedDown()
        {
            if (!_constraintZ)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    _vertical = Mathf.Clamp(_vertical + 1, 0, 1);
                }

                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    _vertical = Mathf.Clamp(_vertical - 1, -1, 0);
                }
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _horizontal = Mathf.Clamp(_horizontal - 1, -1, 0);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _horizontal = Mathf.Clamp(_horizontal + 1, 0, 1);
            }
        }

        private void HandlePressedUp()
        {
            if (!_constraintZ)
            {
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    _vertical = 0;
                }
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                _horizontal = 0;
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _movementVector, _speed * Time.deltaTime);
        }
        #endregion

        #region Jump
        private void Jump()
        {
            CheckGroundStatus();

            if (_isGrounded || _jumpIndex < _maxJumps)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _jumpIndex++; 
                    _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                }
            }
        }

        private void CheckGroundStatus()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, out _, _raycastDistance, _groundLayer);

            if (_isGrounded && _rigidbody.velocity.y < 0f)
            {
                _jumpIndex = 0;
            }
        }
        #endregion 

        private void D(string message)
        {
            Debug.Log($"<<PlayerController>> {message}");
        }
    }
}