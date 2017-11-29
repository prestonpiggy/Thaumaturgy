using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace TurkeyWork.Management {

    public class ProfileCreator : MonoBehaviour {

        public TMPro.TMP_InputField ProfileNameInput;
        public Button CreateProfileButton;

        public UnityEvent OnProfileCreated;
        public UnityEvent OnProfileCreationFailed;

        public void TryCreateProfile () {

        }

        [Button]
        void RenameGameObjects () {
            if (CreateProfileButton != null)
                CreateProfileButton.gameObject.name = "Button - CREATE PROFILE";
            if (ProfileNameInput != null)
                ProfileNameInput.gameObject.name = "Input Field - PROFILE NAME INPUT";
        }


    }

}