using UnityEngine;
using System.Collections;
using TMPro; 

public class AutoDialogueSequence : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;
    public GameObject helloTextObj;
    public GameObject secondTextObj;

    [Header("Settings")]
    public float triggerDistance = 3f; 
    public float displayTime = 5f;     

    [Header("State")]
    public bool isHelloooFinished = false; 

    private bool sequenceStarted = false;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (helloTextObj != null) helloTextObj.SetActive(false);
        if (secondTextObj != null) secondTextObj.SetActive(false);
    }

    void Update()
    {
        if (sequenceStarted) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= triggerDistance)
        {
            StartCoroutine(PlayDialogueSequence());
        }
    }

    IEnumerator PlayDialogueSequence()
    {
        sequenceStarted = true;

        if (helloTextObj != null)
            helloTextObj.SetActive(true);

        Debug.Log("Hellooo appeared!");

        yield return new WaitForSeconds(displayTime);

        if (helloTextObj != null)
            helloTextObj.SetActive(false);

        if (secondTextObj != null)
            secondTextObj.SetActive(true);

        isHelloooFinished = true;
        Debug.Log("Hellooo finished. Next text active.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}