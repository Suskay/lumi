using UnityEngine;

namespace DefaultNamespace
{
    public class OpenMenuButton : MonoBehaviour
    {
        
        public GameObject confirmationDialog; // Reference to the confirmation dialog UI element

        void Start()
        {
            // Find the ConfirmationDialog
            GameObject confirmationDialogObject = GameObject.Find("ConfirmReturnToMainMenuDialog");

            // Assign the GameObject to the public variable
            confirmationDialog = confirmationDialogObject;
        }
    }
}