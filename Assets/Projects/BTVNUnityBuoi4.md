**Bài cũ**

## **1\. New Input System**

New Input System là hệ thống quản lý đầu vào của Unity. Nó được thiết kế theo hướng sự kiện (Event-driven), hỗ trợ đa nền tảng (PC, Mobile, Console) tốt hơn và dễ dàng tuỳ biến phím bấm (Key binding).

> * **Input Action Asset:** File lưu trữ các cấu hình phím bấm (Action Maps, Actions, Bindings).  
> * **Player Input Component:** Component gắn vào GameObject để nhận và xử lý các sự kiện từ Input Action.

**Ví dụ cấu trúc code nhận Input:**

using UnityEngine;  
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour  
{  
    private Vector2 moveInput;

    // Hàm này được gọi qua Unity Event của Player Input Component  
    public void OnMove(InputAction.CallbackContext context)  
    {  
        moveInput \= context.ReadValue\<Vector2\>();  
    }  
}

## **2\. Physics 2D**

### **2.1. Rigidbody 2D**

Rigidbody 2D là component đưa GameObject vào sự mô phỏng của hệ thống vật lý Unity (chịu tác dụng của trọng lực, lực đẩy, va chạm...). Có 3 loại Body Type chính:

| Body Type | Đặc điểm & Ứng dụng |
| :---- | :---- |
| **Dynamic** | Bị ảnh hưởng hoàn toàn bởi vật lý (trọng lực, khối lượng, lực tác động). Dùng cho: Nhân vật người chơi, hòm gỗ có thể đẩy, vật thể rơi,... |
| **Kinematic** | Không bị ảnh hưởng bởi trọng lực hay lực đẩy từ bên ngoài. Chỉ di chuyển bằng code (Transform hoặc Rigidbody2D.velocity). Dùng cho: Nền tảng di chuyển (Moving platform), thang máy,... |
| **Static** | Đứng yên tuyệt đối, không di chuyển, tốn rất ít tài nguyên tính toán. Dùng cho: Mặt đất, bức tường, chướng ngại vật cố định,... |

### **2.2. Va chạm (Collision) & Điều kiện xảy ra**

Va chạm vật lý (Collision) là khi hai vật thể rắn đập vào nhau và ngăn không cho đối phương đi xuyên qua.

> * **Điều kiện xảy ra:**  
  * Cả 2 GameObject **đều phải có Collider 2D**.  
  * Ít nhất 1 trong 2 GameObject **phải có Rigidbody 2D** (thường là loại Dynamic hoặc Kinematic).  
> * **Các hàm bắt sự kiện:** OnCollisionEnter2D, OnCollisionStay2D, OnCollisionExit2D.

'''
using UnityEngine;

public class PlayerPhysicsDemo : MonoBehaviour
{
    public Transform chan;
    public float kc = 0.2f;
    public LayerMask datMask;
    public int coin = 0;
    
    private Rigidbody2D rb;
    private bool chamDat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(chan.position, Vector2.down, kc, datMask);
        chamDat = hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            rb.AddForce(Vector2.up * 250f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            Destroy(other.gameObject);
        }
    }
}
'''
**Bài mới**


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

