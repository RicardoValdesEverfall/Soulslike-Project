using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RVT
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        private CharacterManager _characterManager;

        [Header("Position")]
        public NetworkVariable<Vector3> NetworkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> NetworkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 NetworkPositionVelocity;
        public float NetworkPositionSmoothTime = 0.1f;
        public float NetworkRotationSmoothTime = 0.1f;

        [Header("Animator")]
        public NetworkVariable<float> animatorHorizontalParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> animatorVerticalParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> animatorMoveParameter = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }

        [ServerRpc] public void NotifyServerOfAnimationActionServerRpc(ulong clientID, string animID, bool rootMotion)
        {
            if (IsServer)
            {
                PlayerAnimationActionForAllClientsClientRpc(clientID, animID, rootMotion);
            }
        }

        [ClientRpc] public void PlayerAnimationActionForAllClientsClientRpc(ulong clientID, string animID, bool rootMotion)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformAnimationActionFromServer(animID, rootMotion);
            }
        }

        private void PerformAnimationActionFromServer(string animID, bool rootMotion)
        {
            _characterManager.ApplyRootMotion = rootMotion;
            _characterManager.animator.CrossFade(animID, 0.2f);
        }
    }
}
