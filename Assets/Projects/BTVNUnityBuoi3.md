**Bài cũ**

## 1\. Vòng đời của 1 Script

Vòng đời của một script là thứ tự mà Unity tự động gọi các hàm (event functions) bên trong MonoBehaviour từ lúc script được sinh ra cho đến khi bị phá hủy.

### Giai đoạn Khởi tạo (Initialization)

* **Awake():** Được gọi duy nhất một lần khi script/GameObject được load (ngay cả khi script đó đang bị tắt \- disable). Thường dùng để gán các biến tham chiếu (reference) giữa các component.  
* **OnEnable():** Được gọi mỗi khi script hoặc GameObject chứa nó được bật lên (Active). Có thể được gọi nhiều lần.  
* **Start():** Được gọi duy nhất một lần ngay trước khung hình (frame) đầu tiên, nhưng chỉ khi script đang được bật. Thường dùng để khởi tạo logic game sau khi mọi Awake() đã chạy xong.

### Giai đoạn Cập nhật Vật lý (Physics)

* **FixedUpdate():** Được gọi theo những khoảng thời gian cố định (mặc định là 0.02s). Tất cả các tính toán liên quan đến vật lý (như Rigidbody, thêm lực) bắt buộc phải đặt ở đây để không bị giật lag khi tốc độ khung hình thay đổi.

### Giai đoạn Cập nhật Logic Game (Game Logic)

* **Update():** Được gọi mỗi khung hình (per frame). Tần suất gọi phụ thuộc vào FPS của game. Thường dùng để nhận input từ người chơi hoặc di chuyển nhân vật cơ bản.  
* **LateUpdate():** Được gọi mỗi khung hình, nhưng luôn chạy sau khi tất cả các hàm Update() của mọi script đã chạy xong. Rất hữu ích cho việc làm Camera bám theo nhân vật.

### Giai đoạn Kết thúc (Decommissioning)

* **OnDisable():** Gọi khi GameObject hoặc script bị tắt đi. Dùng để reset các chỉ số hoặc hủy đăng ký sự kiện.  
* **OnDestroy():** Gọi một lần duy nhất khi GameObject bị xóa khỏi bộ nhớ (khi gọi hàm Destroy()). Dùng để dọn dẹp rác, giải phóng tài nguyên.

## **2\. Gizmos**

Gizmos được sử dụng để vẽ các hình khối, đường thẳng, và các ký hiệu trực quan trong cửa sổ Scene. Mục đích chính của Gizmos là để debug dễ hơn hoặc hỗ trợ thiết kế level, giúp dev nhìn thấy những thành phần vô hình (như phạm vi phát hiện kẻ địch, phạm vi tấn công, v.v.).

### **Các hàm gọi Gizmos:**

> * OnDrawGizmos(): Được gọi mỗi frame, dùng để vẽ Gizmos luôn luôn hiển thị trên Scene.  
> * OnDrawGizmosSelected(): Chỉ vẽ Gizmos hiển thị khi GameObj chứa script đó đang được chọn.

### **Một số hàm hay dùng:**

- Gizmos.color: Đặt màu sắc cho các hình Gizmos chuẩn bị được vẽ ở các dòng code dưới nó.  
- Gizmos.DrawLine(Vector3 \<điểm đầu\>, Vector3 \<điểm cuối\>): Vẽ một đường thẳng nối giữa 2 điểm trong không gian 3D.  
- Gizmos.DrawWireSphere(Vector3 \<tâm\>, float \<bán kính\>): Vẽ một hình cầu rỗng (dạng lưới dây) với tâm và bán kính xác định.  
- Gizmos.DrawCube(Vector3 \<tâm\>, Vector3 \<kích thước\>): Vẽ một khối lập phương đặc.

## **3\. Transform và thao tác với Transform (Add assets transform)**

Component Transform xác định Position, Rotation và Scale của mọi GameObj trong scene. Bất kỳ GameObj nào được tạo ra cũng bắt buộc phải có 1 Transform.

> * **Position:** Toạ độ toàn cục (World space) của đối tượng.  
> * **LocalPosition:** Toạ độ tương đối so với đối tượng cha (Parent). Nếu không có cha, nó giống với position.  
> * **Rotation / localRotation:** Góc quay của đối tượng.  
> * **LocalScale:** Tỷ lệ kích thước của đối tượng so với cha của nó.

### **Các hàm thao tác (Methods):**

> * **Translate(Vector3 translation):** Di chuyển đối tượng một khoảng cách theo hướng chỉ định.  
>   *Ví dụ: transform.Translate(Vector3.forward \* speed \* Time.deltaTime);*  
> * **Rotate(Vector3 eulerAngles):** Xoay đối tượng quanh các trục X, Y, Z thêm một góc cụ thể.  
> * **LookAt(Transform target):** Lập tức xoay trục Z (mặt trước) của đối tượng hướng về phía một mục tiêu cụ thể.

**Cấu trúc cha con (Hierarchy):** Quản lý hệ thống phân cấp bằng cách sử dụng transform.parent (để gán hoặc lấy đối tượng cha) hoặc transform.GetChild(int index) (để lấy đối tượng con).

## **4\. Lớp Time và các hàm của Time**

Lớp Time trong Unity cung cấp thông tin về thời gian của trò chơi, giúp xử lý các chuyển động, hiệu ứng và logic vật lý mượt mà, độc lập với tốc độ khung hình (framerate) của máy tính.

- **Time.Time scale:** Tốc độ game chạy  
- **Time.Deltatime:** Thời gian đã trôi qua kể từ frame trước.  
- **Time.fixeddeltatime:** Khoảng thời gian cố định giữa các lần gọi hàm FixedUpdate()  
- **Time.UnscaledDeltatime:** Thời gian đã trôi qua kể từ frame trước, nhưng không ảnh hưởng bởi Time.timeScale 

## **5\. Mathf**

Mathf là một struct chứa các hàm tính toán toán học.

- **Mathf.Abs(float f):** Trả về giá trị tuyệt đối  
- **Mathf.Clamp(float value, float min, float max):** Giới hạn giá trị nằm lọt trong khoảng từ min đến max  
- **Mathf.Round(float f):** Làm tròn số thực về số nguyên gần nhất  
- **Mathf.Max(a, b) / Mathf.Min(a, b):** Trả về giá trị lớn nhất / nhỏ nhất trong các số được truyền vào hàm.  
- **Mathf.Sin(float f) / Mathf.Cos(float f):** Hàm lượng giác cơ bản  
- **Mathf.PI:** Hằng số Pi (\~3.14159...)  
- **Mathf.Infinity:** Biểu diễn giá trị dương vô cùng

**Bài mới**

## **1\. New Input System**

New Input System là hệ thống quản lý đầu vào của Unity. Nó được thiết kế theo hướng sự kiện (Event-driven), hỗ trợ đa nền tảng (PC, Mobile, Console) tốt hơn và dễ dàng tuỳ biến phím bấm (Key binding).

> * **Input Action Asset:** File lưu trữ các cấu hình phím bấm (Action Maps, Actions, Bindings).  
> * **Player Input Component:** Component gắn vào GameObject để nhận và xử lý các sự kiện từ Input Action.

**Ví dụ cấu trúc code nhận Input gọn gàng:**

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

### **2.3. Trigger (Xuyên thấu)**

Trigger xảy ra khi Collider 2D được tick chọn **"Is Trigger"**. Lúc này, vật thể sẽ trở nên "vô hình" về mặt vật lý cản trở (các vật khác có thể đi xuyên qua), nhưng Unity vẫn ghi nhận sự kiện khi có vật chạm vào.

> * **Ứng dụng:** Vùng phát hiện kẻ địch, nhặt xu (coin), cổng dịch chuyển (portal).  
> * **Các hàm bắt sự kiện:** OnTriggerEnter2D, OnTriggerStay2D, OnTriggerExit2D.

### **2.4. Raycast 2D**

Raycast giống như việc bắn ra một tia laser vô hình từ một điểm theo một hướng nhất định. Nếu tia này chạm vào một Collider nào đó, nó sẽ trả về thông tin của vật thể đó (khoảng cách, điểm chạm, tên vật thể...).

> * **Ứng dụng:** Bắn súng hit-scan, kiểm tra nhân vật có đang chạm đất không (Ground check), tầm nhìn của AI.

// Ví dụ bắn một tia xuống dưới để kiểm tra mặt đất  
RaycastHit2D hit \= Physics2D.Raycast(transform.position, Vector2.down, 1.5f);

if (hit.collider \!= null)  
{  
    Debug.Log("Chạm vào: " \+ hit.collider.name);  
}

### **2.5. Layer Mask**

Layer Mask được dùng để lọc và chỉ định các Layer cụ thể mà hệ thống vật lý (như Raycast hoặc va chạm) nên tương tác hoặc bỏ qua. Điều này giúp tối ưu hiệu suất và tránh lỗi (ví dụ: tia raycast của nhân vật tự bắn trúng chính nhân vật).

public LayerMask groundLayer;

// Chỉ trả về true nếu tia Ray chạm vào đối tượng thuộc groundLayer  
RaycastHit2D hit \= Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);

## **3\. Các cách di chuyển nhân vật**

| Phương pháp | Cơ chế & Ưu/Nhược điểm |
| :---- | :---- |
| **Transform.Translate** hoặc đổi **Transform.position** | Di chuyển trực tiếp toạ độ mà không thông qua hệ thống vật lý. *\- Ưu điểm:* Code đơn giản, phản hồi tức thì. *\- Nhược điểm:* Dễ gây lỗi xuyên tường, giật lag khi va chạm vì nó "dịch chuyển tức thời" chứ không "đẩy" nhân vật. Khuyên dùng cho vật thể Kinematic. |
| **Rigidbody2D.velocity** | Gán trực tiếp vận tốc cho Rigidbody. *\- Ưu điểm:* Tương tác tốt với hệ thống vật lý, không bị xuyên tường, kiểm soát tốc độ chính xác (rất hợp cho game Platformer). *\- Nhược điểm:* Sẽ ghi đè lên các lực khác (như lực đẩy, lực nảy) nếu không xử lý khéo. |
| **Rigidbody2D.AddForce** | Thêm một lực tác động lên nhân vật. *\- Ưu điểm:* Chuyển động rất thực tế, có quán tính, gia tốc. Tuyệt vời cho game lái xe hoặc nhảy (Jump). *\- Nhược điểm:* Khó kiểm soát để nhân vật dừng lại ngay lập tức hoặc duy trì một vận tốc không đổi. |

