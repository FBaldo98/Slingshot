using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMoveScript : MonoBehaviour
{

    public float G = 10.0f;

    public float InitialSpeed = 10.0f;

    public Vector2 Direction = new Vector2(1, 0);

    public float Life_Time = 100f;

    protected Vector2 Velocity = new Vector2(0.0f, 0.0f);

    private Vector3 Position;

    private float lifeTime;

    private GameObject[] Planets;

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        Velocity = Direction * InitialSpeed;
        lifeTime = Life_Time;
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        else
            Destroy(gameObject, .5f);
    }

    private void FixedUpdate()
    {

        Vector2 acceleration = new Vector2(0f, 0f);

        foreach(GameObject obj in Planets)
        {
            // Object distance
            Vector3 distance = gameObject.transform.position - obj.transform.position;

            // Squared distance
            float d = distance.x * distance.x + distance.y * distance.y;

            // Planet mass
            float mass = obj.GetComponent<PlanetScript>().Mass;

            // Compute velocity
            float ax = (G * mass * distance.x) / (d * Mathf.Sqrt(d));
            float ay = (G * mass * distance.y) / (d * Mathf.Sqrt(d));

            acceleration.x += ax;
            acceleration.y += ay;

        }

        Velocity -= acceleration;

        Position.x += Velocity.x;
        Position.y += Velocity.y;
        transform.position = Position;
    }
}
