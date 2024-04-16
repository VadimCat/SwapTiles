﻿using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ji2.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;

namespace SwapTiles.UI.Screens
{
    public class LevelCompletedScreen : BaseScreen
    {
        [SerializeField] private Button nextButton;
        [SerializeField] private Button retryButton;

        [SerializeField] private Image levelImageResult;
        [SerializeField] private Image animateResult;
        [SerializeField] private Image border;

        public event Action ClickNext;
        public event Action ClickRetry;

        private void Awake()
        {
            AnimateLevelResultImage();

            AnimateNextButton();

            retryButton.onClick.AddListener(FireRetry);
            nextButton.onClick.AddListener(FireNext);
        }

        private async void AnimateLevelResultImage()
        {
            border.color = new Color(1, 1, 1, 0);
            await border.DOFade(1, .25f).AwaitForComplete();
            
            animateResult.transform.DOMoveY(-0.08f, 1.4f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        private void AnimateNextButton()
        {
            nextButton.image.DOFade(1, 1f);
        }

        public void SetLevelResult(Sprite levelResult)
        {
            this.levelImageResult.sprite = levelResult;
        }

        private async void FireNext()
        {
            await nextButton.transform.DOScale(0.9f, 0.1f).AwaitForComplete();
            Complete();
        }

        private void FireRetry()
        {
            ClickRetry?.Invoke();
        }

        private void Complete()
        {
            ClickNext?.Invoke();
        }

        private void OnDestroy()
        {
            ClickRetry = null;
            ClickNext = null;
        }
    }
}