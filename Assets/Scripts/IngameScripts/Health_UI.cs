using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    public GameObject health;
    public List<Image> healths;
    public PlayerController playerController;

    void Start()
    {
        for(int i = 0; i < playerController.Health; i++)
        {
            GameObject Health_obj = Instantiate(health,this.transform);
            healths.Add(Health_obj.GetComponent<Image>());
        }
    }

    public void Hit_Hearts()
    {
        int heartFill = playerController.Health;

        foreach(Image i in healths)
        {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }
    }

    public void Use_Boom()
    {
        int BoomFill = playerController.Boom;

        foreach(Image i in healths)
        {
            i.fillAmount = BoomFill;
            BoomFill -= 1;
        }
    }
}
