using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   private int _jumpScore;
   [SerializeField] private int jumpScoreForSpawnNewPlatforms = 5;
   public static Action OnEnoughScoreForCreatePlatforms;

   private int _score;
   private int _highScore;
   private int _fruits;
   
   [SerializeField] private PlayerController player;
   [SerializeField] private GameObject[] models;
   [SerializeField] private GameObject defaultChar;

   private void Awake()
   {
      if (Instance==null)
      {
         Instance = this;
      }
      
      SelectChar();
      
   }
   
   private void OnEnable()
   {
      PlayerController.OnPlayerDead += PlayerDie;
      PlayerController.OnScoreAdded += AddScore;
      Fruit.OnFruitCollected += CollectFruit;
   }

   private void OnDisable()
   {
      PlayerController.OnPlayerDead -= PlayerDie;
      PlayerController.OnScoreAdded -= AddScore;
      Fruit.OnFruitCollected -= CollectFruit;
   }

   private void Start()
   {
      if (PlayerPrefs.HasKey(TagManager.FRUITS_SCORE_PREFS))
      {
         _fruits = PlayerPrefs.GetInt(TagManager.FRUITS_SCORE_PREFS);
      }
      else
      {
         PlayerPrefs.SetInt(TagManager.FRUITS_SCORE_PREFS,_fruits);
      }

      if (!PlayerPrefs.HasKey(TagManager.HIGH_SCORE_PREFS))
      {
         PlayerPrefs.SetInt(TagManager.HIGH_SCORE_PREFS,_score);
      }

      UIController.Instance.SetFruits(_fruits);
      UIController.Instance.SetScore(_score);
      
   }
   
   private void SelectChar()
   {
      for (int i = 0; i < models.Length; i++)
      {
         if (models[i].name==PlayerPrefs.GetString(TagManager.SELECTED_CHAR_NAME))
         {
            GameObject clone = Instantiate(models[i], player.modelHolder.position, player.modelHolder.rotation);
            clone.transform.parent = player.modelHolder;
            //Destroy(clone.GetComponent<Rigidbody>());
           //defaultChar.SetActive(false);
         }
      }
   }

   private void PlayerDie()
   {
      print("Dead");
      
      MainMusic.Instance.PlayDeadSound();
      UIController.Instance.ShowGOPanel();
      UIController.Instance.SetFinalScore(_score);
      UIController.Instance.GameOver();

      if (_score>PlayerPrefs.GetInt(TagManager.HIGH_SCORE_PREFS))
      {
         PlayerPrefs.SetInt(TagManager.HIGH_SCORE_PREFS,_score);
         _highScore = _score;
      }
      else
      {
         _highScore = PlayerPrefs.GetInt(TagManager.HIGH_SCORE_PREFS);
      }
      
      UIController.Instance.SetFinalScore(_score);
      UIController.Instance.SetFinalHighScoreText(_highScore);
      
      PlayerPrefs.SetInt(TagManager.FRUITS_SCORE_PREFS,_fruits);
   }

   private void CollectFruit()
   {
      _fruits++;
      UIController.Instance.SetFruits(_fruits);
   }

   private void AddScore()
   {
      _jumpScore++;
      if (_jumpScore>=jumpScoreForSpawnNewPlatforms)
      {
         _jumpScore = 0;
         
         OnEnoughScoreForCreatePlatforms?.Invoke();
      }

      _score++;
      UIController.Instance.SetScore(_score);
   }
   
}
