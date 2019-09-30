using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RestoManager : MonoBehaviour, IGameManager {
	public ManagerStatus Status {get; private set;}

	private Dictionary<string, int> _items;
	public string EquippedItem {get; private set;}

	public void Startup() {
		Debug.Log("Inventory manager starting...");

		_items = new Dictionary<string, int>();

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		Status = ManagerStatus.Started;
	}

	private void DisplayItems() {
		string itemDisplay = "Items: ";
		foreach (KeyValuePair<string, int> item in _items) {
			itemDisplay += item.Key + "(" + item.Value + ") ";
		}
		Debug.Log(itemDisplay);
	}

	public void AddItem(string name) {
		if (_items.ContainsKey(name)) {
			_items[name] += 1;
		} else {
			_items[name] = 1;
		}

		DisplayItems();
	}
	
	public bool ConsumeItem(string name) {
		if (_items.ContainsKey(name)) {
			_items[name]--;
			if (_items[name] == 0) {
				_items.Remove(name);
			}
		} else {
			Debug.Log("cannot consume " + name);
			return false;
		}
		
		DisplayItems();
		return true;
	}

	public List<string> GetItemList() {
		List<string> list = new List<string>(_items.Keys);
		return list;
	}

	public int GetItemCount(string name) {
		if (_items.ContainsKey(name)) {
			return _items[name];
		}
		return 0;
	}

	public bool EquipItem(string name) {
		if (_items.ContainsKey(name) && EquippedItem != name) {
			EquippedItem = name;
			Debug.Log("Equipped " + name);
			return true;
		}

		EquippedItem = null;
		Debug.Log("Unequipped");
		return false;
	}
}
