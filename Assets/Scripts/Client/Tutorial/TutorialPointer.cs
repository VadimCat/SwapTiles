using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ji2Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Tutorial
{
    public class TutorialPointer : MonoBehaviour
    {
        [SerializeField] private Image pointer;
        [SerializeField] private TMP_Text tooltip;

        public async UniTask PlayClickAnimation(Vector3 pos, CancellationToken cancellationToken)
        {
            pointer.transform.position = pos;
            pointer.color = Color.white;
            pointer.transform.localScale = Vector3.one;
            await pointer.transform.DoPulseScale(.85f, .5f, gameObject)
                .AwaitForComplete(cancellationToken: cancellationToken);

            Hide();
        }

        public void Hide()
        {
            pointer.color = new Color(0, 0, 0, 0);
        }

        public void ShowTooltip()
        {
            tooltip.DOFade(1, .5f);
        }
        
        public void HideTooltip()
        {
            tooltip.DOFade(0, .5f);
        }
    }
}