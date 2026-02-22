using System.Collections;
using UnityEngine;

public class interact : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            RaycastHit hit;
            Ray ray = Camera .main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast (ray,out hit ,10000))
            {
                return;
            }

            hit.rigidbody.AddForce(ray.direction * 1000);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit, 10000))
            {
                return;
            }

            if (hit.rigidbody.gameObject.name == "SpinHead")
            {
                hit.rigidbody.AddTorque(Vector3.up * Input.GetTouch(0).deltaPosition.x * -10);
            }
        }
    }
}
