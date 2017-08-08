using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//Skrypt wymaga tych rzeczy na objekcie
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]

//Skrypt z pathfindingiem, troszkę z neta, główne załorzenia skryptu to że ma znajdować drogę do celu
//TODO: Inny skrypt, który znajduje pozycje do strzelania (dla innych wrogów) ?
public class EnemyAI : MonoBehaviour {

    public Transform target;

    //Jak często updejtujemy ścieżkę
    public float updateRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private Vector3 dir = new Vector3();
    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 3000f;
    public ForceMode2D fMode;


    private bool pathIsEnded = false;

    //Jak blisko musi być punktu by uznać że do niego już doszedł?
    public float nextWaypointDistance = 1f;

    private int currentWaypoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponentInChildren<CircleCollider2D>();
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }

        //Wytycz ścieżkę od razu
        //seeker.StartPath(transform.position, target.position, OnPathComplete);

        //Rozpocznij wytyczanie ścieżki co jakiś czas
        StartCoroutine( UpdatePath() );
    }

    IEnumerator UpdatePath()
    {
        if(target == null)
        {
            yield return false;
        }
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        //Jeśli dystans do celu W LINI PROSTEJ jest większy niż nasz promień plus jakaś mała liczba to wyznaczaj trasę
        if (distanceToTarget > col.radius+0.1f)
        {
            //Debug.Log("UpdatePath idziemy");
            
            //Wytycz ścieżkę
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            
            //Funkcja się wywoła co 1/updateRate sekund. UpdateRate = 4  daje nam cztery razy na sekundę
            yield return new WaitForSeconds(1 / updateRate);
            StartCoroutine(UpdatePath());
        }
        //Jeśli jest blisko, to nie szukaj trasy, bo tylko zajmować to będzie cpu, a i tak jesteś w zasięgu ataku z bliska
        else {
            //Debug.Log("UpdatePath doszlismy");
            //Funkcja się wywoła co 1/updateRate sekund. UpdateRate = 4  daje nam cztery razy na sekundę
            yield return new WaitForSeconds(1 / updateRate);
            //Sprawdź ponownie
            StartCoroutine(UpdatePath());
        }
        
    }

    //Jeśli doszło do celu to ta funkcja zostanie zawołana.
    //Narazie jeśli idzie do playera to nigdy nie dojdzie bo player ma jakaś szerokość i blokuje przed dojściem jeśli się nie mylę
    public void OnPathComplete (Path p)
    {
        //Debug.Log("we got a path, did it have an error?");
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        //Jeśli mamy cel i ścieżkę do przejścia i...
        if (target == null)
        {
            return;
        }

        if (path == null)
        {
            return;
        }
        

        //i nie ma już pozostałych kroków na naszej ścieżce
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }

            //Doszliśmy do celu
            pathIsEnded = true;
            return;
        }
        //nie doszliśmy do celu, jeszcze zostały kroki na ścieżcę
        pathIsEnded = false;

        //Obliczamy i dodajemy prędkość jednostki w kierunku następnego punktu na ścieżce
        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        dir *= speed * Time.fixedDeltaTime;
        

        rb.AddForce(dir, fMode);

        
        
        //Dodajemy punkt trasy jeśli już doszliśmy do jednego
        float distance = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
            //Debug.Log("CurrentWaypoint = " + currentWaypoint + " / " + path.vectorPath.Count);
            return;
        }
    }

    void Update()
    {
        //face direction it's walking to
        
        bool nearPlayer = 5 > Vector2.Distance(transform.position, target.position);
        
        if (nearPlayer)
        {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 0.1f);
        }
        else
        {
            if (rb.velocity.normalized != Vector2.zero)
            {
                float angle = Mathf.Atan2(rb.velocity.normalized.y, rb.velocity.normalized.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 0.1f);
            }
        }
        
    }
}
