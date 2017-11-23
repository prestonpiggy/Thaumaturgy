using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

using TurkeyWork.Actors;
using TurkeyWork.Events;

namespace TurkeyWork.Cameras
{
    /*
     * Kommentoin aika ison kansan juttuja pois ja lisäsin muutaman toiminnon vähän erilailla.
     * Ootko varma että tää on kaikki mitä tarttit? Mites noin prefabit (cam ja cam1)
     * Kerro mitän tarviit ja mitä dataaa mahdollisesti haluisit saada näppärästi.
     */

    public sealed class CameraSystem : MonoBehaviour
    {
        public float OrthographicSize = 7;
        public Vector3 LookOffset = new Vector3 (0, 1, -10);
        public Vector2 MaxLook = new Vector2 (5, 3);

        public CinemachineVirtualCamera cam;
        public CinemachineTransposer cam1;

        [AssetsOnly]
        public GameEvent LocalPlayerCreated;

        GameObject player;
        Camera mainCamera;
        Vector3 actorVelocity;
        Vector3 p;
        IActorMotor actorMotor;

        // Use this for initialization
        void Start () {
            DontDestroyOnLoad (this);
            cam = Instantiate (cam, transform.position, Quaternion.identity, transform);
            cam1 = cam.GetComponentInChildren<Cinemachine.CinemachineTransposer> ();
            cam1.GetComponent<Cinemachine.CinemachineComposer> ().m_TrackedObjectOffset = LookOffset;
            mainCamera = Camera.main;

            player = Networking.NetworkManager.GetLocalPlayer ().PlayerEntity.gameObject;

            if (player == null) {
                enabled = false;
            }
        }

        // This should be called when the Local Player Created GameEvent is raised.
        // A bit stupid way of doing this. Works for now.
        public void OnPlayerCreated () {
            player = Networking.NetworkManager.GetLocalPlayer ().PlayerEntity.gameObject;
            actorMotor = player.GetComponent<IActorMotor> ();

            enabled = true;
            cam.m_Follow = player.transform;      
            cam.m_Lens.OrthographicSize = OrthographicSize;          
        }

        // Late update to ensure that the player is already updated this frame.
        // I different solution maybe in order.
        void LateUpdate () {
            // Replaced controller.PossibleDeltaMove with IActorMotor.Velocity
            actorVelocity = actorMotor.MovementDelta;


            //Debug.Log(x + "... " + y);
            //x = Input.mousePosition.x;   Used when camera on mouse
            //y = Input.mousePosition.y;
            p = mainCamera.ScreenToWorldPoint (actorVelocity);
            //x = p.x- player.transform.position.x;
            //y = p.y - player.transform.position.y;
            var frameOffset = new Vector3 (
                Mathf.Clamp (actorVelocity.x * 100, -MaxLook.x, MaxLook.x),
                Mathf.Clamp (actorVelocity.y * 100, -MaxLook.y, MaxLook.y),
                0f) + LookOffset;
            cam1.m_FollowOffset = frameOffset;
            //Debug.Log(x + "... " + y);

        }
    }
}