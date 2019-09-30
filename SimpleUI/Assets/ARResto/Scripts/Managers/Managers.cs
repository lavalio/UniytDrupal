using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FancyScrollView.MonResto;

[RequireComponent(typeof(GetPoints))]
//[RequireComponent(typeof(GetSprite))]

public class Managers : MonoBehaviour {

    [SerializeField] ScrollView scrollView = default;
    public static GetPoints Points { get; private set;}
	//public static GetSprite Sprite {get; private set;}

	private List<IGameManager> _startSequence;
	
	void Awake() {
		Points = GetComponent<GetPoints>();
		//Sprite = GetComponent<GetSprite>();

        _startSequence = new List<IGameManager>
        {
            Points,
            //Sprite
        };

        StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		

		foreach (IGameManager manager in _startSequence) {
			manager.Startup();
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach (IGameManager manager in _startSequence) {
				if (manager.Status == ManagerStatus.Started) {
					numReady++;
				}
			}

			if (numReady > lastReady)
				Debug.Log("Progress: " + numReady + "/" + numModules);

			yield return null;
		}


        scrollView.UpdateData(GetPoints.Items);
        scrollView.SelectCell(0);

        Debug.Log("All managers started up");
	}
}
