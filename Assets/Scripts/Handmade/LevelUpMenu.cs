using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject levelMenu;
    public int levelsToProcess;
    public PlayerMovement playerWeapons;
    private int[] weaponSelection = { 0, 0, 0 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerWeapons = GameObject.Find("Player").GetComponent<PlayerMovement>();
        levelsToProcess = 0;
        levelMenu.SetActive(false);
        bool noDefence = true;
        // check if the player has any weapons
        // skip legs
        for (int i = 1; i < playerWeapons.hasWeapon.Length; i++)
        {
            if (playerWeapons.hasWeapon[i])
                noDefence = false;
        }
        // if the player doesnt have any weapons enabled, give them a weapon to choose from
        if (noDefence)
            EnableMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableMenu()
    {
        levelMenu.SetActive(true);
        GenerateWeaponsToChoose();
    }
    public void DisableMenu()
    {
        levelMenu.SetActive(false);
        levelsToProcess--;
        if (levelsToProcess > 0)
            EnableMenu();
        else if (levelsToProcess < 0)
            levelsToProcess = 0;
    }

    public void GenerateWeaponsToChoose()
    {
        for (int i = 0; i < 3; i++)
        {
            int temp = Random.Range(1, playerWeapons.hasWeapon.Length);
            if (playerWeapons.hasWeapon[temp] == false)
            {
                weaponSelection[i] = temp;
            }
            else
            {
                i--;
                return;
            }
        }
    }
    public void EquipWeapon(int buttonNumber)
    {
        playerWeapons.AddWeapon(weaponSelection[buttonNumber]);
    }
    
}
