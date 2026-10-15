using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{
    public int weaponNumber;
    public SpriteRenderer weaponSprite;
    public Animator weaponAnimator;

    void Start()
    {
        weaponSprite = GetComponent<SpriteRenderer>();
        weaponAnimator = GetComponent<Animator>();
    }
}
