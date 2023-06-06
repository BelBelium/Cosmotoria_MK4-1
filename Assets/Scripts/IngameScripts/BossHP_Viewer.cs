using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP_Viewer : MonoBehaviour
{
    [SerializeField]
    private BossPatterns Boss_HP;
    private Slider BossHP_Slider;

    // Start is called before the first frame update
    void Start()
    {
        BossHP_Slider = GetComponent<Slider>();
        Boss_HP = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPatterns>();
    }

    // Update is called once per frame
    void Update()
    {
        BossHP_Slider.value = Boss_HP.boss_HP / Boss_HP.hp_origin;
    }
}
