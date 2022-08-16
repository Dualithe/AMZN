using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarIcon : MonoBehaviour
{
    [SerializeField] private Image backImg;
    [SerializeField] private Image starImg;

    public void PlayAnimation() {
        starImg.transform.localScale = Vector3.one * 2.0f;
        starImg.color = new Color(1f, 1f, 1f, 0f);
        starImg.transform.localRotation = Quaternion.Euler(0f, 0f, 0.0f);

        DOTween.To(() => starImg.transform.localScale, x => starImg.transform.localScale = x, Vector3.one, 1.2f);
        DOTween.To(() => starImg.color, x => starImg.color = x, new Color(1f, 1f, 1f, 1f), 1.2f);
        starImg.transform.DOLocalRotate(new Vector3(0f, 0f, 360f), 1.2f, RotateMode.FastBeyond360);
    }

    public void Reset() {
        starImg.color = new Color(1f, 1f, 1f, 0f);
    }

    private void Start() {
        Reset();
    }
}
