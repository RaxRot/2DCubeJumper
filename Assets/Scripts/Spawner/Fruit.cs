using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject fruitImpactFx;
    public enum  FruitType
    {
        Apple,
        Banana,
        Kiwi,
        Melon,
        Orange
    }
    public FruitType TypeOfFruit;

    public static Action OnFruitCollected;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.Play(TypeOfFruit+TagManager.FRUIT_IDLE_ANIM_NAME);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            MainMusic.Instance.PlayFruitSound();
            
            OnFruitCollected?.Invoke();

            Instantiate(fruitImpactFx, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
