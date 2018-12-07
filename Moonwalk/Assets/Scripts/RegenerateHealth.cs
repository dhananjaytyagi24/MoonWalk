using UnityEngine;

public class RegenerateHealth : MonoBehaviour {

    private PlayerStats stats;
    [SerializeField]
    private StatusIndicator statusIndicator;

    public string heartbeat = "Heartbeat";
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioManager.PlaySound(heartbeat);
            Destroy(gameObject);
            RegenHealth();
        }
    }

    void Start()
    {
        stats = PlayerStats.instance;

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }

    void RegenHealth()
    {
        stats.curHealth += stats.maxHealth - stats.curHealth;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
