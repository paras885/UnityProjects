using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public Text[] buttonLists;
	private string PlayerSide;
	public int BoardLimit; 
	public GameObject gameOverPanel;
	public Text gameOverText;

	void Awake() {
		SetControllerRefToButtons ();
		gameOverPanel.SetActive (false);
		PlayerSide = "X";
		BoardLimit = 3;
	}

	public void SetControllerRefToButtons() {
		for (int i = 0; i < buttonLists.Length; ++i) {
			buttonLists [i].GetComponentInParent< GridSpace >().SetGameControllerRef(this);
			buttonLists[i].text = "";
		}
	}

	public void ResetBoard() {
		for (int i = 0; i < buttonLists.Length; ++i) {
			buttonLists [i].GetComponentInParent<Button> ().interactable = true;
			buttonLists [i].text = "";
		}
	}

	public string GetPlayerSide() {
		return PlayerSide;
	}

	private bool WinCondition(List<List<string> > board, string current_player) {
		//If Continous in Row
		int cnt;
		for (int i = 0; i < board.Count; ++i) {
			cnt = 0;
			for (int j = 0; j < board [i].Count; ++j) {
				if (current_player == board [i] [j])
					cnt++;
			}
			if (cnt == board[i].Count)
				return true;
		}

		//For Col
		for (int i = 0; i < board[0].Count; ++i) {
			cnt = 0;
			for (int j = 0; j < board.Count; ++j) {
				if (current_player == board [j] [i])
					cnt++;
			}
			if (cnt == board.Count)
				return true;
		}

		//Digonal
		cnt = 0;
		for (int i = 0; i < board.Count; ++i) {
			if(board[i][i] == current_player) cnt++;
		}

		if(cnt == board.Count) return true;

		cnt = 0;
		for(int i = 0; i < board.Count; ++i) {
			if(current_player == board[i][(board.Count - 1) - i])cnt++;
		}

		if(cnt == board.Count) return true;
		return false;
	}

	private void GameOver(string result) {
		for (int i = 0; i < buttonLists.Length; ++i) {
			buttonLists[i].GetComponentInParent<Button>().interactable = false;
		}
		gameOverText.text = result;
		gameOverPanel.SetActive (true);
	}

	private bool CountMoves(List<List<string> > board) {
		for (int i = 0; i < board.Count; ++i) {
			for (int j = 0; j < board [i].Count; ++j) {
				if (board [i] [j] == "X" || board [i] [j] == "O")
					continue;
				return true;
			}
		}
		return false;
	}

	private string GenerateResultString(bool is_current_win, string current_player) {
		if (is_current_win) {
			return current_player + " Won!!";
		}
		return " Draw!!";
	}

	public void EndTurn () {
		
		List<List<string> > board = new List<List<string> >();
		for (int i = 0; i < BoardLimit; ++i) {
			board.Add (new List<string> ());
		}

		for (int i = 0; i < buttonLists.Length; ++i) {
			int row = i / 3;
			board [row].Add (buttonLists [i].text);
		}

		bool is_current_win = WinCondition (board, PlayerSide);
		bool has_more_move = CountMoves (board);
		if (is_current_win || !has_more_move) {
			string result_string = GenerateResultString (is_current_win,PlayerSide);
			GameOver (result_string);
		}

	    PlayerSide = (PlayerSide == "X") ? "O" : "X";
	}
}
