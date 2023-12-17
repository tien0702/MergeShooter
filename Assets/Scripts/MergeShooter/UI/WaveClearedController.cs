using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveClearedController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusTxt, totalTxt;

    Reward[] rewards;

    private void Awake()
    {
        rewards = new RewardDAL().GetRewards();
    }

    private void OnEnable()
    {
        bonusTxt.text = rewards[UserData.Instance.WaveIndex].BonusEarnings.ToString();
        totalTxt.text = rewards[UserData.Instance.WaveIndex].TotalEarnings.ToString();
    }

    public void ClaimReward()
    {
        UserData.Instance.AddCoin(rewards[UserData.Instance.WaveIndex].TotalEarnings);
    }   

    public void ClaimX2Reward()
    {
        UserData.Instance.AddCoin(rewards[UserData.Instance.WaveIndex].TotalEarnings * 2);
    }
}
