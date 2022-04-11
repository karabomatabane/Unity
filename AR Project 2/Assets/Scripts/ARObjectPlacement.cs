using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectPlacement : MonoBehaviour
{

    public ARSessionOrigin aRSessionOrigin;

    public List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    public GameObject cube;
    public GameObject instantiatedCube;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1) Detect the user touch.
        // 2) Project a raycast.
        // 3) Instatiate a virtual cube at the point where raycast meets the detected plane.

        if (Input.GetMouseButton(0))
        {
            bool collision = aRSessionOrigin.GetComponent<ARRaycastManager>().Raycast(Input.mousePosition, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

            if (collision)
            {

                if (instantiatedCube == null)
                {
                    instantiatedCube = Instantiate(cube);

                    foreach (var plane in aRSessionOrigin.GetComponent<ARPlaneManager>().trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    aRSessionOrigin.GetComponent<ARPlaneManager>().enabled = false;
                }

                instantiatedCube.transform.position = raycastHits[0].pose.position;
            }
        }
    }
}
