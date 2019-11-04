using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseShootScript : MonoBehaviour
{
	/// <summary>
	/// Projectile min power
	/// </summary>
	public float MinPower = 1.0f;

	/// <summary>
	/// Projectile max power
	/// </summary>
	public float MaxPower = 10.0f;

	/// <summary>
	/// Projectile maximum normalized power
	/// </summary>
	public float MaxNormPower = 1.0f;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;

	private float power = 0.0f;

	private Vector3 mouseDownPos;

    private LineRenderer lineRenderer;

	private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			isDragging = true;
		}

		if (Input.GetMouseButtonUp(0) && isDragging)
		{
			Debug.Log(power);
			fire();
			isDragging = false;
            lineRenderer.SetPosition(1, this.transform.position);
		}

		if (isDragging)
		{
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDiff = mousePos - mouseDownPos;
			power = Vector3.ClampMagnitude(mouseDiff, MaxPower).magnitude;
			power = normalize_power(power, MaxPower, 0.0f, 0.0f, MaxNormPower);

            Vector3 dir = Quaternion.AngleAxis(90, Vector3.forward) * this.transform.right;
            lineRenderer.SetPosition(1, dir.normalized * mouseDiff.magnitude);
		}
    }


	private void fire()
	{
		var shotTransform = Instantiate(shotPrefab) as Transform;

		// Assign position
		shotTransform.position = transform.position;

		// Make the weapon shot always towards it
		ParticleMoveScript move = shotTransform.gameObject.GetComponent<ParticleMoveScript>();
		if (move != null)
		{
			Vector3 dir = Quaternion.AngleAxis(90, Vector3.forward) * this.transform.right;
			// Sets the power
			move.InitialSpeed = power;
			move.Direction = dir.normalized; // towards in 2D space is the right of the sprite
		}
	}

	private float normalize_power(float pow, float pow_max, float pow_min, float new_min, float new_max)
	{
		return (pow / ((pow_max - pow_min) / (new_max - new_min))) + new_min;
	}
}
