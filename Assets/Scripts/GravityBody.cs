using UnityEngine;

public class GravityBody : MonoBehaviour
{
     Rigidbody2D rb;

    Vector2 lookDirection;

    float lookAngle;

    [Header ("Gravity")]
    
    [Range(0.0f, 1000.0f)]
    public float maxGravDist = 150.0f;
    
    [Range(0.0f, 1000.0f)]
    public float maxGravity = 150.0f;
    
    public GameObject planet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        float dist = Vector3.Distance(planet.transform.position, transform.position);

        Vector3 v = planet.transform.position - transform.position;
        rb.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);

        lookDirection = planet.transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);  
    }
}