using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    public LeanTweenType leanTweenType;
    public LeanTweenType leanTweenType2;
    public GameObject levelExit, dummy, levelExitGraphic;

    public float moveSpeed = 0.5f;
    
    public void Animate()
    {
        levelExit.SetActive(true);
        LeanTween.scale(levelExitGraphic, new Vector3(1.5f,1.5f,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(levelExitGraphic, new Vector3(1,1,1), 0.5f).setDelay(0.3f).setIgnoreTimeScale(true);
        LeanTween.scale(dummy, new Vector3(1,1.3f,1), 0.5f).setDelay(1f).setIgnoreTimeScale(true);
        LeanTween.moveY(dummy, 5, moveSpeed).setDelay(2f).setEase(leanTweenType2).setIgnoreTimeScale(true);
    }

    private void OnTriggerEnter2D() 
    {
        this.gameObject.SetActive(false);
        LevelManagerRL.Instance.ChooseNewMap();
    }
    
}
