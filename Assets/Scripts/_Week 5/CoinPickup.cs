using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinPickup : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Coin Settings")]
    public int moneyAmount = 1;

    [Header("Debug")]
    public bool showDebugLogs = true;

    private void Awake()
    {
        // TODO: Get the Collider2D component from this GameObject.
        Collider2D col = GetComponent<Collider2D>();



        // TODO: Check that col is not null.
        // If it exists, set the Collider2D to be a trigger.
        if (col == null)
        {
            return;
        }
        else
        {
            col.isTrigger = true;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Check whether the object that entered the trigger
        // has the Player tag.
        // If it does not, stop this function.
        if (other.tag != "Player")
        {
            return;
        }


        // TODO: Get the PlayerMoney component from the object
        // that entered the trigger.
        PlayerMoney money = other.GetComponent<PlayerMoney>();



        // TODO: If money is null, try to find the
        // PlayerMoney component in the scene.
        if (money == null)
        {
            money = GameObject.FindAnyObjectByType<PlayerMoney>();
            if (money == null) { return; }
        }


        // TODO: Check that money is not null.
        // If it exists:
        // - Add moneyAmount to the player's money.
        // - Print a debug message if debug logs are enabled.
        if (money != null)
        {
            moneyAmount += money.currentMoney;
            if (showDebugLogs)
            {
                Debug.Log("added " +  moneyAmount + " to player money");
            }
        }





        // Remove the coin after it has been collected.
        Destroy(gameObject);
    }
}