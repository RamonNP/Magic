using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Magic.Player
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 2.0f;
        [SerializeField] private float _jumpForce = 4f;
        [SerializeField] private Transform _player;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Vector2 _moveTarget;
        [SerializeField] private AnimatorController _animatorController;
        [SerializeField] private DistanceFightingController _distanceFightingController;

        [SerializeField] private PlayerStatusController _playerStatusController;
        [SerializeField] private MagicCastController _magicController;
        public Action<bool> OnplayerMove;
        public bool isPlayeMoving;
        public Rigidbody2D rb;
        private bool _isCharging;
        [SerializeField] private bool _isAttacking;

        public Action<bool> OnplayerFlip;

        public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

        private void Start()
        {
            rb = _player.GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _playerStatusController.OnplayerFlying += Fly;
            _magicController.OnplayerChargind += PlayCharging;
        }


        private void OnDisable()
        {
            _playerStatusController.OnplayerFlying -= Fly;
        }
        private void PlayCharging(bool isCharging)
        {
            _isCharging = isCharging;
        }

        private void Fly(bool fly)
        {
            if(fly)
            {
                rb.simulated = false;
            } else
            {
                rb.velocity = new Vector2(0,0);
                rb.simulated = true;
            }
        }


        void Update()
        {
            //MovePlayer();
            MovePlayerChat();
            JumpPlayer();
            Flip();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //_distanceFightingController.ShootArrow(ArrowType.RegularArrow);
                _animatorController.ShootArrow();
            }            
            if (Input.GetKeyDown(KeyCode.H))
            {
                //_distanceFightingController.ShootArrow(ArrowType.RegularArrow);
                _animatorController.HighShot();
            }
        }

        private void Flip()
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _player.transform.localScale = new Vector3(-1, 1, 1);
                OnplayerFlip?.Invoke(true);
                //Debug.Log("FLIP TRUE");
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _player.transform.localScale = new Vector3(1, 1, 1);
                OnplayerFlip?.Invoke(false);
                //Debug.Log("FLIP False");
            }
        }

        IEnumerator FlyAnimationInitial()
        {

            for (float alpha = 50; alpha >= 0; alpha -= 1f)
            {
                _moveTarget = new Vector2(0 * _playerSpeed, 1 * _playerSpeed);
                _moveTarget *= Time.deltaTime;

                _player.transform.Translate(_moveTarget);
                yield return new WaitForSeconds(.01f);
            }

        }

        private void JumpPlayer()
        {
            if (Input.GetButtonDown("Jump") && _groundCheck.IsGround)
            {
                rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void MovePlayer()
        {
            float move = 0;
            if (!_isCharging)
            {
                move = Input.GetAxis("Horizontal");
                if(move != 0)
                {
                    if(!isPlayeMoving)
                    {
                        isPlayeMoving = true;
                        OnplayerMove?.Invoke(true);
                    }
                } else
                {
                    if (isPlayeMoving)
                    {
                        isPlayeMoving = false;
                        OnplayerMove?.Invoke(false);
                    }
                }
            }
            if (_playerStatusController.Isflying)
            {
                _moveTarget = new Vector2(move * _playerSpeed, Input.GetAxis("Vertical") * 2 *_playerSpeed);
            } else
            {
                _moveTarget = new Vector2(move * _playerSpeed, 0 *_playerSpeed);
            }

            _moveTarget *= Time.deltaTime;

            _player.transform.Translate(_moveTarget);
        }        
        private void MovePlayerChat()
        {
            if (!_isCharging && !_playerStatusController.Isflying )
            {
                if(!IsAttacking)
                {
                    float horizontalInput = Input.GetAxis("Horizontal");
                    OnplayerMove?.Invoke( (horizontalInput) != 0);

                    if (Mathf.Abs(horizontalInput) > 0)
                    {
                        transform.position += new Vector3(horizontalInput * _playerSpeed * Time.deltaTime, 0f, 0f);
                    }
                    else
                    {
                        isPlayeMoving = false;
                    }
                }
            }
            else if (_playerStatusController.Isflying)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                isPlayeMoving = true;
                OnplayerMove?.Invoke(true);
                _player.transform.position += new Vector3(horizontalInput * _playerSpeed * Time.deltaTime, verticalInput * _playerSpeed * Time.deltaTime, 0f);
            }

            // Aqui você pode adicionar outras condições para mover o jogador
        }

        public void MoveUpFly()
        {
            StartCoroutine(FlyAnimationInitial());
        }
    }
      
}