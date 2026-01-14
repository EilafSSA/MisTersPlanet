using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoDialogueSequence : MonoBehaviour
{
    [System.Serializable]
    public class DialoguePhase
    {
        public string phaseName;

        [Header("Condition")]
        public string checkQuestID;
        public int requiredValue;

        [Header("Dialogue")]
        public GameObject[] lines;

        [Header("Outcome")]
        public bool updateQuest;
        public string setQuestID;
        public int newValue;
    }

    [Header("Targets")]
    public Transform player;

    [Header("Configuration")]
    public List<DialoguePhase> dialoguePhases;
    public GameObject refusalText;

    [Header("Settings")]
    public float triggerDistance = 3f;
    public float timePerMessage = 4f;

    private int currentPhaseIndex = -1;
    private int currentLineIndex = 0;
    private bool isPlayerInRange = false;
    private bool isTalking = false;
    private Coroutine activeDialogueRoutine;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
        StopAllDialogue();
    }

    void Update()
    {

        if (StoryManager.Instance == null)
        {
            if (Time.frameCount % 100 == 0) Debug.LogError("StoryManager is MISSING! Create an empty object and add StoryManager script.");
            return;
        }

        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= triggerDistance)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                CheckAndStartDialogue();
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                PauseDialogue();
            }
        }
    }

    void CheckAndStartDialogue()
    {
        DialoguePhase bestPhase = null;
        int bestPhaseIndex = -1;

        for (int i = 0; i < dialoguePhases.Count; i++)
        {
            DialoguePhase p = dialoguePhases[i];

            int currentVal = StoryManager.Instance.GetQuestStatus(p.checkQuestID);

            if (currentVal == p.requiredValue)
            {
                bestPhase = p;
                bestPhaseIndex = i;
            }
        }

        if (bestPhase != null)
        {
            if (bestPhaseIndex != currentPhaseIndex)
            {
                currentPhaseIndex = bestPhaseIndex;
                currentLineIndex = 0;
            }

            if (!isTalking && currentLineIndex < bestPhase.lines.Length)
            {
                activeDialogueRoutine = StartCoroutine(PlayPhase(bestPhase));
            }
        }
        else
        {
            if (!isTalking) activeDialogueRoutine = StartCoroutine(PlayRefusal());
        }
    }

    IEnumerator PlayPhase(DialoguePhase phase)
    {
        isTalking = true;

        for (int i = currentLineIndex; i < phase.lines.Length; i++)
        {
            currentLineIndex = i;
            if (phase.lines[i] != null) phase.lines[i].SetActive(true);
            yield return new WaitForSeconds(timePerMessage);
            if (phase.lines[i] != null) phase.lines[i].SetActive(false);
        }

        currentLineIndex++;
        isTalking = false;

        if (phase.updateQuest)
        {
            StoryManager.Instance.SetQuestStatus(phase.setQuestID, phase.newValue);
            Debug.Log("Quest Updated: " + phase.setQuestID + " is now " + phase.newValue);
        }
    }

    void PauseDialogue()
    {
        if (activeDialogueRoutine != null) StopCoroutine(activeDialogueRoutine);
        isTalking = false;

        if (currentPhaseIndex != -1 && currentPhaseIndex < dialoguePhases.Count)
        {
            var lines = dialoguePhases[currentPhaseIndex].lines;
            if (currentLineIndex < lines.Length && lines[currentLineIndex] != null)
                lines[currentLineIndex].SetActive(false);
        }
        if (refusalText != null) refusalText.SetActive(false);
    }

    void StopAllDialogue()
    {
        if (refusalText != null) refusalText.SetActive(false);
        foreach (var phase in dialoguePhases)
        {
            foreach (var line in phase.lines)
                if (line != null) line.SetActive(false);
        }
    }

    IEnumerator PlayRefusal()
    {
        isTalking = true;
        if (refusalText != null)
        {
            refusalText.SetActive(true);
            yield return new WaitForSeconds(2f);
            refusalText.SetActive(false);
        }
        isTalking = false;
    }
}