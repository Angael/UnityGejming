using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    //zmienne edytowane już w unity, bo każdy pistolet może mieć inne statystyki
    [Header("Gun stats")]
    public float damage;
    public float shotsPerSecond;
    public int range;

    [Space]
    [Header("Gun effects")]
    public LineRenderer shootLine;
    public GameObject shootParticles; //pojawiają się przy celu

    private Transform gunPoint;
    private float timeOfNextShot = 0; //czas w którym już będzie można wykonać następny strzał

    void Awake()
    {
        //narazie zakładam że jest tylko jedna rzecz (child) w naszej broni, punkt z którego lecą pociski
        gunPoint = transform.GetChild(0).transform;
    }

    public void useMain()
    {
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

        //badamy czy przed lufą jest jakiś cel
        RaycastHit2D hit = Physics2D.Raycast(gunPoint.position, gunPoint.TransformDirection(Vector2.right), 20);
        float distance = 0f; // distance to tutaj odległość od trafionego celu
        if (hit.collider != null)
        {
            distance = Vector2.Distance(gunPoint.transform.position, hit.point);
            Debug.Log("Hit something with tag: " + hit.transform.tag);

            Enemy enemyScript = hit.transform.parent.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
            //W przyszłości cała akcja zadawania obrażeń i strzelania powinna nie być wykonywana przez playera,
            //a przez samą broń, która będzie osobnym objektem
            shootLine.enabled = true;
            shootLine.SetPosition(0, gunPoint.transform.position);
            shootLine.SetPosition(1, hit.point);
            Invoke("HideShootLine", 0.04f);

            //Pojawiają się cząsteczki po uderzeniu
            GameObject spawnedParticles = Instantiate(shootParticles, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(spawnedParticles, 0.25f);
        }
        else
        {
            shootLine.enabled = true;
            shootLine.SetPosition(0, gunPoint.transform.position);
            shootLine.SetPosition(1, gunPoint.transform.position + gunPoint.TransformDirection(Vector2.right * 20));
            Invoke("HideShootLine", 0.04f);
        }
    }

    //ukryj linię po strzale
    void HideShootLine()
    {
        shootLine.enabled = false;
    }

}
