using UnityEngine;

public class CrystalShardSpawner : MonoBehaviour
{
    private const float maxShardDistanse = 5;
    private const float minShardDistanse = 3;
    [SerializeField] GameObject _shardPrefab;
    private ObjectPooler _objectPooler;
    private int _shardIndex;

    public static CrystalShardSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _shardIndex = _objectPooler.AddObject(_shardPrefab, 7);
    }

    public void SpawnShards(int amount, Transform bigShardTransform)
    {
        var shards = _objectPooler.GetAmountOfPulledObjects(_shardIndex, amount);
        foreach (var shard in shards)
        {
            shard.transform.localPosition = bigShardTransform.localPosition + new Vector3(Random.Range(-maxShardDistanse, maxShardDistanse),
                                                                                          Random.Range(minShardDistanse, maxShardDistanse),
                                                                                          Random.Range(-maxShardDistanse, maxShardDistanse));
            shard.SetActive(true);
        }
    }
}
