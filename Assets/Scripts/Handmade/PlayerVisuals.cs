using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void WeaponCheck()
    {
        playerMovement.CheckAttacksForIdle();
    }
}
