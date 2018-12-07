using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private float healthMultiplier = 1.3f;

    [SerializeField]
    private float movementSpeedMultiplier = 1.3f;

    [SerializeField]
    private float fireRateInreaser = 2f;

    [SerializeField]
    private int upgradeCost = 100;

    [SerializeField]
    private int fireupgradeCost = 150;

    [SerializeField]
    private int weaponUpgradeCostAK47 = 250;

    [SerializeField]
    private int weaponUpgradeCostMG = 350;

    private PlayerStats stats;

    public GameObject pistol;
    public GameObject AK47;
    public GameObject MachineGun;

    void OnEnable()
    {
        stats = PlayerStats.instance;
        UpdateValues();
    }

    void UpdateValues()
    {
        healthText.text = "HEALTH: " + stats.maxHealth.ToString();
    }

    public void UpgradeHealth()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);

        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Heartbeat");

        UpdateValues();
    }

    public void UpgradeFireRate()
    {
        if (GameMaster.Money < fireupgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        Weapon.fireRate += fireRateInreaser;

        GameMaster.Money -= fireupgradeCost;
        AudioManager.instance.PlaySound("BulletFalling");

        UpdateValues();
    }

    public void UpgradeWeaponAK47()
    {
        if (GameMaster.Money < weaponUpgradeCostAK47)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        
        pistol.SetActive(false);
        AK47.SetActive(true);
        //MachineGun.SetActive(false);

        GameMaster.Money -= weaponUpgradeCostAK47;
        AudioManager.instance.PlaySound("AK47Draw");

        UpdateValues();
    }

    //public void UpgradeWeaponMG()
    //{
    //    if (GameMaster.Money < weaponUpgradeCostMG)
    //    {
    //        AudioManager.instance.PlaySound("NoMoney");
    //        return;
    //    }

    //    //pistol.SetActive(false);
    //    //AK47.SetActive(false);
    //    MachineGun.SetActive(true);

    //    GameMaster.Money -= weaponUpgradeCostMG;
    //    AudioManager.instance.PlaySound("Money");

    //    UpdateValues();
    //}

}
