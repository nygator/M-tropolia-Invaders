using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public sealed class GameManager : MonoBehaviour
{
    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    private AudioSource audioSource;
    public GameObject soundPlayer;

    public AudioClip wonGame;

    //public GameObject gameOverUI;
    public Text scoreText;
    public Text livesText;
    public Text levelText;
    public int levelUpScore = 1000;

    public int score { get; private set; }
    public int lives { get; private set; }
    public int level { get; private set; }
    

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();
    }

    private void Start()
    {
        player.killed += OnPlayerKilled;
        mysteryShip.killed += OnMysteryShipKilled;
        invaders.killed += OnInvaderKilled;
        audioSource = soundPlayer.GetComponent<AudioSource>();

        NewGame();
    }

    private void Update()
    {

        
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
    }

    private void NewGame()
    {
        //gameOverUI.SetActive(false);

        SetScore(0);
        SetLevel(1);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++) {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

   // private void GameOver()
   // {
    //    gameOverUI.SetActive(true);
    //    invaders.gameObject.SetActive(false);
   // }

    private void SetLevel(int level)
    {
        this.level = level;
        levelText.text = "Level " + level.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = lives.ToString();
    }

    private void OnPlayerKilled()
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0) {
            Invoke(nameof(NewRound), 1.5f);
        } else {
            
            Invoke(nameof(GameOver), 2.0f);
        }
    }

    private void GameOver()
    {
        ScenesManager.Instance.LoadNextScene();
    }

    private void OnInvaderKilled(Invader invader)
    {
        SetScore(score + invader.score);

        if (invaders.AmountKilled == invaders.TotalAmount) {
            // SOITA MUSAA

            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = this.wonGame;
            audioSource.Play();

            SetScore(score + levelUpScore);

            this.level++;
            SetLevel(this.level);
            Invoke(nameof(NewRound), 3.1f);
            //NewRound();
        }
    }

    private void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        SetScore(score + mysteryShip.score);
    }

}
