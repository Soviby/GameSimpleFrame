using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MyPanel
{
    public void SetData(int endValue)
    {
        this.text_p1_win.SetActive(endValue== 0 );
        this.text_p_other_win.SetActive(endValue == 1);
    }


    #region UI_AUTOCODE_RC 94c79fea49844b54a625f14b80f6e3da

    public GameObject text_p1_win;
    public GameObject text_p_other_win;

    public void CacheReference()
    {
        var rc = this.gameObject.GetComponent<ReferenceCollector>();
        this.text_p1_win = rc.GetReference<GameObject>(0); // name: text_p1_win
        this.text_p_other_win = rc.GetReference<GameObject>(1); // name: text_p_other_win
    }

    #endregion

}
