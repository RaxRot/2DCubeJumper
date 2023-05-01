using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForceUp = 25f;
    [SerializeField] private float jumpForceForward = 20;
    
    public float maxJumpTime = 2f;
    
    private Rigidbody2D rb;
    private float jumpTime;
    private bool isJumping;
    private float jumpPower;

    private Camera _camera;
    public float cameraLerpSpeed = 0.5f;
    private bool _isOnPlatform;
    [SerializeField]private float cameraOffsetX = 5f;
    [SerializeField] private float cameraOffsetY = 1f;

    private Vector3 _deadPosition;
    [SerializeField] private float deadPositionOffset = 5f;
    private bool _isPlayerAlive;
    
    private Animator _characterAnim;

    public static Action OnPlayerDead;
    public static Action OnScoreAdded;
    public static Action OnScrollStart;

    private bool _shouldIncreaseScore;
    public Transform modelHolder;
    
    private void Awake()
    {
        _camera=Camera.main;
    }

    void Start()
    {
        _characterAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        _isPlayerAlive = true;
    }

    void Update()
    {
        ControlPlayer();
        
        ShowUIJumpPower();
        
        CheckForDead();
        
        AnimatePlayer();
        
    }

    private void ControlPlayer()
    {
        if (_isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isJumping = true;
                jumpTime = 0f;
            }
            else if (Input.GetMouseButtonUp(0) && isJumping)
            {
                isJumping = false;
                rb.AddForce(new Vector2(jumpPower * jumpForceForward, jumpPower * jumpForceUp), ForceMode2D.Impulse);
                jumpPower = 0f;
                
                MainMusic.Instance.PlayJumpSound();
            }

            if (isJumping)
            {
                jumpTime += Time.deltaTime;
                jumpPower = Mathf.Clamp01(jumpTime / maxJumpTime);
            }
        }
    }

    private void CheckForDead()
    {
        if (transform.position.y<_deadPosition.y - deadPositionOffset)
        {
            if (_isPlayerAlive)
            {
                OnPlayerDead?.Invoke();
            }
            _isPlayerAlive = false;
        }
    }

    private void LateUpdate()
    {
        if (_isOnPlatform)
        {
            Vector3 targetPosition = new Vector3(transform.position.x + cameraOffsetX, transform.position.y+cameraOffsetY,
                _camera.transform.position.z);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition,
                cameraLerpSpeed * Time.deltaTime);
            
        }
    }

    private void ShowUIJumpPower()
    {
        UIController.Instance.jumpPowerSlider.value = jumpPower;
    }

    private void AnimatePlayer()
    {
        _characterAnim.SetBool(TagManager.PLAYER_ON_PLATFORM_ANIM_PARAMETR,_isOnPlatform);
        _characterAnim.SetFloat(TagManager.PLAYER_VELOCITY_Y_ANIM_PARAMETR,rb.velocity.y);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLATFORM_TAG))
        {
            if (_shouldIncreaseScore)
            {
                MainMusic.Instance.PlayJumpSound();
                
                OnScrollStart?.Invoke();
                OnScoreAdded?.Invoke();
            }
            _shouldIncreaseScore = true;

            _isOnPlatform = true;
            rb.velocity = Vector2.zero;
            transform.parent = collision.transform;
            _deadPosition = collision.gameObject.transform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLATFORM_TAG))
        {
            _isOnPlatform = false;
            transform.parent = null;
        }
    }
}
