using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fruitsPrefab;
    private int _fruitIndex;
    [SerializeField] private Transform pointToSpawn;
    [SerializeField] private int percentChanceToSpawn = 40;

    private void Start()
    {
       Spawn();
    }

    public void Spawn()
    {
        if (Random.Range(0,100)<=percentChanceToSpawn)
        {
            _fruitIndex = Random.Range(0, fruitsPrefab.Length);
            Instantiate(fruitsPrefab[_fruitIndex], pointToSpawn.position, Quaternion.identity);
        }
    }
}
