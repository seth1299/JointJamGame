using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class conversationTemplate : MonoBehaviour
{
    public NPCConversation myConversation;
    ConversationManager.Instance.StartConversation(myConversation);        
    
}
