using UnityEngine;

public class PortalTeleport2chieu : MonoBehaviour
{
    public Transform targetPosition; // Nơi dịch chuyển tới
    private static bool globalCooldown = false; // Cooldown toàn cục để áp dụng khi vừa qua portal

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !globalCooldown)
        {
            StartCoroutine(TeleportWithGlobalCooldown(other.transform));
        }
    }

    System.Collections.IEnumerator TeleportWithGlobalCooldown(Transform player)
    {
        globalCooldown = true; // Bật cooldown
        yield return new WaitForSeconds(0.1f); // Delay nhẹ

        player.position = targetPosition.position;

        yield return new WaitForSeconds(5f); // Đợi 5 giây rồi mới cho phép dùng lại
        globalCooldown = false;
    }
}
