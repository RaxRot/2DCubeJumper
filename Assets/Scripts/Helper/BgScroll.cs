using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour
{
    private Material _bgQuadMaterial;
    [SerializeField] private float scrollSpeed = 0.1f;
    [SerializeField] private float scrollTime = 3f;
    private float _scrollTimeCounter;

    [SerializeField] private bool isMenuLogo;
    private void Awake()
    {
        _bgQuadMaterial = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        PlayerController.OnScrollStart += StartScroll;
    }

    private void OnDisable()
    {
        PlayerController.OnScrollStart -= StartScroll;
    }

    private void Update()
    {
        if (isMenuLogo)
        {
            Vector2 offset = new Vector2(scrollSpeed * Time.deltaTime, 0);
            _bgQuadMaterial.mainTextureOffset += offset;
        }
    }


    private void StartScroll()
    {
        StartCoroutine(_StartScrollCo());
    }

    private IEnumerator _StartScrollCo()
    {
        while (_scrollTimeCounter<scrollTime)
        {
            _scrollTimeCounter += Time.deltaTime;
            
            Vector2 offset = new Vector2(scrollSpeed * Time.deltaTime, 0);
            _bgQuadMaterial.mainTextureOffset += offset;
        }

        _scrollTimeCounter = 0;
        yield return null;
    }
}
