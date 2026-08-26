# Kiến Thức Nền Tảng Unity

## 1\. Tìm hiểu MonoBehaviour

Trong Unity, Mono Behaviour là lớp cơ sở (base class) mặc định mà mọi script kế thừa khi được tạo ra.

* Tác dụng chính: Việc kế thừa Mono Behaviour cho phép script có thể được kéo thả trực tiếp vào các Game Object trong môi trường Unity dưới dạng một Component (Thành phần).  
* Quyền năng: Nó cung cấp cho script khả năng giao tiếp với Engine của Unity. Nhờ đó, script có thể lắng nghe các sự kiện hệ thống (như khi game bắt đầu, khi mỗi khung hình trôi qua, khi va chạm vật lý xảy ra) và truy cập nhanh vào các thành phần khác như transform, gameObject, hoặc chạy Coroutine.

Lưu ý: Nếu xóa đoạn “: MonoBehaviour” đi, script đó sẽ không thể gắn vào GameObject được nữa.

## 2\. Vòng đời của 1 Script (Script Lifecycle)

Vòng đời của một script là thứ tự mà Unity tự động gọi các hàm (event functions) bên trong MonoBehaviour từ lúc script được sinh ra cho đến khi bị phá hủy.

### Giai đoạn Khởi tạo (Initialization)

* Awake(): Được gọi duy nhất một lần khi script/GameObject được load (ngay cả khi script đó đang bị tắt \- disable). Thường dùng để gán các biến tham chiếu (reference) giữa các component.  
* OnEnable(): Được gọi mỗi khi script hoặc GameObject chứa nó được bật lên (Active). Có thể được gọi nhiều lần.  
* Start(): Được gọi duy nhất một lần ngay trước khung hình (frame) đầu tiên, nhưng chỉ khi script đang được bật. Thường dùng để khởi tạo logic game sau khi mọi Awake() đã chạy xong.

### Giai đoạn Cập nhật Vật lý (Physics)

* FixedUpdate(): Được gọi theo những khoảng thời gian cố định (mặc định là 0.02s). Tất cả các tính toán liên quan đến vật lý (như Rigidbody, thêm lực) bắt buộc phải đặt ở đây để không bị giật lag khi tốc độ khung hình thay đổi.

### Giai đoạn Cập nhật Logic Game (Game Logic)

* Update(): Được gọi mỗi khung hình (per frame). Tần suất gọi phụ thuộc vào FPS của game. Thường dùng để nhận input từ người chơi hoặc di chuyển nhân vật cơ bản.  
* LateUpdate(): Được gọi mỗi khung hình, nhưng luôn chạy sau khi tất cả các hàm Update() của mọi script đã chạy xong. Rất hữu ích cho việc làm Camera bám theo nhân vật.

### Giai đoạn Kết thúc (Decommissioning)

* OnDisable(): Gọi khi GameObject hoặc script bị tắt đi. Dùng để reset các chỉ số hoặc hủy đăng ký sự kiện.  
* OnDestroy(): Gọi một lần duy nhất khi GameObject bị xóa khỏi bộ nhớ (khi gọi hàm Destroy()). Dùng để dọn dẹp rác, giải phóng tài nguyên.

## 3\. Tìm hiểu Class C\# thuần

Class C\# thuần đơn giản là những class không kế thừa từ MonoBehaviour.

public class PlayerStats {

    public int health;

    public int mana;

    public PlayerStats(int h, int m) {

        health \= h;

        mana \= m;

    }

}

* Cách hoạt động: Unity không tự động quản lý các class này. Ta không thể kéo thả chúng vào GameObject. Để sử dụng, ta phải tự tạo ra chúng bằng từ khóa new.  
* Ưu điểm: Hiệu suất cao, tốn ít bộ nhớ. Hỗ trợ đầy đủ các đặc tính của Lập trình hướng đối tượng (OOP) như Constructor, tính đa hình, kế thừa.  
* Ứng dụng: Dùng làm các class lưu trữ Dữ liệu (chỉ số nhân vật, inventory), quản lý logic độc lập, hoặc các mô hình mạng (API models).