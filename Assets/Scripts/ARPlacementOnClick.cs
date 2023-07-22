using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlacementOnClick : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public ARRaycastManager RayCastManager;

    private int numberOfModels = 0;

    void Update()
    {
        if (numberOfModels > 0)
            return;

        if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
        {
            List<ARRaycastHit> touches = new List<ARRaycastHit>();

            RayCastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (touches.Count > 0)
            {
                GameObject.Instantiate(ObjectToSpawn, touches[0].pose.position, touches[0].pose.rotation);
                numberOfModels++;
            }
        }
    }
}
