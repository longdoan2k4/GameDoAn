using UnityEngine;
using UnityEngine.UI;

public class PortalTeleport : MonoBehaviour
{
    public GameObject teleportCanvas; // Panel chọn map
    public Transform targetLv1;
    public Transform targetLv5;
    public GameObject player;

    void Start()
    {
        teleportCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            teleportCanvas.SetActive(true);
            player = other.gameObject;
        }
    }

    public void TeleportToLv1()
    {
        player.transform.position = targetLv1.position;
        teleportCanvas.SetActive(false);
    }

    public void TeleportToLv5()
    {
        player.transform.position = targetLv5.position;
        teleportCanvas.SetActive(false);
    }

    public void CancelTeleport()
    {
        teleportCanvas.SetActive(false);
    }
}
