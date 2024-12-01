using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            TextManager.Instance.ShowText(StringStorageType.End, win: true);
        }
    }
}
