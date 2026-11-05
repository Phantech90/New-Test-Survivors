using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Spawner Settings")]
    public GameObject[] enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 8f;

    [Header("Animation")]
    public Animator animator;
    public bool playIdleAnimationOnStart = true;
    public string idleAnimationName = "Idle";

    [Header("Spawn Feedback")]
    public GameObject spawnParticlePrefab;
    public float spawnParticleLifetime = 2f;
    public AudioClip spawnSound;
    public AudioSource audioSource;

    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Debug")]
    public bool spawningEnabled = true;
    public bool enableDebugKeys = true;
    public bool showDebugLogs = true;

    public Key toggleSpawnKey = Key.P;
    public Key spawnOnceKey = Key.O;
    public Key destroyEnemiesKey = Key.I;

    [Header("Debug View")]
    public float spawnTimer = 0f;

    // This collection stores all enemies created by this spawner.
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        // TODO: Check if audioSource is null.
        // If it is, get the AudioSource component from this GameObject.
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }



        // TODO: Check if animator is null.
        // If it is, get the Animator component from this GameObject.
        if (animator  == null)
        {
            animator = GetComponent<Animator>();
        }



        // TODO: Check if gameManager is null.
        // If it is, find the GameManager in the scene.
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

    }

    private void Start()
    {
        // TODO: Call the function that plays the idle animation.
        PlayIdleAnimation();


        // TODO: If debug logs are enabled, print that the EnemySpawner has started.
        if (showDebugLogs)
        {
            Debug.Log("Enemy Spawner Started");
        }


        // TODO: Check that gameManager is not null.
        // If it exists, call AddSpawner.
        if (gameManager != null)
            gameManager.AddSpawner();

    }

    private void Update()
    {
        // TODO: Call the function that handles the debug input.
        HandleDebugInput();


        // TODO: Check whether spawning is disabled.
        // If it is, stop this function.
        if (!spawningEnabled)
            return;


        // TODO: Check whether enemyPrefab is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if (enemyPrefab == null)
        {
            if (showDebugLogs)
            {
                Debug.Log("No enemy prefab");
                return;
            }
        }



        // TODO: Add Time.deltaTime to spawnTimer.
        spawnTimer += Time.deltaTime;


        // TODO: Check whether spawnTimer is greater than or equal to spawnInterval.
        // If it is:
        // - Reset spawnTimer to 0.
        // - Call SpawnEnemy.
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }


    }

    private void PlayIdleAnimation()
    {
        // TODO: Check whether playIdleAnimationOnStart is false.
        // If it is, stop this function.
        if (!playIdleAnimationOnStart)
            return;


        // TODO: Check whether animator is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if (animator == null)
        {
            if (showDebugLogs)
                Debug.Log("No animator found");
            return;
        }



        // TODO: Check whether idleAnimationName is empty.
        // Hint: Use string.IsNullOrWhiteSpace().
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if (idleAnimationName == null)
        {
            if (showDebugLogs)
                Debug.Log("Idle animation name missing");
            return;
        }
            


        // TODO: Play the animation stored in idleAnimationName.
        animator.Play(idleAnimationName);


        // TODO: If debug logs are enabled, print the animation being played.
        if (showDebugLogs)
            Debug.Log("Animation is playing");

    }

    private void HandleDebugInput()
    {
        // TODO: Check whether debug keys are disabled.
        // If they are, stop this function.
        if (!enableDebugKeys)
            return;


        // TODO: Check whether Keyboard.current is null.
        // If it is, stop this function.
        if (Keyboard.current == null)
            return;


        // TODO: Check whether toggleSpawnKey was pressed.
        // If it was:
        // - Toggle spawningEnabled between true and false.
        // - Print the new value if debug logs are enabled.
        if (Keyboard.current[toggleSpawnKey].isPressed)
        {
            spawningEnabled = !spawningEnabled;
            if (showDebugLogs)
                Debug.Log($"Spawning set to {spawningEnabled}");
        }


        // TODO: Check whether spawnOnceKey was pressed.
        // If it was:
        // - Call SpawnEnemy.
        // - Print a debug message if debug logs are enabled.
        if (Keyboard.current[spawnOnceKey].isPressed)
        {
            SpawnEnemy();
            if (showDebugLogs)
                Debug.Log("Spawned Enemy");
        }


        // TODO: Check whether destroyEnemiesKey was pressed.
        // If it was, call DestroySpawnedEnemies.
        if (Keyboard.current[destroyEnemiesKey].isPressed)
        {
            DestroySpawnedEnemies();
        }

    }

    public void StopSpawning()
    {
        // TODO: Set spawningEnabled to false.
        spawningEnabled = false;


        // TODO: If debug logs are enabled, print that spawning has stopped.
        if (showDebugLogs)
            Debug.Log("Spawning has stopped");

    }

    public void SpawnerDestroyed()
    {
        // TODO: Check that gameManager is not null.
        // If it exists, call RemoveSpawner.
        if (gameManager != null)
            gameManager.RemoveSpawner();

    }

    private void SpawnEnemy()
    {
        // TODO: Check whether enemyPrefab is null.
        // If it is:
        // - Print a warning if debug logs are enabled.
        // - Stop this function.
        if (enemyPrefab == null)
        {
            if (showDebugLogs)
                Debug.Log("no prefab found");
            return;
        }


        // Generate a random position inside the spawn radius.
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

        // TODO: Calculate the final spawn position using
        // this GameObject's position and randomOffset.
        Vector3 spawnPosition = randomOffset + new Vector2(transform.position.x, transform.position.y);



        // TODO: Instantiate enemyPrefab at spawnPosition.
        // Store the newly created GameObject in enemyObject.
        int temp = Random.Range(0, enemyPrefab.Length);
        GameObject enemyObject = Instantiate(enemyPrefab[temp], spawnPosition, Quaternion.identity);



        // TODO: Add enemyObject to the spawnedEnemies collection.
        spawnedEnemies.Add(enemyObject);


        // TODO: Call PlaySpawnFeedback and give it spawnPosition.
        PlaySpawnFeedback(spawnPosition);


        // TODO: If debug logs are enabled, print where the enemy spawned.
        if (showDebugLogs)
            Debug.Log($"Enemy spawned at {spawnPosition}");

    }

    public void DestroySpawnedEnemies()
    {
        // Loop through every GameObject in the spawnedEnemies collection.
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            // TODO: Check that the enemy at position i is not null.
            if (spawnedEnemies[i] != null)
            {
                Destroy(spawnedEnemies[i]);
            }
            // TODO: Destroy the enemy at position i.
        }

        // TODO: Clear the spawnedEnemies collection.
        spawnedEnemies.Clear();


        // TODO: If debug logs are enabled, print that all spawned enemies were destroyed.
        if (showDebugLogs)
            Debug.Log($"All enemies destroyed");

    }

    private void PlaySpawnFeedback(Vector3 spawnPosition)
    {
        // TODO: Check that spawnParticlePrefab is not null.
        if (spawnParticlePrefab != null)
        {
            // TODO: Instantiate spawnParticlePrefab at spawnPosition.
            // Store the newly created GameObject in particleObject.
            GameObject particleObject = Instantiate(spawnParticlePrefab, spawnPosition, Quaternion.identity);



            // TODO: Destroy particleObject after spawnParticleLifetime seconds.
            Destroy(particleObject, spawnParticleLifetime);

        }

        // TODO: Check that spawnSound and audioSource are not null.
        // If they exist, play spawnSound using the AudioSource.
        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }



        // TODO: If debug logs are enabled, print that the spawn feedback played.
        if (showDebugLogs)
            Debug.Log("Spawn feedback played");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}