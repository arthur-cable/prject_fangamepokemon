using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

  

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    //pour ne pas faire baisser les pv instantanement mais de manière smooth
    public IEnumerator SetHPSmooth(float newHP)
    {
        float curHP = health.transform.localScale.x;
        float changAmt = curHP - newHP;

        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changAmt * Time.deltaTime;
            health.transform.localScale = new Vector3(curHP, 1f);
            yield return null;

        }
        health.transform.localScale = new Vector3(newHP, 1f);

    }
}
