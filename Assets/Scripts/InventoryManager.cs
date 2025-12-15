using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public bool hasEmptyBottle=false;
    public bool hasWaterBottle=false;
    public bool waterBottleUsed=false;
    public bool hasComputer=false;
    public bool hasDocuments=false;
    public bool documentsUsed=false;
    public bool hasSewingMachine=false;
    public bool sewingMachineUsed=false;
    public bool hasLockpick=false;
    public bool lockipckUsed=false;
    public bool hasTeacherCard=false;
    public bool hasKnowledge=false;
    public bool hasPhotoshop=false;
    public bool hasTried=false;
    public bool hasTried2=false;
    public bool hasGone =false;
    public bool isRat= false;
    public bool sewingMachineEnabled = false;

    private void Awake()
    {
        if(Instance == null)
        { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void EmptyBottle()
    {
        hasEmptyBottle=true;
        UIManager.Instance.Items("GetEmptyBottle");
    }
    public void WaterBottle()
    {
        hasWaterBottle=true;
        UIManager.Instance.Items("GetWaterBottle");
    }
    public void WaterBottleUsed()
    {
        waterBottleUsed=true;
        UIManager.Instance.Items("UseWaterBottle");
    }
    public void Computer()
    {
        hasComputer = true;
        UIManager.Instance.Items("GetComputer");
    }
    public void Documents()
    {
        hasDocuments = true;
        UIManager.Instance.Items("GetDocuments");
    }
    public void DocumentsUsed()
    {
        documentsUsed=true;
        UIManager.Instance.Items("UseDocuments");
    }
    public void SewingMachine()
    {
        hasSewingMachine=true;
        UIManager.Instance.Items("GetNeedle");
    }
    public void SewingMachineUsed()
    {
        sewingMachineUsed=true;
        UIManager.Instance.Items("UseNeedle");
    }
    public void Lockpick()
    {
        hasLockpick=true;
        UIManager.Instance.Items("GetLockpick");
    }
    public void LockpickUsed()
    {
        lockipckUsed=true;
        UIManager.Instance.Items("UseLockpick");
    }
    public void TeacherCard()
    {
        hasTeacherCard=true;
        UIManager.Instance.Items("GetCard");
    }
}
