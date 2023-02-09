using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenController : MonoBehaviour
{

    public static void OpenWindowByScale(RectTransform rect)
    {
        rect.DOKill(true);
        rect.gameObject.SetActive(true);

        Sequence mySequence = DOTween.Sequence();
        mySequence
        .Append(rect.transform.DOScale(0f, 0))
        .Append(rect.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack, 1));
    }

    public static void CloseWindowByScale(RectTransform rect)
    {
        rect.DOKill(true);

        rect.transform.DOScale(0f, 0.15f).OnComplete(() => rect.gameObject.SetActive(false));
    }

    public static void OpenWindowByMoveAnchors(RectTransform rect,Vector3 startPos, Vector3 endPos)
    {
        rect.DOKill(true);
        rect.gameObject.SetActive(true);

        rect.DOAnchorPos(startPos, 0).OnComplete(() => rect.DOAnchorPos(endPos, 0.5f).SetEase(Ease.InOutBack));
    }

    public static void CloseWindowByMoveAnchors(RectTransform rect, Vector3 endPos)
    {
        rect.DOKill(true);

        rect.DOAnchorPos(endPos, 0.5f).SetEase(Ease.InOutBack).OnComplete(() => rect.gameObject.SetActive(false));
    }



    /*public void MoveLoop()
    {
        _objDoTween.transform.DOMove(new Vector3(2, 2, 2), 2)
          .SetOptions(true)
          .SetEase(Ease.OutQuint)
          .SetLoops(2)
          .OnComplete(OnCompleteAnimation);
    }    
    public void MoveY()
    {
        _objDoTween.transform.DOMoveY(250, 2);
    }  
    
    public void Fade()
    {
        _objDoTween.transform.DOMove(new Vector2(-5, 0), 0  );
        _objDoTween.GetComponent<SpriteRenderer>().DOFade(0, 2);
        _objDoTween.transform.DORotate(new Vector3(0, 0, 0), 0);
    }
    public void RotateAndMoveParallel()
    {
        _objDoTween.transform.DOMove(new Vector2(200, 200), 2);
        _objDoTween.transform.DORotate(new Vector3(0, 0, 180), 2);
    }
    public void RotateAndMoveSequence()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_objDoTween.transform.DOMove(new Vector2(200, 200), 2))
            .Append(_objDoTween.transform.DORotate(new Vector3(0, 0, 180), 2) );
    }
    public void MoveUpAndDown()
    {
        _objDoTween.transform.DOMoveY(250, 0.5f).SetEase(Ease.InOutSine).SetLoops(12, LoopType.Yoyo);
    }


    public void Restore()
    {
        _objDoTween.GetComponent<SpriteRenderer>().DOFade(1, 0);
    }

    public void OnCompleteAnimation()
    {
        print("completou");
    }

    public void ClosePopUp()
    {
        if(_popUpDoTween.GetComponent<RectTransform>().transform.localScale.y < 1)
        {
            _popUpDoTween.GetComponent<RectTransform>().DOScaleY(1, 1);
            _popUpDoTween.GetComponent<RectTransform>().DOScaleX(1, 1);            

        } else
        {
            _popUpDoTween.GetComponent<RectTransform>().DOScaleY(0, 1);
            _popUpDoTween.GetComponent<RectTransform>().DOScaleX(0, 1);

        }
       // _popUpDoTween.GetComponent<RectTransform>().DOMoveY(112, 2);
    }*/
}
