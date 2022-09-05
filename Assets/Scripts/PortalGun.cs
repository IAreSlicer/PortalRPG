using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public Portal Red;
    public Portal Blue;
    public GameObject full;
    public GameObject redFull;
    public GameObject blueFull;
    public GameObject Empty;
    public AudioSource audio;
    public AudioSource noShoot;

    private void CrossUpdate()
    {
        if(Blue.transform.gameObject.activeSelf && Red.transform.gameObject.activeSelf)
        {
            full.transform.gameObject.SetActive(true);
            redFull.transform.gameObject.SetActive(false);
            blueFull.transform.gameObject.SetActive(false);
            Empty.transform.gameObject.SetActive(false);
        }
        else if (Blue.transform.gameObject.activeSelf)
        {
            full.transform.gameObject.SetActive(false);
            redFull.transform.gameObject.SetActive(false);
            blueFull.transform.gameObject.SetActive(true);
            Empty.transform.gameObject.SetActive(false);
        }
        else if (Red.transform.gameObject.activeSelf)
        {
            full.transform.gameObject.SetActive(false);
            redFull.transform.gameObject.SetActive(true);
            blueFull.transform.gameObject.SetActive(false);
            Empty.transform.gameObject.SetActive(false);
        }
        else
        {
            full.transform.gameObject.SetActive(false);
            redFull.transform.gameObject.SetActive(false);
            blueFull.transform.gameObject.SetActive(false);
            Empty.transform.gameObject.SetActive(true);
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.layer < 10) 
                {
                    noShoot.Play(0);
                    return;
                } 

                audio.Play(0);
                
                if (Input.GetMouseButtonDown(0))
                {
                    Red.transform.rotation = Quaternion.LookRotation(hit.normal);
                    if (hit.collider.gameObject.layer == 11) 
                    {
                        Red.transform.position = Blue.transform.position;
                        Blue.transform.gameObject.SetActive(false);
                        Red.GetComponentInChildren<MeshRenderer>().enabled = false;
                        Red.GetComponentInChildren<Collider>().enabled = false;       
                    }
                    else if (hit.collider.gameObject.layer == 12) return;
                    else 
                    {
                        Red.transform.gameObject.SetActive(true);
                        Red.transform.position = hit.point + Red.transform.forward * 0.6f;

                        if (Red.transform.position.y < 1.8f && Red.transform.rotation.y % 90 != 0)  
                        {
                            Vector3 vec = Red.transform.position;
                            vec.y = 1.8f;
                            Red.transform.position = vec;
                        }

                        if (Blue.transform.gameObject.activeSelf)
                        {
                            Red.GetComponentInChildren<MeshRenderer>().enabled = true;
                            Red.GetComponentInChildren<Collider>().enabled = true;
                            Blue.GetComponentInChildren<MeshRenderer>().enabled = true;
                            Blue.GetComponentInChildren<Collider>().enabled = true;
                        }
                    }
                }
                else
                {
                    Blue.transform.rotation = Quaternion.LookRotation(hit.normal);

                    if (hit.collider.gameObject.layer == 12)
                    {
                        Blue.transform.position = Red.transform.position;
                        Red.transform.gameObject.SetActive(false);
                        Blue.GetComponentInChildren<MeshRenderer>().enabled = false;
                        Blue.GetComponentInChildren<Collider>().enabled = false;
                    }
                    else if (hit.collider.gameObject.layer == 11) return;
                    else 
                    {
                        Blue.transform.gameObject.SetActive(true);
                        Blue.transform.position = hit.point + Blue.transform.forward * 0.6f;

                        if (Blue.transform.position.y < 1.8f && Blue.transform.rotation.y % 90 != 0) 
                        {
                            Vector3 vec = Blue.transform.position;
                            vec.y = 1.8f;
                            Blue.transform.position = vec;
                        }

                        if(Red.transform.gameObject.activeSelf)
                        {
                            Blue.GetComponentInChildren<MeshRenderer>().enabled = true;
                            Blue.GetComponentInChildren<Collider>().enabled = true;
                            Red.GetComponentInChildren<MeshRenderer>().enabled = true;
                            Red.GetComponentInChildren<Collider>().enabled = true;
                        }
                    }
                }
            }
            else
            {
                noShoot.Play(0);
            }
        }
        CrossUpdate();
    }
}
