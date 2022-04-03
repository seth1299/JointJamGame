using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class conversationTemplate : MonoBehaviour
{

    public NPCConversation myConversation;

    private void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)){
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
