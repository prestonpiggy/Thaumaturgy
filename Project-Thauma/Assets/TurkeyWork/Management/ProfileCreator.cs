﻿using System.Collections;
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
            TryCreateProfile (ProfileNameInput.text);
        }

        public void TryCreateProfile (string name) {
            ProfileNameInput.interactable = false;
            CreateProfileButton.interactable = false;

            if (ProfileManager.CreateProfile (name)) {
                OnProfileCreated.Invoke ();
            } else {
                OnProfileCreationFailed.Invoke ();
            }
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