using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    [Header("Experience")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 10;

    [Header("XP UI")]
    public Slider xpSlider;
    public bool useXpSlider = true;

    [Header("Level UI")]
    public TextMeshProUGUI levelText;
    public bool useLevelText = true;

    [Header("Debug")]
    public bool enableDebugKeys = true;
    public int debugXpAmount = 5;
    public Key debugKey = Key.J;

    [Header("Game Over UI")]
    public TextMeshProUGUI gameOverLevelText;

    private void Start()
    {
        // Call the function that sets up the XP slider.
        SetupXpSlider();



        // Call the function that updates the XP UI.
        UpdateXpUI();


        // Call the function that updates the Level UI.
        UpdateLevelUI();


        // Print the player's starting level and XP to the Console.
        Debug.Log($"player starting level is {level}");

    }

    private void Update()
    {
        if (enableDebugKeys == true)
        {
            HandleDebugInput();
        }
    }

    private void HandleDebugInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        // If the debug XP key is pressed, add the debug XP amount.
        if (Keyboard.current[debugKey].wasPressedThisFrame)
        {
            // Call AddXP and give it debugXpAmount.
            AddXP(debugXpAmount);


            // Print the amount of XP added and the player's current XP.
            Debug.Log($"player got {debugXpAmount} xp, and now has {currentXP}");

        }
    }

    public void AddXP(int amount)
    {
        // Add amount to the player's current XP.
        currentXP += amount;


        // Check whether the player has enough XP to level up.
        // If they do, call the LevelUp function.
        if (currentXP >= xpToNextLevel)
            LevelUp();


        // Call the function that updates the XP UI.
        UpdateXpUI();

    }

    private void LevelUp()
    {
        // Remove the required XP from the player's current XP.
        currentXP -= xpToNextLevel;



        // Increase the player's level by 1.
        level++;


        // Print the player's new level to the Console.
        Debug.Log($"Player's level is now {level}");


        // Call the function that updates the Level UI.
        UpdateLevelUI();



        // Increase the XP required for the next level by 25.
        xpToNextLevel += 25;


        // Check whether the player still has enough XP to level up again.
        // If they do, call LevelUp again.
        // This is recursion: a function calling itself.
        // Careful...do this one last as it can crash Unity.
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }


        // Call the function that updates the XP UI.
        UpdateXpUI();

    }

    private void SetupXpSlider()
    {
        if (useXpSlider == false)
        {
            return;
        }

        if (xpSlider == null)
        {
            Debug.LogWarning("PlayerExperience: No xpSlider assigned in the Inspector.");
            return;
        }

        xpSlider.minValue = 0;

        // Set the slider's maximum value to the XP required for the next level.
        xpSlider.maxValue = xpToNextLevel;

        // Set the slider's current value to the player's current XP.
        xpSlider.value = currentXP;
    }

    private void UpdateXpUI()
    {
        if (useXpSlider == false)
        {
            return;
        }

        if (xpSlider == null)
        {
            return;
        }

        // Keep the slider's maximum value matched to the XP required for the next level.
        xpSlider.maxValue = xpToNextLevel;

        // Set the slider's current value to the player's current XP.
        xpSlider.value = currentXP;
    }

    private void UpdateLevelUI()
    {
        if (useLevelText == false)
        {
            return;
        }

        if (levelText == null)
        {
            return;
        }

        // Display the player's current level.
        levelText.text = level.ToString();

        // Check that gameOverLevelText is not null.
        // If it exists, display the level reached on the Game Over screen.
        if (gameOverLevelText != null)
        {
            gameOverLevelText.text = level.ToString();
        }

    }
}