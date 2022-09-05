using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{

    public GameObject PortalGun;
    public GameObject Gun;
    private int NumberGun = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            print("doing stuff");
            NumberGun = NumberGun == 1 ? 0 : 1;

            switch(NumberGun)
            {
                case 0:
                    PortalGun.transform.gameObject.SetActive(true);
                    Gun.transform.gameObject.SetActive(false);
                    break;
                case 1:
                    PortalGun.transform.gameObject.SetActive(false);
                    Gun.transform.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
