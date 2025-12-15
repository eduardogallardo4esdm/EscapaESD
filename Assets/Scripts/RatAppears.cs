using UnityEngine;
using System.Collections;

public class RatAppears : MonoBehaviour
{
    [SerializeField] private GameObject rat;
    
    void Awake()
    {
        rat.SetActive(false);
    }

    public IEnumerator ActivateRatForTime()
    {
        rat.SetActive(true);
        InventoryManager.Instance.isRat = true;
        yield return new WaitForSeconds(15f);
        InventoryManager.Instance.isRat = false;
        rat.SetActive(false);
    }
}
