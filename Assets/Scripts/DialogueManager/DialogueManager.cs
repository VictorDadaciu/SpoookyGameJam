
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#pragma warning disable 0649, 0414
public class DialogueManager : MonoBehaviour
{
    [SerializeField] public bool isAutomatic;

    [SerializeField] private TextMeshProUGUI m_dialogueText;
    [SerializeField] private float m_dialogueSpeed;
    [SerializeField] private float m_dialogueStartOffset;
    [SerializeField] private Image borderImage;
    [SerializeField] public DialogueLine[] m_dialogueLines;
    [SerializeField] private Sprite[] m_dialogueSprites;
    //[SerializeField] private PlayerInteraction m_playerInteraction;
    [SerializeField] private Animator m_borderAnimator;

    [SerializeField]  private KeyCode startDialogueKey = KeyCode.F;
    [Tooltip("ReadOnly")]
    [SerializeField]  private KeyCode advanceDialogueKey = KeyCode.Space;

    private bool m_startDialogue = true;
    private bool m_nextText = true;
    private int lastSpriteIndex = 0;
    private int m_index = 0;

    private void Update()
    {
        if (m_index <= m_dialogueLines.Length - 1)
        {
            if (m_dialogueLines[m_index].spriteIndex != lastSpriteIndex)
            {
                lastSpriteIndex = m_dialogueLines[m_index].spriteIndex;
            }
        }

        if (Input.GetKeyDown(startDialogueKey) && m_startDialogue)
        {
            m_borderAnimator.SetTrigger("enter");
            // m_playerInteraction.m_controller.SetVelocity(Vector2.zero);
            // m_playerInteraction.m_canControlCharacter = false;
            // m_playerInteraction.m_animator.SetBool("isDialogActive", true);
            m_startDialogue = false;
            StartCoroutine(WaitForText());
        }
        else if (Input.GetKeyDown(advanceDialogueKey) && m_nextText)

        {
            m_nextText = false;
            NextSentence();
        }

        if (isAutomatic)
        {
            if (m_startDialogue)
            {
                m_borderAnimator.SetTrigger("enter");
                // m_playerInteraction.m_controller.SetVelocity(Vector2.zero);
                // m_playerInteraction.m_canControlCharacter = false;
                // m_playerInteraction.m_animator.SetBool("isDialogActive", true);
                m_startDialogue = false;
                StartCoroutine(WaitForText());
            }
            else if (m_nextText && Input.GetKeyDown(advanceDialogueKey))
            {
                m_nextText = false;
                NextSentence();
            }
        }
    }

    void NextSentence()
    {
        //restart
        if (m_index <= m_dialogueLines.Length - 1)
        {
            m_dialogueText.text = "";
            borderImage.sprite = m_dialogueSprites[lastSpriteIndex];

            StartCoroutine(WriteSentence());
        }
        //end of dialogue
        else
        {
            m_dialogueText.text = "";
            m_borderAnimator.SetTrigger("spriteDissapear");
            m_borderAnimator.SetTrigger("exit");
            isAutomatic = false;
            m_index = 0;
            m_startDialogue = true;

            // m_playerInteraction.m_canControlCharacter = true;
            // m_playerInteraction.m_animator.SetBool("isDialogActive", false);
        }
    }

    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(m_dialogueStartOffset);
        m_nextText = false;
        NextSentence();
    }

    IEnumerator WriteSentence()
    {
        m_borderAnimator.SetTrigger("spriteAppear");
        foreach (var character in m_dialogueLines[m_index].sentence.ToCharArray())
        {
            m_dialogueText.text += character;
            yield return new WaitForSeconds(m_dialogueSpeed);
        }

        m_index++;
        m_nextText = true;
    }
}