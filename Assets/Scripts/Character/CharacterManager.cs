using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RVT
{
    public class CharacterManager : NetworkBehaviour
    {
        [HideInInspector] public CharacterController CharacterControllerComponent;
        [HideInInspector] public Animator animator;

        [HideInInspector] public CharacterNetworkManager CharacterNetworkManagerComponent;

        [Header("Flags")]
        public bool PerformingAction;
        public bool ApplyRootMotion;
        public bool CanRotate = true;
        public bool CanMove = true;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            CharacterControllerComponent = GetComponent<CharacterController>();
            CharacterNetworkManagerComponent = GetComponent<CharacterNetworkManager>();

            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                CharacterNetworkManagerComponent.NetworkPosition.Value = transform.position;
                CharacterNetworkManagerComponent.NetworkRotation.Value = transform.rotation;
            }

            else
            {
                transform.position = Vector3.SmoothDamp
                    (transform.position,
                    CharacterNetworkManagerComponent.NetworkPosition.Value,
                    ref CharacterNetworkManagerComponent.NetworkPositionVelocity,
                    CharacterNetworkManagerComponent.NetworkPositionSmoothTime);

                transform.rotation = Quaternion.Slerp
                    (transform.rotation,
                    CharacterNetworkManagerComponent.NetworkRotation.Value,
                    CharacterNetworkManagerComponent.NetworkRotationSmoothTime);
            }
        }

        protected virtual void LateUpdate()
        {

        }
    }
}
