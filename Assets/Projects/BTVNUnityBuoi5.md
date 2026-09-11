**Bài cũ**

### **1\. Trigger (Xuyên thấu)**

Trigger xảy ra khi Collider 2D được tick chọn **"Is Trigger"**. Lúc này, vật thể sẽ trở nên "vô hình" về mặt vật lý cản trở (các vật khác có thể đi xuyên qua), nhưng Unity vẫn ghi nhận sự kiện khi có vật chạm vào.

> * **Ứng dụng:** Vùng phát hiện kẻ địch, nhặt xu (coin), cổng dịch chuyển (portal).  
> * **Các hàm bắt sự kiện:** OnTriggerEnter2D, OnTriggerStay2D, OnTriggerExit2D.

### **2\. Raycast 2D**

Raycast giống như việc bắn ra một tia laser vô hình từ một điểm theo một hướng nhất định. Nếu tia này chạm vào một Collider nào đó, nó sẽ trả về thông tin của vật thể đó (khoảng cách, điểm chạm, tên vật thể...).

> * **Ứng dụng:** Bắn súng hit-scan, kiểm tra nhân vật có đang chạm đất không (Ground check), tầm nhìn của AI.

// Ví dụ bắn một tia xuống dưới để kiểm tra mặt đất  
RaycastHit2D hit \= Physics2D.Raycast(transform.position, Vector2.down, 1.5f);

if (hit.collider \!= null)  
{  
    Debug.Log("Chạm vào: " \+ hit.collider.name);  
}

### **3\. Layer Mask**

Layer Mask được dùng để lọc và chỉ định các Layer cụ thể mà hệ thống vật lý (như Raycast hoặc va chạm) nên tương tác hoặc bỏ qua. Điều này giúp tối ưu hiệu suất và tránh lỗi (ví dụ: tia raycast của nhân vật tự bắn trúng chính nhân vật).

public LayerMask groundLayer;

// Chỉ trả về true nếu tia Ray chạm vào đối tượng thuộc groundLayer  
RaycastHit2D hit \= Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);

## **4\. Các cách di chuyển nhân vật**

| Phương pháp | Cơ chế & Ưu/Nhược điểm |
| :---- | :---- |
| **Transform.Translate** hoặc đổi **Transform.position** | Di chuyển trực tiếp toạ độ mà không thông qua hệ thống vật lý. *\- Ưu điểm:* Code đơn giản, phản hồi tức thì. *\- Nhược điểm:* Dễ gây lỗi xuyên tường, giật lag khi va chạm vì nó "dịch chuyển tức thời" chứ không "đẩy" nhân vật. Khuyên dùng cho vật thể Kinematic. |
| **Rigidbody2D.velocity** | Gán trực tiếp vận tốc cho Rigidbody. *\- Ưu điểm:* Tương tác tốt với hệ thống vật lý, không bị xuyên tường, kiểm soát tốc độ chính xác (rất hợp cho game Platformer). *\- Nhược điểm:* Sẽ ghi đè lên các lực khác (như lực đẩy, lực nảy) nếu không xử lý khéo. |
| **Rigidbody2D.AddForce** | Thêm một lực tác động lên nhân vật. *\- Ưu điểm:* Chuyển động rất thực tế, có quán tính, gia tốc. Tuyệt vời cho game lái xe hoặc nhảy (Jump). *\- Nhược điểm:* Khó kiểm soát để nhân vật dừng lại ngay lập tức hoặc duy trì một vận tốc không đổi. |

**Bài mới**

## I. Sự kiện (Event) trong C# & Unity

Cơ chế sự kiện hoạt động theo mô hình Observer (Publisher - Subscriber), cho phép một đối tượng thông báo cho các đối tượng khác khi có hành động xảy ra mà không cần liên kết chặt chẽ (tight coupling) với nhau.

---

### 1. Delegate

Delegate là một kiểu dữ liệu tham chiếu đại diện cho các hàm có cùng danh sách tham số và kiểu trả về. Nó đóng vai trò như một con trỏ hàm an toàn (type-safe).

```csharp
using UnityEngine;

public class DelegateExample : MonoBehaviour
{
    public delegate void DamageHandler(int amount);
    public DamageHandler onTakeDamage;

    private void Start()
    {
        onTakeDamage = ReduceHealth;
        onTakeDamage += PlayHitEffect;

        onTakeDamage.Invoke(25);
    }

    private void ReduceHealth(int amount)
    {
        Debug.Log("Tru mau: " + amount);
    }

    private void PlayHitEffect(int amount)
    {
        Debug.Log("Hieu ung trung don");
    }
}
```

---

### 2. Action

`Action` là một delegate generic được C# định nghĩa sẵn thuộc namespace `System`. `Action` đại diện cho các phương thức có kiểu trả về là `void` và có thể nhận từ 0 đến 16 tham số (ví dụ: `Action`, `Action<int>`, `Action<string, float>`), giúp loại bỏ bước khai báo delegate thủ công.

```csharp
using System;
using UnityEngine;

public class ActionExample : MonoBehaviour
{
    public static Action<int> OnCoinCollected;

    private void Start()
    {
        OnCoinCollected += UpdateCoinUI;
        
        OnCoinCollected?.Invoke(10);
    }

    private void OnDestroy()
    {
        OnCoinCollected -= UpdateCoinUI;
    }

    private void UpdateCoinUI(int count)
    {
        Debug.Log("So coin moi: " + count);
    }
}
```

---

### 3. UnityEvent

`UnityEvent` là lớp sự kiện do Unity xây dựng sẵn (thuộc namespace `UnityEngine.Events`).

* **Điểm nổi bật:** Có thể hiển thị trực tiếp lên bảng **Inspector**, cho phép kéo-thả các GameObject và chọn hàm cần gọi ngay trên giao diện mà không cần can thiệp code (rất hữu ích cho Level Designer và UI hệ thống).
* **Đa năng:** Có thể truyền tham số bằng cách kế thừa `UnityEvent<T>`.
* **Hiệu năng:** Chạy chậm hơn thuần `Action`/`delegate` của C# do chi phí serialize và reflection, nhưng mang lại tính trực quan cao.

```csharp
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExample : MonoBehaviour
{
    public UnityEvent onPlayerDeath;
    public UnityEvent<int, string> onScoreChanged;

    private void OnEnable()
    {
        onPlayerDeath.AddListener(ShowGameOverUI);
        onScoreChanged.AddListener(UpdateScoreBoard);
    }

    private void OnDisable()
    {
        onPlayerDeath.RemoveListener(ShowGameOverUI);
        onScoreChanged.RemoveListener(UpdateScoreBoard);
    }

    private void Start()
    {
        onPlayerDeath?.Invoke();
        onScoreChanged?.Invoke(100, "Bonus");
    }

    private void ShowGameOverUI()
    {
        Debug.Log("Hien thi man hinh Game Over");
    }

    private void UpdateScoreBoard(int points, string reason)
    {
        Debug.Log(reason + ": +" + points);
    }
}
```

---

### 4. Cách đăng ký và phát sự kiện (Invoke)

| Loại | Cú pháp đăng ký | Cú pháp hủy đăng ký | Cú pháp phát sự kiện (Invoke) |
|---|---|---|---|
| **Delegate** | `event += MethodName;` | `event -= MethodName;` | `event?.Invoke(args);` |
| **Action** | `action += MethodName;` | `action -= MethodName;` | `action?.Invoke(args);` |
| **UnityEvent** | `unityEvent.AddListener(MethodName);` | `unityEvent.RemoveListener(MethodName);` | `unityEvent.Invoke(args);` |

---

## II. Coroutine trong Unity

### 1. Khái niệm luồng Coroutine

Coroutine **không** phải là một luồng (thread) chạy đa luồng (multi-threading). Coroutine chạy hoàn toàn trên **Main Thread** của Unity.

Coroutine bản chất là một hàm có khả năng tạm dừng việc thực thi tại vị trí lệnh `yield return`, trả quyền kiểm soát lại cho engine Unity tiếp tục xử lý các tác vụ khác (như Render, Update của các script khác), sau đó tiếp tục thực thi câu lệnh tiếp theo ngay tại điểm dừng ở các khung hình kế tiếp khi điều kiện `yield` thỏa mãn.

### 2. Các lệnh yield thường gặp

* `yield return null`: Tạm dừng hàm và tiếp tục chạy ở khung hình (frame) kế tiếp.
* `yield return new WaitForSeconds(float time)`: Tạm dừng hàm trong một khoảng thời gian thực tế (tính theo giây, bị ảnh hưởng bởi `Time.timeScale`).

### 3. Khởi chạy và dừng Coroutine

* `StartCoroutine(...)`: Khởi tạo và chạy coroutine.
* `StopCoroutine(...)`: Dừng coroutine cụ thể (bằng biến tham chiếu `Coroutine` hoặc tên chuỗi).
* `StopAllCoroutines()`: Dừng toàn bộ các coroutine đang chạy trên script hiện tại.
* Coroutine sẽ tự động bị dừng nếu GameObject chứa script bị `SetActive(false)` hoặc bị tiêu hủy (`Destroy`).

```csharp
using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    private Coroutine flashEffectRoutine;

    private void Start()
    {
        flashEffectRoutine = StartCoroutine(FlashEffect(3, 0.5f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flashEffectRoutine != null)
            {
                StopCoroutine(flashEffectRoutine);
                flashEffectRoutine = null;
            }
        }
    }

    private IEnumerator FlashEffect(int repeatCount, float delay)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Debug.Log("Bat den flash");
            yield return new WaitForSeconds(delay);

            Debug.Log("Tat den flash");
            yield return null;
        }

        Debug.Log("Ket thuc hieu ung");
    }
}
```
