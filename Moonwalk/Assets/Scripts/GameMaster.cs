using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;
    public static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    [SerializeField]
    private int startingMoney;
    public static int Money;

    public int startingOxygen = 100;
    public static int remainingOxygen = 0;
    public static float startTime;
    private bool isDead = false;

    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public GameObject spawnPrefab;
    public float startingFireRate = 5f;
    public string respawnCountdownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";

    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    //cache
    private AudioManager audioManager;

    public GameObject[] powerUps;
    public Transform[] powerUpPoints;
    public float spawnRate = 0;
    public float timeToRegenerate = 0;

    public static int highScore;

    void Start()
    {
        startTime = Time.time;
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced in GameMaster");
        }

        _remainingLives = maxLives;
        Money = startingMoney;
        remainingOxygen = startingOxygen;
        highScore = PlayerPrefs.GetInt("HighScore");
        Weapon.fireRate = startingFireRate;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleUpgradeMenu();
        }

        if (Time.time > timeToRegenerate)
        {
            timeToRegenerate = Time.time + 1 / spawnRate;
            spawnPowerUP();
        }

        float t = Time.time - startTime + 1;
        remainingOxygen = (int)(t % 60);

        if (startingOxygen - remainingOxygen <= 0)
        {
            remainingOxygen = 0;
        }

        if (isDead)
        {
            remainingOxygen = startingOxygen;
            //startTime = Time.time;
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);
        if (highScore < Money)
            PlayerPrefs.SetInt("HighScore", Money);

        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        startTime = Time.time;
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.isDead = true;
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        // Let's play some sound
        audioManager.PlaySound(_enemy.deathSoundName);

        // Gain some money
        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

        // Add particles
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);

        // Go camerashake
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

    void spawnPowerUP()
    {
        Transform _sp = powerUpPoints[Random.Range(0, powerUpPoints.Length)];
        GameObject go = powerUps[Random.Range(0, powerUpPoints.Length)];
        Instantiate(go, _sp.position, _sp.rotation);
    }

}
