using UnityEngine;
using UnityEngine.UI;

public class PortalTeleport2chieu : MonoBehaviour
{
    public Transform targetPosition; // Nơi dịch chuyển tới
    public GameObject teleportConfirmPanel; // Panel xác nhận dịch chuyển
    private Transform currentPlayer; // Người chơi hiện tại chạm portal
    private static bool globalCooldown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !globalCooldown)
        {
            currentPlayer = other.transform;
            teleportConfirmPanel.SetActive(true); // Hiện UI xác nhận
        }
    }

    // Gọi khi người chơi bấm nút "Yes"
    public void ConfirmTeleport()
    {
        if (currentPlayer != null)
        {
            StartCoroutine(TeleportWithGlobalCooldown(currentPlayer));
            teleportConfirmPanel.SetActive(false);
        }
    }

    // Gọi khi người chơi bấm "No"
    public void CancelTeleport()
    {
        teleportConfirmPanel.SetActive(false);
        currentPlayer = null;
    }

    System.Collections.IEnumerator TeleportWithGlobalCooldown(Transform player)
    {
        globalCooldown = true;
        yield return new WaitForSeconds(0.1f);

        player.position = targetPosition.position;

        yield return new WaitForSeconds(2f);
        globalCooldown = false;
    }
}
