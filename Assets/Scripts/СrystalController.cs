using UnityEngine;

public class СrystalController : MonoBehaviour
{
    private const int CRYSTALAMOUNT = 8;
    private int _availableCrystalAmount;
    private CrystalShardSpawner _shardSpawner;

    private int AvailableCrystals
    {
        get
        {
            return _availableCrystalAmount;
        }
        set
        {
            _availableCrystalAmount = value;
            if (_availableCrystalAmount <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        _shardSpawner = CrystalShardSpawner.Instance;
        _availableCrystalAmount = CRYSTALAMOUNT;
    }
    public void GetDamageByDrop(float distase)
    {
        int crystalAmount = 0;
        switch (distase)
        {
            case < 2:
                return;
                break;
            case < 5:
                crystalAmount = 2;
                break;
            case < 10:
                crystalAmount = 3;
                break;
            case < 17:
                crystalAmount = 5;
                break;
        }
        if (AvailableCrystals < crystalAmount)
        {
            crystalAmount = AvailableCrystals;
        }
        _shardSpawner.SpawnShards(crystalAmount, transform);

        AvailableCrystals -= crystalAmount;



    }

}
