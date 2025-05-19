using UnityEngine;

public class OneWayTeleport : MonoBehaviour
{
    public Transform targetPosition; // Nơi dịch chuyển đến
    private bool isTeleporting = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportWithDelay(other.transform));
        }
    }

    System.Collections.IEnumerator TeleportWithDelay(Transform player)
    {
        isTeleporting = true;

        yield return new WaitForSeconds(0.1f); // Delay ngắn

        player.position = targetPosition.position;

        yield return new WaitForSeconds(0.5f); // Ngăn spam
        isTeleporting = false;
    }
}
