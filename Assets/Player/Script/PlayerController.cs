using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Magic.Player
{

    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private GameObject TESTE;
        [SerializeField] private float _playerSpeed = 2.0f;
        [SerializeField] private float _jumpForce = 4f;
        [SerializeField] private Transform _player;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Vector2 _moveTarget;

        [SerializeField] private PlayerStatusController _playerStatusController;
        public Rigidbody2D rb;

        public Action<bool> OnplayerFlip;

        private void OnEnable()
        {
            _playerStatusController.OnplayerFlying += Fly;
        }
        private void OnDisable()
        {
            _playerStatusController.OnplayerFlying -= Fly;
        }

        private void Fly(bool fly)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if(fly)
            {
                rb.simulated = false;
            } else
            {
                rb.velocity = new Vector2(0,0);
                rb.simulated = true;
            }
        }

        private void Start()
        {
            rb = _player.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            MovePlayer();
            JumpPlayer();
            Flip();
        }

        private void Flip()
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                OnplayerFlip?.Invoke(true);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
                OnplayerFlip?.Invoke(false);
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
            if(_playerStatusController.Isflying)
            {
                _moveTarget = new Vector2(Input.GetAxis("Horizontal") * _playerSpeed, Input.GetAxis("Vertical") * 2 *_playerSpeed);
            } else
            {
                _moveTarget = new Vector2(Input.GetAxis("Horizontal") * _playerSpeed, 0 *_playerSpeed);
            }

            _moveTarget *= Time.deltaTime;

            _player.transform.Translate(_moveTarget);
        }
        public void MoveUpFly()
        {
            StartCoroutine(FlyAnimationInitial());
        }
    }
      
}