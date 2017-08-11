using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveGranade : MonoBehaviour {


    [Header("Specifics")]
    public Transform[] parts;
    public LineRenderer lineRenderer;
    public PolygonCollider2D polyCollider;
    public float distance = 1f;
    public float throwSpeed = 4;
    public float width = 0.4f;
    public float lifeSpan = 2f;
    public float armSpeed = 4;
    private bool isArmed = false;
    private float timeOfSpread = Mathf.Infinity;
    public float timeOfSpreading = 1;
    private bool isSpreading = false;
    

    public Vector3 targetLocation;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (targetLocation!=null)
        {
            if (Vector3.Distance(transform.position, targetLocation) < 0.3f && !isSpreading)
            {
                timeOfSpread = Time.time + timeOfSpreading;
                isSpreading = true;
            }
            if (Time.time >= timeOfSpread)
            {

                SetPartsPositions();
                

                if (isArmed)
                {
                    SetCollider();
                    SetLinePositions();
                    Destroy(this.gameObject, lifeSpan);
                }
                
            }else
            {
                transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime* throwSpeed);


            }
        }
    }

    
    public void SetLinePositions()
    {
        int i = 0;
        foreach (Transform part in parts)
        {
            lineRenderer.SetPosition(i, new Vector3(part.localPosition.x, part.localPosition.y, part.localPosition.z +0.1f));
            //dont use, but lerp is cool:
            //lineRenderer.SetPosition(i, Vector3.Lerp(part.localPosition , new Vector3(part.localPosition.x, part.localPosition.y, part.localPosition.z + 0.1f), Time.deltaTime * armSpeed) );
            i++;
        }
    }

    public void SetPartsPositions()
    {
        int i = 0;
        Vector3 testDistanceA = new Vector3();
        Vector3 testDistanceB = new Vector3();
        foreach (Transform part in parts)
        {
            //localposition is vector right with some distance, imagine radius in circle, rotated by 72*2 around its Z axis
            part.rotation = Quaternion.AngleAxis(72*i, Vector3.back) * Quaternion.identity;
            //Dodatkowy rotate i 72, by były obrócone do środka, a nie do lini , to ma do czynienia ze spritem
            part.Rotate(Vector3.back * 50);

            //mnożenie vektora przez rotacje wskazuje gdzie on będzie wskazywał, czyli taka rotacja quaterniona i magnitude vektora
            part.localPosition = Vector3.Lerp(part.localPosition, (Quaternion.AngleAxis(72 * i, Vector3.back) * Quaternion.identity) * (Vector2.right * distance), Time.deltaTime * armSpeed);
            //Tutaj bierzemy jakąś częśc i jej cel, by zmieżyć czy już można uaktywnić collider i linerenderer
            if (i == 0)
            {
                testDistanceA = part.localPosition;
                testDistanceB = (Quaternion.AngleAxis(72 * i, Vector3.back) * Quaternion.identity) * (Vector2.right * distance);
            }
            i++;
        }
        if (Vector3.Distance(testDistanceA, testDistanceB) < 0.1f)
        {
            isArmed = true;
        }
    }

    public void SetCollider()
    {
        
        int len = polyCollider.points.Length;
        Vector2[] path = new Vector2[len];
        for (int i = 0; i < len; i++)
        {
            if (i < len/2)
            {
                //W jednym odejmujemy od dystansu pół szerokości collidera a w drugim dodajemy, żeby z dwuch stron było tak samo
                Vector2 pos = (Quaternion.AngleAxis(72 * i, Vector3.back) * Quaternion.identity) * (Vector2.right * (distance - width/2) );
                path[i] = pos;
            }
            

            if(i >= len/2)
            {
                //teraz od tego samego co poprzednia czesc skonczyla tylko w drugą stronę
                Vector2 pos = (Quaternion.AngleAxis(-72 * i-1, Vector3.back) * Quaternion.identity) * (Vector2.right *  (distance + width / 2));
                path[i] = pos;
            }
            

        }
        polyCollider.SetPath(0, path);
        polyCollider.enabled = true;
    }

    public void Throw(Vector3 target)
    {
        targetLocation = target;
        
    }
}
