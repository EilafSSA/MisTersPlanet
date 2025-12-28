using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 2f;

    [Header("UI Elements")]
    public GameObject interactionPrompt;

    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            GameObject playerObj = GameObject.Find("NPC - Old Man");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= interactionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                if (interactionPrompt != null)
                    interactionPrompt.SetActive(true);
            }

            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartDialogue();
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);
            }
        }
    }

    void StartDialogue()
    {
        Debug.Log("Talking to NPC - Old Man!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}