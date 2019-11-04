using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    private float shootCooldown;

    // Start is called before the first frame update
    void Start()
    {
        shootCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // Make the weapon shot always towards it
            ParticleMoveScript move = shotTransform.gameObject.GetComponent<ParticleMoveScript>();
            if (move != null)
            {
                Vector3 dir = Quaternion.AngleAxis(90, Vector3.forward) * this.transform.right;
                move.Direction = dir.normalized; // towards in 2D space is the right of the sprite
            }
        }
    }

    protected bool CanAttack
    {
        get => shootCooldown <= 0;
    }
}
