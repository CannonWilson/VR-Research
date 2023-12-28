    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
     
    public class Blinking : MonoBehaviour
    {
        public Transform testBlink;

        public float waitTime = 0.5f;
        public bool shouldBlink = false;

        private bool isBlinkActive = false;

        private float timeSinceLastBlink = 0.0f;

        private void Update() {
            if (shouldBlink) {
                if (timeSinceLastBlink >= waitTime) {
                    ToggleBlink();
                    timeSinceLastBlink -= waitTime;
                }
                else {
                    timeSinceLastBlink += Time.deltaTime;
                }
            }
        }

        private void ToggleBlink() {
            testBlink.gameObject.SetActive(!isBlinkActive);
            isBlinkActive = !isBlinkActive;
        }
     
    }
