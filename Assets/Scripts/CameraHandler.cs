using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera mainCamera, miniMapCamera;
    Vector3 initialCameraPosition, initialMiniMapCameraPosition;

    void LateUpdate()
    {
        if (SpawnScript.instance && MiniMapClickHandler.instance &&
            MiniMapClickHandler.instance.isPlayerSpawned && !MiniMapClickHandler.instance.isMiniMapClicked)
        {
            Transform player = SpawnScript.instance.myPlayerObject.transform;

            initialCameraPosition = player.position - Vector3.forward * 20f;
            initialMiniMapCameraPosition = player.position;

            initialCameraPosition.y = mainCamera.transform.position.y;
            initialMiniMapCameraPosition.y = miniMapCamera.transform.position.y;

            mainCamera.transform.position = initialCameraPosition;
            miniMapCamera.transform.position = initialMiniMapCameraPosition;
        }
    }
}
