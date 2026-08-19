using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txtResult;
    public TextMeshProUGUI txtEnemyChoice;

    private string[] choices = { "Búa", "Bao", "Kéo" };

    public void PlayerChoose(int playerChoice)
    {
        // Thêm dòng này: Bắt buộc Unity bỏ chọn nút vừa bấm
        EventSystem.current.SetSelectedGameObject(null);

        // --- Phần code bên dưới giữ nguyên như cũ ---
        int enemyChoice = Random.Range(0, 3);
        txtEnemyChoice.text = "Máy chọn: " + choices[enemyChoice];

        if (playerChoice == enemyChoice)
        {
            txtResult.text = "Kết quả: HÒA!";
        }
        else if ((playerChoice == 0 && enemyChoice == 2) ||
                 (playerChoice == 1 && enemyChoice == 0) ||
                 (playerChoice == 2 && enemyChoice == 1))
        {
            txtResult.text = "Kết quả: BẠN THẮNG!";
        }
        else
        {
            txtResult.text = "Kết quả: BẠN THUA!";
        }
    }
}