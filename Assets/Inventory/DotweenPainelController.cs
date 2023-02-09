using UnityEngine;
using DG.Tweening;

public class DotweenPainelController : MonoBehaviour
{
    public RectTransform _uiPanel;
    [SerializeField] bool _isHorizontal;
    [SerializeField] float _targetDistanci;
    [SerializeField] float _positionInitalY;
    [SerializeField] float _positionInitalX;


    private void Start()
    {

        _uiPanel.DOAnchorPosX(_positionInitalX, _positionInitalY).SetEase(Ease.OutBounce);

    }

    public void Minimize()
    {
        _uiPanel.DOComplete();
        if (_isHorizontal)
        {
            _uiPanel.DOAnchorPosX(_targetDistanci + _positionInitalX, 1).SetEase(Ease.OutBounce);

        }
        else
        {
            _uiPanel.DOAnchorPosY(_targetDistanci, 1).SetEase(Ease.OutBounce);
        }
    }
    public void Maximixe()
    {
        _uiPanel.DOComplete();
        if(_isHorizontal)
        {
            _uiPanel.DOAnchorPosX(_positionInitalX, 1).SetEase(Ease.OutBounce);
        } else
        {
            _uiPanel.DOAnchorPosY(0, 1).SetEase(Ease.OutBounce);
        }
    }
}
