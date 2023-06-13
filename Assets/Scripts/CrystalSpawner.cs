using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    private const int MINAMOUNT = 6;
    private const int MAXAMOUNT = 10;
    private const int DISTANCE = 3;
    private int rowSize = 4;
    private int columnSize = 4;


    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private Transform[] _points;

    private List<GameObject> _crystals;

    private void Start()
    {
        var points = GetRandomPointsForCrystals();
        foreach (var point in points)
        {
            var crystal = Instantiate(_crystalPrefab, transform);
            crystal.transform.position = point.position;
        }

    }
    public List<Transform> GetRandomPointsForCrystals()
    {
        var count = Random.Range(MINAMOUNT, MAXAMOUNT + 1);

        HashSet<int> selectedIndices = new HashSet<int>();
        List<Transform> selectedElements = new List<Transform>();

        while (selectedIndices.Count < count)
        {
            var randomIndex = Random.Range(0, _points.Length);

            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedElements.Add(_points[randomIndex]);
            }
        }

        return selectedElements;
    }


}
