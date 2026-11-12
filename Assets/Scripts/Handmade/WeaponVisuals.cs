using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{
    public int weaponNumber;
    public SpriteRenderer weaponSprite;
    public Animator weaponAnimator;
    public bool currentIdle;

    void Start()
    {
        currentIdle = true;
        weaponSprite = GetComponent<SpriteRenderer>();
        weaponAnimator = GetComponent<Animator>();
    }
    public void EndAttack()
    {
        currentIdle = true;
    }
}
