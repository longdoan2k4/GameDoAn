using UnityEngine;
using UnityEngine.UI;

public class CharTeleport : MonoBehaviour
{
    public GameObject teleportCanvas;       // UI Panel chứa nút Y/N
    public Transform targetLevel;           // Mục tiêu dịch chuyển
    public KeyCode openKey = KeyCode.E;     // Phím mở bảng xác nhận

    private GameObject player;

    void Start()
    {
        teleportCanvas.SetActive(false);

        // Tìm nhân vật theo tag (cần tag "Player")
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer;
        }
        else
        {
            Debug.LogError("Không tìm thấy Player có tag 'Player'");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            teleportCanvas.SetActive(true);
        }
    }

    // Nút Yes
    public void Teleport()
    {
        if (player != null && targetLevel != null)
        {
            player.transform.position = targetLevel.position;
        }
        teleportCanvas.SetActive(false);
    }

    // Nút No
    public void CancelTeleport()
    {
        teleportCanvas.SetActive(false);
    }

    // Nếu muốn đổi mục tiêu từ code khác
    public void SetTeleportTarget(Transform newTarget)
    {
        targetLevel = newTarget;
    }
}
