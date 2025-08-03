# Hệ Thống Camera Theo Dõi Player

## Mô tả
Hệ thống camera này cho phép camera di chuyển theo người chơi với khả năng xoay camera tự do. Camera luôn hướng về player và hướng di chuyển của player sẽ theo hướng camera.

## Tính năng chính

### CameraFollow.cs
- **Di chuyển theo player**: Camera tự động di chuyển theo vị trí của player
- **Xoay camera**: Sử dụng chuột để xoay camera quanh player
- **Giới hạn góc nhìn**: Có thể giới hạn góc nhìn lên/xuống
- **Khóa cursor**: Tự động khóa con trỏ chuột vào giữa màn hình
- **Smooth movement**: Di chuyển mượt mà với SmoothDamp

### PlayerController.cs
- **Di chuyển theo hướng camera**: Player di chuyển theo hướng camera thay vì hướng world
- **WASD movement**: Điều khiển di chuyển bằng WASD
- **Jump**: Nhảy bằng phím Space
- **Sprint**: Chạy nhanh bằng phím Shift
- **Character Controller**: Sử dụng Unity CharacterController để xử lý collision

### GameManager.cs
- **Tự động thiết lập**: Tự động kết nối camera và player
- **Scene setup**: Thiết lập scene một cách tự động
- **Reset scene**: Khả năng reset vị trí player

### ControlDisplay.cs
- **Hiển thị hướng dẫn**: Hiển thị hướng dẫn điều khiển trên màn hình
- **Auto-hide**: Tự động ẩn sau một thời gian
- **Toggle với Tab**: Nhấn Tab để hiển thị/ẩn hướng dẫn

## Cách thiết lập

### Bước 1: Thiết lập Player
1. Tạo một GameObject với tag "Player"
2. Thêm component `CharacterController`
3. Thêm component `PlayerController`

### Bước 2: Thiết lập Camera
1. Chọn Main Camera trong scene
2. Thêm component `CameraFollow`
3. Cấu hình các thông số:
   - **Target**: Gán player GameObject
   - **Offset**: Vị trí camera so với player (mặc định: 0, 5, -8)
   - **Smooth Time**: Thời gian smooth movement (mặc định: 0.2)
   - **Rotation Speed**: Tốc độ xoay camera (mặc định: 2)

### Bước 3: Thiết lập GameManager (Tùy chọn)
1. Tạo một GameObject trống
2. Thêm component `GameManager`
3. Cấu hình các thông số nếu cần

### Bước 4: Thiết lập UI (Tùy chọn)
1. Tạo Canvas
2. Tạo Panel chứa Text
3. Thêm component `ControlDisplay` vào Canvas
4. Gán references cho Text và Panel

## Điều khiển

### Di chuyển
- **W**: Tiến về phía trước (theo hướng camera)
- **S**: Lùi về phía sau (ngược hướng camera)
- **A**: Di chuyển sang trái (theo hướng camera)
- **D**: Di chuyển sang phải (theo hướng camera)

### Camera
- **Chuột**: Xoay camera quanh player
- **ESC**: Toggle khóa/không khóa cursor

### Khác
- **Space**: Nhảy
- **Shift**: Chạy nhanh
- **Tab**: Hiển thị/ẩn hướng dẫn điều khiển

## Cấu hình nâng cao

### CameraFollow Settings
```csharp
public Vector3 offset = new Vector3(0, 5, -8); // Vị trí camera so với player
public float smoothTime = 0.2f;                 // Thời gian smooth movement
public float rotationSpeed = 2f;                // Tốc độ xoay camera
public float minVerticalAngle = -30f;           // Góc nhìn xuống tối thiểu
public float maxVerticalAngle = 60f;            // Góc nhìn lên tối đa
public bool lockCursor = true;                  // Khóa cursor
```

### PlayerController Settings
```csharp
public float moveSpeed = 5f;        // Tốc độ di chuyển bình thường
public float sprintSpeed = 8f;      // Tốc độ chạy nhanh
public float jumpHeight = 2f;       // Độ cao nhảy
public float gravity = -9.81f;      // Trọng lực
```

## Lưu ý quan trọng

1. **Input System**: Script sử dụng Unity Input System, đảm bảo đã cài đặt và cấu hình đúng
2. **Character Controller**: Player phải có component CharacterController để xử lý collision
3. **Camera Target**: CameraFollow cần có target (player) được gán đúng
4. **Tag Player**: Player GameObject phải có tag "Player"

## Troubleshooting

### Camera không di chuyển theo player
- Kiểm tra target trong CameraFollow component
- Đảm bảo player có tag "Player"

### Player không di chuyển theo hướng camera
- Kiểm tra cameraFollow reference trong PlayerController
- Đảm bảo CameraFollow component có các hàm GetCameraForward() và GetCameraRight()

### Input không hoạt động
- Kiểm tra Unity Input System đã được cài đặt
- Đảm bảo Input Actions được cấu hình đúng

## Tùy chỉnh

Bạn có thể dễ dàng tùy chỉnh hệ thống bằng cách:
- Thay đổi các thông số trong Inspector
- Sửa đổi logic trong các script
- Thêm các tính năng mới như zoom camera, collision detection, etc. 