using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniMapClickHandler : MonoBehaviour, IPointerClickHandler
{
    public static MiniMapClickHandler instance;

    public Camera minimapCamera;
    public Camera mainCamera;
    public RectTransform minimapRect;

    public bool isPlayerSpawned = false;

    public float followSmoothSpeed = 0.125f;

    public bool isMiniMapClicked = false;

    public Button exitButton;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        exitButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localCursor;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRect, eventData.position,
            eventData.pressEventCamera, out localCursor)) return;

        Rect rect = minimapRect.rect;
        float normalizedX = (localCursor.x - rect.x) / rect.width;
        float normalizedY = (localCursor.y - rect.y) / rect.height;

        Ray ray = minimapCamera.ViewportPointToRay(new Vector3(normalizedX, normalizedY, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && isPlayerSpawned)
        {
            isMiniMapClicked = true;

            Vector3 hitPoint = hit.point;

            float y_Offset = mainCamera.transform.position.y;
            Vector3 newCameraPosition = new Vector3(hitPoint.x, y_Offset, hitPoint.z);

            mainCamera.transform.position = newCameraPosition - new Vector3(0, 0, 10f);

            mainCamera.transform.rotation = Quaternion.Euler(45.31f, mainCamera.transform.rotation.eulerAngles.y,
                mainCamera.transform.rotation.eulerAngles.z);
        }
    }
}
