﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Client.Presenters
{
    public class ModelAnimator
    {
        private List<UniTask> animations = new();
        private Queue<Func<UniTask>> animationsQueue = new();
            
        public async UniTask Animate(UniTask uniTask)
        {
            animations.Add(uniTask);
            await uniTask;
            if (animationsQueue.TryDequeue(out var result))
            {
                await Animate(result());
                animations.Remove(uniTask);
            }
            else
            {
                animations.Remove(uniTask);
            }
        }

        public async UniTask EnqueueAnimation(Func<UniTask> animationFunc)
        {
            if (CheckAnimationsListEmpty())
            {
                await Animate(animationFunc());
            }
            else
            {
                animationsQueue.Enqueue(animationFunc);
            }
        }

        public async UniTask AwaitAllAnimationsEnd()
        {
            await UniTask.WaitUntil(CheckAnimationsListEmpty);
        }
        
        private bool CheckAnimationsListEmpty()
        {
            return animations.Count == 0 && animationsQueue.Count == 0;
        }
    }
}