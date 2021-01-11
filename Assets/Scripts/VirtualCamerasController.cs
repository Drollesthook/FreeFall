using Cinemachine;

using UnityEngine;

public class VirtualCamerasController : MonoBehaviour {
   CinemachineVirtualCamera[] _cameras;

   void Awake() {
      _cameras = GetComponentsInChildren<CinemachineVirtualCamera>();
      LevelBuilder.Instance.PlayerSpawned += OnPlayerSpawned;
   }

   void OnDestroy() {
      LevelBuilder.Instance.PlayerSpawned -= OnPlayerSpawned;
   }

   void OnPlayerSpawned(Transform player) {
      RefreshLookAndFollowOnPlayer(player);
   }

   void RefreshLookAndFollowOnPlayer(Transform player) {
      foreach (CinemachineVirtualCamera camera in _cameras) {
         camera.LookAt = player;
         camera.Follow = player;
      }
   }
}
