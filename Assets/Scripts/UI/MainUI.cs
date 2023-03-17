using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] 
        private UIDocument _uiDoc;

        private Label _lblScore;

        private void Awake()
        {
            _lblScore = _uiDoc.rootVisualElement.Q<Label>("lblScore");
        }
        
        public void UpdateScore(IScore score) => _lblScore.text = $"Score: {score.ScorePoints}";
    }   
}
