using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 lookAt = mouseScreenPosition;

		float angle = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x) * Mathf.Rad2Deg;

		this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

    }
}
