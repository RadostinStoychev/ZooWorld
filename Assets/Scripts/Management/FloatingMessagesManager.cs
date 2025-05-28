using System;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Class handling floating messages logic.
    /// </summary>
    public class FloatingMessagesManager : MonoBehaviour
    {
        private const string FloatingMessageObjectNameTemplate = "Message_{0}";
        
        [Header("Message Settings")]
        [SerializeField] 
        private float messageDuration = 3f;
        [SerializeField] 
        private int fontSize = 10;
        [SerializeField] 
        private Color messageColor = Color.green;
    
        private void Start()
        {
            ZooActions.OnMessageSpawned += CreateFloatingMessage;
        }
    
        private void OnDestroy()
        {
            ZooActions.OnMessageSpawned -= CreateFloatingMessage;
        }
    
        private void CreateFloatingMessage(Vector3 position, string message)
        {
            GameObject messageObject = new GameObject(String.Format(FloatingMessageObjectNameTemplate, message));
            
            messageObject.transform.position = position + Vector3.down * 0.5f;
            messageObject.transform.rotation = Quaternion.Euler(90, 0, 0);

            TextMesh textMesh = messageObject.AddComponent<TextMesh>();
            textMesh.text = message;
            textMesh.fontSize = fontSize;
            textMesh.color = messageColor;
            textMesh.anchor = TextAnchor.MiddleCenter;

            Destroy(messageObject, messageDuration);
        }
    }
}
