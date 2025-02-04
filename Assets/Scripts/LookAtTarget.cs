using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour
{
    [Tooltip("This is the object that the script's game object will look at by default")]
    public GameObject defaultTarget; // The default target that the camera should look at

    [Tooltip("This is the object that the script's game object is currently looking at based on the player clicking on a GameObject")]
    public GameObject currentTarget; // The target that the camera should look at

    [Tooltip("The child object of the camera that moves to the clicked object")]
    public GameObject childObject; // The object that moves to the clicked target

    // Start happens once at the beginning of playing
    void Start()
    {
        if (defaultTarget == null)
        {
            defaultTarget = this.gameObject;
            Debug.Log("defaultTarget not specified. Defaulting to parent GameObject");
        }

        if (currentTarget == null)
        {
            currentTarget = this.gameObject;
            Debug.Log("currentTarget not specified. Defaulting to parent GameObject");
        }

        if (childObject == null)
        {
            Debug.LogWarning("childObject is not assigned! Make sure to assign a child object in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If primary mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (hits.Length > 0)
            {
                RaycastHit hit = hits[0];
                currentTarget = hit.collider.gameObject;

                Debug.Log("currentTarget changed to " + currentTarget.name);

                // Move the childObject to the clicked object's position
                if (childObject != null)
                {
                    childObject.transform.position = currentTarget.transform.position;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) // If the second mouse button is pressed
        {
            currentTarget = defaultTarget;
            Debug.Log("currentTarget changed to " + currentTarget.name);
        }

        // If a currentTarget is set, then look at it
        if (currentTarget != null)
        {
            transform.LookAt(currentTarget.transform);
        }
        else
        {
            currentTarget = defaultTarget;
            Debug.Log("currentTarget changed to " + currentTarget.name);
        }
    }
}