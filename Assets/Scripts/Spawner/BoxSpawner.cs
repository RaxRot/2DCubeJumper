using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] boxPrefabs;
   private int _boxIndexToSpawn;
   [SerializeField] private int startXToSpawn = 0;
   [SerializeField] private int startYToSpawn = -2;
   private Vector2 _spawnPosition;

   [SerializeField] private float minXStep = 2f;
   [SerializeField] private float maxXStep = 5f;

   [SerializeField] private float minYStep = -0.8f;
   [SerializeField] private float maxYStep = 0.8f;
   private float _lastX;
   private float _lastY;

   [SerializeField] private int boxForSpawn = 15;

   private void OnEnable()
   {
      GameManager.OnEnoughScoreForCreatePlatforms += SpawnBlocks;
   }

   private void OnDisable()
   {
      GameManager.OnEnoughScoreForCreatePlatforms -= SpawnBlocks;
   }

   private void Start()
   {
      _spawnPosition = new Vector2(startXToSpawn, startYToSpawn);
      
      SpawnBlocks();
   }

   private void SpawnBlocks()
   {
      for (int i = 0; i < boxForSpawn; i++)
      {
         _boxIndexToSpawn = Random.Range(0, boxPrefabs.Length);
         Instantiate(boxPrefabs[_boxIndexToSpawn], _spawnPosition, Quaternion.identity);

         _lastX = _spawnPosition.x;
         _lastY = _spawnPosition.y;

         _lastX += Random.Range(minXStep, maxXStep);
         _lastY += Random.Range(minYStep, maxYStep);

         _spawnPosition = new Vector2(_lastX, _lastY);
      }
   }
}
