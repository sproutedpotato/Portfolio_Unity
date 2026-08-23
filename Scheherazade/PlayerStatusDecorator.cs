using UnityEngine;
public enum Status
{
    Standard,
    Immune,
    Die
}

public class PlayerStatusDecorator : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        playerController.OnStatusChange += SetStatus;
    }

    private void OnDisable()
    {
        playerController.OnStatusChange -= SetStatus;
    }

    void Start()
    {
        SetStatus(Status.Standard);
    }

    public void SetStatus(Status newstatus)
    {
        playerInfo.status = newstatus;
    }
}
