using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

using TurkeyWork.Actors;
using TurkeyWork.Events;

namespace TurkeyWork.Cameras
{
  
    public sealed class CameraSystem : MonoBehaviour
    {
        public float OrthographicSize = 7;
        public Vector3 LookOffset = new Vector3 (0, 0, -10);
        public Vector2 MaxLook = new Vector2 (0.1E+3f, 0.1E+3f);

        public CinemachineVirtualCamera cam;
        public CinemachineTransposer cam1;
        public Queue postmoves = new Queue();

        GameObject player;
        Camera mainCamera;
        Vector3 actorVelocity;
        Vector3 p;
        IActorMotor actorMotor;
        bool start = false;
        Vector3 velocity;
        float CenterCamTimer = 2;
        float TimeSpend = 0;
        // Use this for initialization
        void Start () {
            enabled = false;

            DontDestroyOnLoad (this);
            cam = Instantiate (cam, transform.position, Quaternion.identity, transform);
          
            mainCamera = Camera.main;
            cam1 = cam.GetComponentInChildren<Cinemachine.CinemachineTransposer>();
        }

        public void GetPlayerInfo () {
            player = Networking.NetworkManager.GetLocalPlayer ().PlayerEntity.gameObject;
            actorMotor = player.GetComponent<IActorMotor> ();
            cam.m_Follow = player.transform;      
            cam.m_Lens.OrthographicSize = OrthographicSize;
            Invoke("SetTrue",0.1f); // need to wait for player to be funny instantiated, otherwise 2-10 errors for NaN camera position 
            Debug.Log("info catched");
            cam1.m_FollowOffset = Vector3.zero;
        }


        void LateUpdate () {
                
            actorVelocity = actorMotor.Velocity;

                
            //x = Input.mousePosition.x;   Used when camera on mouse
            //y = Input.mousePosition.y;
            //p = mainCamera.ScreenToWorldPoint(actorVelocity);
            //x = p.x- player.transform.position.x;
            //y = p.y - player.transform.position.y;
            if (actorVelocity == Vector3.zero && cam1.m_FollowOffset != new Vector3(0, 0, -10))
            {
                TimeSpend = Time.time;
                if (TimeSpend>=CenterCamTimer)
                {                      
                    actorVelocity = new Vector3(-cam1.m_FollowOffset.x * 0.1f, -cam1.m_FollowOffset.y * 0.1f, -10f);
                }
                    
            }

            var frameOffset = new Vector3(
                Mathf.Clamp(actorVelocity.x, -MaxLook.x, MaxLook.x),
                Mathf.Clamp(actorVelocity.y, -MaxLook.y, MaxLook.y),
                -10f);

            cam1.m_FollowOffset = new Vector3(
                Mathf.Clamp(cam1.m_FollowOffset.x + frameOffset.x/25, -3, 3),
                Mathf.Clamp(cam1.m_FollowOffset.y + frameOffset.y/100, 0, 2),
                -10f);
               
        }
        public void SetTrue()
        {
            enabled = true;
        }
    }
}