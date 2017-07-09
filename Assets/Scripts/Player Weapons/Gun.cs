using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour {

    //zmienne edytowane już w unity, bo każdy pistolet może mieć inne statystyki
    [Header("Gun stats")]
    public float damage;
    public float penetration = 0.5f; // w procentach ile pancerza omija, 1 = omija cały pancerz, 0 = nie ma żadnej penetracji
    public float shotsPerSecond;
    public float reloadTime = 1f;
    public float bulletSpeed;
    public float MaxRecoil = 5;
    public float recoilAcceleration = 1;
    public float recoilStopTime = 1;
    public GameObject bullet;
    public int MaxAmmoInMag = 60;
    public int AmmoInStock = 240;
    public int ammoLeftInMag = 0;

    [Space]
    [Header("Gun effects")]
    public GameObject shootParticles; //pojawiają się przy celu

    private Transform gunPoint;
    private float timeOfNextShot = 0; //czas w którym już będzie można wykonać następny strzał
    private float timeOfReloaded; // Kiedy się skończy reload
    private float timeOfStartOfReload; // Kiedy zaczeliśmy reload
    private bool isReloading = false; // czy sprawdzać czas w którym będzie reloaded
    private WeaponHolder weaponHolder;
    [SerializeField]
    private float currentRecoil;
    private int recoilDirection = 1;

    void Awake()
    {
        //narazie zakładam że jest tylko jedna rzecz (child) w naszej broni, punkt z którego lecą pociski
        gunPoint = transform.GetChild(0).transform;
        weaponHolder = transform.parent.GetComponent<WeaponHolder>();
    }

    public void useMain()
    {
        if (isReloading)
        {
            //Nie można strzelać jeśli robimy właśnie reloading
            return;
        }

        //Sprawdzamy czy już można oddać następny strzał
        if (Time.time < timeOfNextShot)
        {
            //jeśli jeszcze nie to kończymy funkcję
            return;
        }
        else {
            //jeśli tak to ustalamy czas w którym będzie można wykonać następny strzał
            timeOfNextShot = Time.time + 1/shotsPerSecond;
        }
        if(ammoLeftInMag <= 0)
        {
            return;
        }
        else
        {
            ammoLeftInMag--;
        }
        UpdateUI();

        GameObject shotBullet = Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        Bullet bulletScript = shotBullet.GetComponent<Bullet>();
        bulletScript.damage = damage;
        bulletScript.penetration = penetration;
        bulletScript.hitEffect = shootParticles;

        shotBullet.GetComponent<Rigidbody2D>().velocity = gunPoint.right * bulletSpeed;
        Destroy(shotBullet, 3f);

        //rotate the gun a little bit, so we have some kind of fireSpread
        currentRecoil += (recoilAcceleration * 1/shotsPerSecond); // mnożymy przez 1/shotsPerSecond bo w ten sposób nie ważne jak szybko broń strzela, 
        //... recoilAcceleration będzie coś dla nas znaczyło
        if (currentRecoil > MaxRecoil)
        {
            currentRecoil = MaxRecoil;
        }
        if ((Random.value > 0.5f))
        {
            recoilDirection *= -1;
        }
        //parent parent bo obracamy "Player"
        transform.parent.parent.transform.Rotate(Vector3.back, currentRecoil*recoilDirection);
    }

    //Kliknięte r, zacznij proces przeładowywania
    void R()
    {
        int missingAmmoInMag = MaxAmmoInMag - ammoLeftInMag;
        if (missingAmmoInMag == 0)
        {
            //Jeśli nie brakuje ammo, no to nie przeładowywujemy
            return;
        }

        timeOfStartOfReload = Time.time;
        timeOfReloaded = Time.time + reloadTime;
        isReloading = true;
    }
    //Skończ proces przeładowania
    void Reload()
    {
        isReloading = false; // już nie ma animacji więc isReloading = false
        int missingAmmoInMag = MaxAmmoInMag - ammoLeftInMag;
        if (missingAmmoInMag > AmmoInStock)
        {
            ammoLeftInMag += AmmoInStock;
            AmmoInStock = 0;

        }
        else
        {
            AmmoInStock -= missingAmmoInMag;
            ammoLeftInMag += missingAmmoInMag;
        }
        UpdateUI();
    }

    void OnEnable()
    {
        UpdateUI(); // pokaż prawidłową amunicję kiedy zmieniamy broń
    }
    void OnDisable()
    {
        //Jeśli broń zostanie zmieniona w trakcie przeładowania to nie można pozwolić by się skończyło przeładowanie
        // Dodatkowo przerywamy animacje przeładowania
        isReloading = false;
        weaponHolder.ReloadAnimationHide();
    }

    void Update()
    {
        //Jeśli jest reload to updejtuj animacje
        if (isReloading)
        {
            float reloadProgress = Mathf.InverseLerp(timeOfStartOfReload, timeOfReloaded, Time.time);
            weaponHolder.ReloadAnimationUpdate(reloadProgress);
            if (timeOfReloaded <= Time.time)
            {
                Reload();
                weaponHolder.ReloadAnimationHide();
                
            }
        }

        //Jeśli ciągle strzelamy to nie ma co zwalniać zwiększania się rozrzutu broni
        if (Time.time > timeOfNextShot+Time.deltaTime)
        {
            currentRecoil -= (MaxRecoil / recoilStopTime) * Time.deltaTime;
            if (currentRecoil < 0)
            {
                currentRecoil = 0;
            }
        }
        

    }

    //update ui, każe singletonowi napisać coś w ui, np. ile jest amunicji czy cokolwiek innego
    void UpdateUI()
    {
        SingleTon.instance.UpdateAmmoUI(ammoLeftInMag,AmmoInStock);
    }
}
