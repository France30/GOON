using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class VRGunController : MonoBehaviour
{    
    [SerializeField] private InputActionReference triggerButton;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 20f;
    [SerializeField] private int maxAmmo = 15;
    [SerializeField] private float impactForce = 600f;

    [SerializeField] private GameObject barrel;
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private TextMeshProUGUI ammoCounter;

    private HapticFeedback hapticFeedback;

    private GunAudioController gunSoundEffect;

    private float nextTimeToFire = 1f;
    private int currentAmmo;

    private void Awake()
    {
        gunSoundEffect = GetComponent<GunAudioController>();
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString();
    }

    private void Start()
    {
        hapticFeedback = GameObject.FindObjectOfType<HapticFeedback>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool isGamePaused = GameController.Instance.IsGamePaused;
        bool isGameOver = GameController.Instance.IsGameOver;
        if (isGamePaused || isGameOver) 
        {
            ShootEffectOnly();
            return; 
        }

        Gameplay();
    }

    private void ShootEffectOnly()
    {
        if (triggerButton.action.WasPressedThisFrame() && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            muzzleFlash.Play();
            gunSoundEffect.Play("shoot");
            hapticFeedback.SendHaptics();
        }
    }

    private void Gameplay()
    {
        if (triggerButton.action.WasPressedThisFrame() && Time.time >= nextTimeToFire)
        {
            if (currentAmmo <= 0)
            {
                gunSoundEffect.Play("no ammo");
                return;
            }

            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Vector3.Angle(transform.up, Vector3.up) > 90 && currentAmmo < maxAmmo)
            Reload();
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        gunSoundEffect.Play("shoot");      
        hapticFeedback.SendHaptics();

        currentAmmo--;
        ammoCounter.text = currentAmmo.ToString();

        RaycastHit hit;
        bool hasHit = Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, range);
        if (hasHit)
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

        BulletLine bulletLine = GetComponent<BulletLine>();
        bulletLine.SetPositions(new Vector3[] { barrel.transform.position, hasHit ? hit.point :
            barrel.transform.position + barrel.transform.forward * range });
    }

    private void Reload()
    {
        gunSoundEffect.Play("reload");

        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString();
    }
}
