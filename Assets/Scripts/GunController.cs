using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private float reloadTime = 3f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private Text ammoCounter;

    private GunAudioController gunSoundEffect;

    private float nextTimeToFire = 1f;
    private int currentAmmo;
    private bool isReloading = false;
   
    private void Awake()
    {
        gunSoundEffect = GetComponent<GunAudioController>();
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isReloading) return;

        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            if (currentAmmo <= 0)
            {
                gunSoundEffect.Play("no ammo");
                return;
            }

            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        gunSoundEffect.Play("shoot");

        currentAmmo--;
        //Debug.Log(currentAmmo);
        ammoCounter.text = currentAmmo.ToString();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if(enemy != null)
                enemy.TakeDamage(damage);
        }

        BulletLine bulletLine = GetComponent<BulletLine>();
        bulletLine.SetPositions(new Vector3[] {muzzleFlash.transform.position, muzzleFlash.transform.position + fpsCam.transform.forward * range});
    }

    private IEnumerator Reload()
    {
        gunSoundEffect.Play("reload");

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Reloading", true);

        isReloading = true;
        //Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString();
        isReloading = false;
        Debug.Log(currentAmmo);
    }
}