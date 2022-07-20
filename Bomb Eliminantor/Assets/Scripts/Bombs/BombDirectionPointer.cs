using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BombDirectionPointer : MonoBehaviour
{
    [SerializeField] private Transform bombDirectionPointer;
    [SerializeField] private float endValueY=3;
    [SerializeField] private float duration=0.75f;
    private Tween bombDirectionPointerTween;
    private void Awake()
    {
        bombDirectionPointer.gameObject.SetActive(false);
    }
    void Start()
    {
        EnemyCounterOnScene.instance.OnAllEnemiesDied += OnAllEnemiesDied;
        Bomb.StopBombPointerAnimation += Bomb_StopBombPointerAnimation;
    }

    private void Bomb_StopBombPointerAnimation(object sender, System.EventArgs e)
    {
        bombDirectionPointer.gameObject.SetActive(false);
       
    }

    private void OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        bombDirectionPointer.gameObject.SetActive(true);
        bombDirectionPointerTween = bombDirectionPointer.DOLocalMoveY(endValueY, duration, false).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDisable()
    {
        bombDirectionPointerTween.Kill();
        EnemyCounterOnScene.instance.OnAllEnemiesDied -= OnAllEnemiesDied;
        Bomb.StopBombPointerAnimation -= Bomb_StopBombPointerAnimation;
    }

}
