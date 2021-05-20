using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SmoothCameraRMB
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Main : MonoBehaviour
    {
        bool d;
        Vector3 p, v;
        readonly float s = FlightCamera.fetch.orbitSensitivity;

        void Update()
        {
            if (CameraManager.Instance.currentCameraMode == CameraManager.CameraMode.Flight)
            {
                var c = FlightCamera.fetch;

                if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
                {
                    p = Input.mousePosition;
                    d = true;
                    c.orbitSensitivity = 0;
                }

                if (!Input.GetMouseButton(1))
                {
                    d = false;
                    c.orbitSensitivity = s;
                }

                var q = Input.mousePosition;

                if (d)
                {
                    if (Input.GetKey(KeyCode.LeftAlt))
                        v.z += (q.y - p.y) / 10000f;
                    else
                        v += (q - p) / 10000f;
                }

                c.camHdg += v.x;
                c.camPitch -= v.y;
                c.SetDistance(c.Distance * (1 - v.z));

                p = q;
                v *= Input.GetMouseButton(0) ? 0.9f : (Input.GetMouseButton(1) ? 1 : 0.99f);
            }
        }
    }
}