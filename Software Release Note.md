Release Note Of M.O.C (Version 1)

1. General Information
•	Product: Multiverse Of Cinema (M.O.C)
•	Software Product: CNPMNC_REPORT1
•	Software version number: V1
•	Date of Release: 20/10/2023

2. New Features/ User Story
•	Features
o	Admin có thể quản lý phim, thể loại phim, giới hạn tuổi.
o	Admin có thể quản lý phòng chiếu, loại phòng chiếu.
o	Khách hàng có thể xem thông tin chi tiết phim (trailer).

•	User Story
Khách vãng lai	KVL001	Là khách vãng lai, tôi muốn xem danh sách phim sắp chiếu để có thể tiện chọn phim muốn xem
Khách vãng lai	KVL002	Là khách vãng lai, tôi muốn xem danh sách phim đang chiếu để có thể tiện theo dõi và cập nhật về phim muốn xem
Khách vãng lai	KVL003	Là khách vãng lai, tôi muốn xem danh sách phim đã công chiếu có lượt đặt mua vé hiện tại cao nhất để có thể biết được phim nào hay nhất và đặt mua vé
Khách vãng lai	KVL004	Là khách vãng lai, tôi muốn xem chi tiết thông tin phim để nắm bắt thông tin cụ thể của phim đó
Khách vãng lai	KVL005	Là khách vãng lai, tôi muốn xem trailer của phim để biết sơ bộ nội dung của phim
Nhân viên	NV001	Là một nhân viên, tôi muốn thêm mới hoặc xóa, sửa phim trong hệ thống để dễ dàng quản lý phim khi cần
Nhân viên	NV003	Là một nhân viên, tôi muốn thêm, xóa hay sửa thể loại phim để dễ dàng quản lý thể loại phim
Nhân viên	NV007	Là một nhân viên, tôi muốn quản lý phòng chiếu và các thông tin liên quan tới phòng chiếu như loại phòng chiếu để dễ dàng cập nhật thông tin hệ thống khi phòng chiếu thay đổi

3. Enhancement



4. Bug fixed
1.	Bug cập nhật thông tin bị trùng khóa chính trong quản lý của admin.
2.	Bug không thể hiển thị trailer.
3.	Bug lỗi khi mở trang chi tiết phim.
4.	Bug không hiển thị danh sách phim trong trang chủ.
5.	Bug không hiển thị danh sách trong trang quản lý admin.
6.	Bug không thể thêm mới trong trang quản lý của admin.
7.	Bug không thể cập nhật trong trang quản lý của admin.
8.	Bug hiển thị không đúng thông tin trong trang chi tiết phim.

5. Hướng dẫn cài đặt
1.	Chạy file CNPMNC_DATA1 để tạo cơ sở dữ liệu trong Microsoft SQL.
2.	Chạy file CNPMNC_REPORT1.sln để mở folder trong Visual Studio.
3.	Vào folder Models sau đó vào SQLData.cs.
4.	Chỉnh lại địa chỉ dẫn đến server SQL với “localhost” là tên server và “CNPMNC_DATA1” là tên database. 
5.	Sau khi chỉnh xong thì chọn run để chạy code.
