use master
go

if exists (select* from sysdatabases where name = 'CNPMNC_DATA1')
	drop database CNPMNC_DATA1
go

create database CNPMNC_DATA1
go

use CNPMNC_DATA1
go

CREATE TABLE LOAIKH(
	MaLoaiKH INT IDENTITY(1,1) PRIMARY KEY,
	TenLKH NVARCHAR(50) NOT NULL,
	ChietKhau FLOAT
)
go
CREATE TABLE KHACHHANG(
	MaKH INT IDENTITY(1,1) PRIMARY KEY,
	TenTKKH VARCHAR(25) NOT NULL,
	MatKhauKH VARCHAR(30) NOT NULL,
	EmailKH VARCHAR(35) NOT NULL,
	DiemThuongKH SMALLINT NOT NULL,
	TrangThaiTKKH NVARCHAR(25) NOT NULL,
	MaLoaiKH INT REFERENCES LOAIKH(MaLoaiKH)
)
go
CREATE TABLE LOAITKNV(
	MaLNV INT IDENTITY(1,1) PRIMARY KEY,
	TenLNV NVARCHAR(25) NOT NULL
)
go
CREATE TABLE NHANVIEN(
	MaNV INT IDENTITY(1,1) PRIMARY KEY,
	HoTenNV NVARCHAR(100) NOT NULL,
	Email VARCHAR(35) NOT NULL,
	MatKhauNV VARCHAR(30) NOT NULL,
	TrangThaiTKNV NVARCHAR(25) NOT NULL
)
go
CREATE TABLE PHANQUYENNV(
	MaPQ INT IDENTITY(1,1) PRIMARY KEY,
	MaLNV INT REFERENCES LOAITKNV(MaLNV),
	MaNV INT REFERENCES NHANVIEN(MaNV)
)
go
CREATE TABLE THELOAIP(
	MaTL INT IDENTITY(1,1) PRIMARY KEY,
	TenTL NVARCHAR(100) NOT NULL,
	MoTaTL NVARCHAR(255) NOT NULL
)
go
CREATE TABLE GIOIHANTUOI(
	MaGHT INT IDENTITY(1,1) PRIMARY KEY,
	TenGHT CHAR(10) NOT NULL,
	MoTaGHT NVARCHAR(255) NOT NULL
)
go
CREATE TABLE PHIM(
	MaPhim INT IDENTITY(1,1) PRIMARY KEY,
	TenPhim NVARCHAR(100) NOT NULL,
	TomTatP NVARCHAR(MAX) NOT NULL,
	NgayCongChieu DATE NOT NULL,
	ThoiLuongP SMALLINT NOT NULL,
	LuotMua SMALLINT CHECK (LuotMua>=0) NOT NULL,
	LuotThich SMALLINT CHECK (LuotThich>=0) NOT NULL,
	HinhAnh VARCHAR(50) NOT NULL,
	Trailer VARCHAR(100) NOT NULL,
	GiaPhim FLOAT CHECK (GiaPhim>=0) NOT NULL,
	MaGHT INT REFERENCES GIOIHANTUOI(MaGHT)
)
go
CREATE TABLE TL_P(
	MaTLP INT IDENTITY(1,1) PRIMARY KEY,
	MaPhim INT REFERENCES PHIM(MaPhim),
	MaTL INT REFERENCES THELOAIP(MaTL)
)
go
CREATE TABLE LOAIPC(
	MaLPC INT IDENTITY(1,1) PRIMARY KEY,
	TenLPC NVARCHAR(30) NOT NULL,
	MoTaLPC NVARCHAR(100) NOT NULL
)
go
CREATE TABLE LOAIGHE(
	MaGhe INT IDENTITY(1,1) PRIMARY KEY,
	TenLG CHAR(10) NOT NULL,
	GiaLGhe FLOAT NOT NULL
)
go
CREATE TABLE PHONGCHIEU(
	MaPC INT IDENTITY(1,1) PRIMARY KEY,
	TenPC NVARCHAR(30) NOT NULL,
	SLGheThuong SMALLINT CHECK (SLGheThuong>=1) NOT NULL,
	SLGheVIP SMALLINT CHECK (SLGheVIP>=1) NOT NULL,
	MaLPC INT REFERENCES LOAIPC(MaLPC)
)
go
CREATE TABLE GHECUAPC(
	MaPC INT REFERENCES PHONGCHIEU(MaPC),
	MaGhe INT REFERENCES LOAIGHE(MaGhe),
	TrangThaiGhePC NVARCHAR(50) NOT NULL,
	TenGhePC CHAR(10) NOT NULL,
	PRIMARY KEY (MaPC, MaGhe)
)
go
CREATE TABLE XUATCHIEU(
	MaXC INT IDENTITY(1,1) PRIMARY KEY,
	CaXC CHAR(10),
	GioXC TIME NOT NULL
)
go
CREATE TABLE LICHCHIEU(
	MaLC INT IDENTITY(1,1) PRIMARY KEY,
	NgayLC DATE NOT NULL,
	TrangThaiLC NVARCHAR(50) NOT NULL,
	SLVeDat INT CHECK (SLVeDat>=0) NOT NULL,
	MaXC INT REFERENCES XUATCHIEU(MaXC), --sai here, HPhước đã sửa
	MaPC INT REFERENCES PHONGCHIEU(MaPC),
	MaPhim INT REFERENCES PHIM(MaPhim)
)
go
CREATE TABLE BINHLUAN(
	MaBL INT IDENTITY(1,1) PRIMARY KEY,
	MaPhim INT REFERENCES PHIM(MaPhim),
	TenTK VARCHAR(25),
	GhiChu NVARCHAR(MAX) NOT NULL,
	TrangThai NVARCHAR(30) NOT NULL,
	NgayTao DATETIME NOT NULL
)
go
CREATE TABLE YEUTHICH(
	MaPhim INT REFERENCES PHIM(MaPhim),
	MaKH INT REFERENCES KHACHHANG(MaKH),
	PRIMARY KEY (MaPhim, MaKH)
)
go
CREATE TABLE VEPHIM(
	MaVe INT IDENTITY(1,1) PRIMARY KEY,
	NgayDat DATETIME NOT NULL,
	TrangThaiThanhToan NVARCHAR(20),-- NOT NULL,
	TrangThaiHetHan NVARCHAR(20), --NOT NULL,
	SLGhe SMALLINT NOT NULL CHECK (SLGhe>=0),
	GiaVe FLOAT NOT NULL,
	MaLC INT REFERENCES LICHCHIEU(MaLC),
	MaKH INT REFERENCES KHACHHANG(MaKH)
)
go
CREATE TABLE VE_GHE(
	MaVG INT IDENTITY (1,1) PRIMARY KEY,
	MaVe INT REFERENCES VEPHIM(MaVe),
	TenGheVG CHAR(10) NOT NULL,
	TrangThaiVG NVARCHAR (MAX) NOT NULL
)
go
CREATE TABLE HOADON(
	MaHD INT IDENTITY(1,1) PRIMARY KEY,
	NgayTao DATETIME NOT NULL,
	TongGiaHD FLOAT NOT NULL,
	TongGiaSauGiam FLOAT NOT NULL,
	TiLeGG FLOAT NOT NULL,
	MaKH INT REFERENCES KHACHHANG(MaKH),
	MaNV INT REFERENCES NHANVIEN(MaNV)
)
go
CREATE TABLE CHITIETHD(
	MaVe INT REFERENCES VEPHIM(MaVe),
	MaHD INT REFERENCES HOADON(MaHD),
	SoLuongVe SMALLINT CHECK (SoLuongVe>=1) NOT NULL,
	ThanhTienVe FLOAT NOT NULL,
	PRIMARY KEY (MaVe, MaHD)
)
go
INSERT INTO GIOIHANTUOI values ('P', N'Mọi lứa tuổi')
INSERT INTO GIOIHANTUOI values ('P13', N'Trên 13 tuổi')
INSERT INTO GIOIHANTUOI values ('P16', N'Trên 16 tuổi')
INSERT INTO GIOIHANTUOI values ('P18', N'Trên 18 tuổi')

INSERT INTO PHIM VALUES
(N'ÁN MẠNG Ở VENICE', 
N'Dựa trên tiểu thuyết Halloween Party của nhà văn Agatha Christie, hành trình 
phá án của thám tử Hercule Poirot tiếp tục được đưa lên màn ảnh rộng.',
GETDATE(),103,0,0,'anmangovenice.png',
'https://www.youtube.com/watch?v=EL8FdLQFUhc',50000,3)

INSERT INTO PHIM VALUES
(N'THE NUN', N'Là phần phim tiếp nối câu chuyện năm 2019 của The Nun, phim lấy bối cảnh nước Pháp năm 1956, 
		cùng cái chết bí ẩn của một linh mục, một giai thoại đáng sợ và ám ảnh sẽ mở ra tiếp tục xoay 
		quanh nhân vật chính - Sơ Irene. Liệu Sơ Irene có nhận ra, đây chính là hồn ma nữ tu Valak từng 
		có cuộc chiến “sống còn” với cô không lâu trước đây.',GETDATE(),110,0,0,
		'thenun.png','https://www.youtube.com/watch?v=vab6sPIceuU',50000,4)
	
INSERT INTO PHIM VALUES
(N'BIỆT ĐỘI ĐÁNH THUÊ 4', N'Biệt Đội Đánh Thuê - gồm cả các gương mặt kỳ cựu và những tân binh - đã bắt đầu một nhiệm vụ mới. 
		Lần này, họ sẽ tới một nhà máy vũ khí hạt nhân cũ tại Qadhafi để tóm gọn Suharato Rahmat, kẻ đang âm 
		mưu một mình đánh cắp kíp nổ hạt nhân cho gã khách hàng xảo quyệt Ocelot. Rahmat từng là một tay buôn
		vũ khí người Anh với đội quân của riêng mình. Hắn đã thành công đánh cắp va li chứa kíp nổ trước khi
		Biệt Đội Đánh Thuê kịp tìm đến. Nếu kíp nổ rơi vào tay Ocelot, gã sẽ hủy diệt cả thế giới. 
		Sau khi nhiệm vụ tóm gọn Rahmat thất bại, cả đội tiếp tục hành trình trên con thuyền Jantara, 
		nơi bước ngoặt thực sự của câu chuyện xảy ra, và một sự thật gây sốc về Ocelot được hé lộ…',GETDATE(),103,10,0,'bddt4.png',
		'https://www.youtube.com/watch?v=P5zcuOefk1A',50000,4)

INSERT INTO PHIM VALUES
(N'HỌA QỦY', N'Nhà khoa học thiên tài Tomohiko Kataoka được trưởng nhóm nghiên cứu Synthekai VR yêu cầu tham gia 
		cùng họ trên Đảo Abominable. Ở đó, họ đã tạo ra một không gian ảo cho toàn bộ hòn đảo và họ muốn 
		Tomohiko sử dụng các kỹ thuật tiên tiến của mình để nâng cấp dự án. Tuy nhiên, khi Tomohiko đeo kính
		VR và bước vào thế giới ảo, trời đột nhiên trở nên tối tăm và một người phụ nữ bí ẩn xuất hiện. 
		Những cái chết bí ẩn xảy ra với nhân viên công ty công nghệ VR. Có một nỗi sợ hãi chưa từng có 
		đang chờ đợi giữa thực tế và thế giới ảo.',GETDATE(),107,10,0,'hoaquy.png',
		'https://www.youtube.com/watch?v=WUuMNNqzEO0',50000,4)

INSERT INTO PHIM VALUES
(N'NHÂN DUYÊN TIỀN ĐÌNH', N'Chuyện phim xoay quanh nhân vật Chi-ho (Yoo Hae-jin) - nhà nghiên cứu bim bim với khả năng nếm
		vị xuất chúng, nhưng lại ngờ nghệch với mọi thứ xung quanh. Chi-ho là một người cực kỳ hướng nội,
		thích ở một mình và sống như một cái máy được lập trình sẵn mà không hề mắc lỗi dù chỉ một giây. 
		Trong lúc phải đi trả nợ thay cho người anh trai cờ bạc (Cha In-pyo), Chi-ho đã gặp gỡ “nhân viên 
		đòi nợ” Il-young - người phụ nữ hướng ngoại, luôn suy nghĩ tích cực về cuộc sống dù đang ở trong 
		hoàn cảnh khó khăn của một bà mẹ đơn thân. Khác biệt về tính cách lẫn ngoại hình khiến cả hai trở 
		thành “trái dấu hút nhau”. Sự “trái dấu” này đã đẩy đưa cuộc tình của họ đến vô vàn tình huống 
		“cười ra nước mắt” nhưng cũng không kém phần cảm xúc.',GETDATE()+1,119,10,0,
		'ndtd.png','https://www.youtube.com/watch?v=zlPzyxdhQbI',50000,3)

INSERT INTO PHIM VALUES
(N'LIVE - #pháttrựctiếp', N'Bộ phim Việt đầu tiên trực diện nói về vấn đề bạo lực mạng xã hội.
		Câu chuyện xoay quanh hai người trẻ đầy tham vọng, bất chấp tất cả
		để có thể trở nên nổi tiếng trên mạng. Họ dùng đủ cách thức lẫn chiêu
		trò để đạt được mục đích của mình, cho đến khi chính bản thân họ lại 
		thành con mồi mới cho những kẻ trên mạng, những người sẵn sàng lao vào
		tấn công người khác chỉ vì “Không ưa con đó.”',
		GETDATE()+1,119,10,0,'phattructiep.png','https://www.youtube.com/watch?v=REgmCauEHDM',50000,4)

INSERT INTO PHIM VALUES
(N'THE NUN II', N'Là phần phim tiếp nối câu chuyện năm 2019 của The Nun, phim lấy bối cảnh nước Pháp năm 1956, 
		cùng cái chết bí ẩn của một linh mục, một giai thoại đáng sợ và ám ảnh sẽ mở ra tiếp tục xoay 
		quanh nhân vật chính - Sơ Irene. Liệu Sơ Irene có nhận ra, đây chính là hồn ma nữ tu Valak từng 
		có cuộc chiến “sống còn” với cô không lâu trước đây.',
		GETDATE()+1,91,0,0,'thenun2.png','https://www.youtube.com/watch?v=QF-oyCwaArU',50000,4)

INSERT INTO PHIM VALUES
(N'ĐẤT RỪNG PHƯƠNG NAM', N'Sau bao ngày chờ đợi, dự án điện ảnh gợi ký ức tuổi thơ của nhiều thế hệ người Việt chính thức 
		tung hình ảnh đầu tiên đầy cảm xúc. First look poster khắc họa hình ảnh đối lập: bé An đang ôm 
		chặt mẹ giữa một khung cảnh chạy giặc loạn lạc. Cùng chờ đợi và theo dõi thêm hành trình bé An đi
		tìm cha khắp nam kỳ lục tỉnh cùng các người bạn đồng hành nhé!',
		GETDATE()+1,110,0,0,'drpn.png','https://www.youtube.com/watch?v=hzyg3lvFPvk',50000,3)

INSERT INTO PHIM VALUES
(N'ARGYLLE SIÊU ĐIỆP VIÊN', N'Argylle là ai? Duy nhất 1 cách có thể tìm ra câu trả lời. 
		ARGYLLE SIÊU ĐIỆP VIÊN | Dự Kiến Khởi Chiếu - Mùng 1 Tết 10.02.2024',
		GETDATE(),95,0,0,'angylle.png','https://www.youtube.com/watch?v=7mgu9mNZ8Hk',50000,2)


INSERT INTO THELOAIP VALUES (N'Hành động', N'Phim có tính chất bạo lực và hành động mãn nhãn')
INSERT INTO THELOAIP VALUES (N'Giải trí', N'Phim có tính chất giải trí cao')
INSERT INTO THELOAIP VALUES (N'Kinh dị', N'Phim có tính chất kinh dị cao')

INSERT INTO TL_P VALUES (1,1)
INSERT INTO TL_P VALUES (1,3)
INSERT INTO TL_P VALUES (2,3)
INSERT INTO TL_P VALUES (3,1)
INSERT INTO TL_P VALUES (3,2)
INSERT INTO TL_P VALUES (4,3)
INSERT INTO TL_P VALUES (5,2)
INSERT INTO TL_P VALUES (6,2)
INSERT INTO TL_P VALUES (7,3)
INSERT INTO TL_P VALUES (7,1)
INSERT INTO TL_P VALUES (8,2)
INSERT INTO TL_P VALUES (8,1)

INSERT INTO XUATCHIEU VALUES ('CA 1', '7:00:00')
INSERT INTO XUATCHIEU VALUES ('CA 2', '10:30:00')
INSERT INTO XUATCHIEU VALUES ('CA 3', '14:00:00')
INSERT INTO XUATCHIEU VALUES ('CA 4', '17:30:00')
INSERT INTO XUATCHIEU VALUES ('CA 5', '21:00:00') -- Ca chiếu lúc 9h là ca đêm cuối cùng, chiếu tới 0:30 hôm sau

INSERT INTO LOAIPC VALUES ('2D', N'Phòng chiếu 2D')
INSERT INTO LOAIPC VALUES ('3D', N'Phòng chiếu 3D')

INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 1', 70, 10, 1) 
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 2', 70, 20, 2) 
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 3', 50, 10, 1) 
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 4', 50, 10, 2) 

INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 1, 1, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 2, Phòng 1(2D), Mã phim 2
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 2, 1, 2)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 3, Phòng 1(2D), Mã phim 3
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 3, 1, 3)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 4, Phòng 1(2D), Mã phim 4
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 4, 1, 4)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 5, Phòng 1(2D), Mã phim 5
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 5, 1, 5)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 1, Phòng 2(3D), Mã phim 6
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 1, 2, 6)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 2, Phòng 2(3D), Mã phim 7
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 2, 2, 7)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 3, Phòng 2(3D), Mã phim 8
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 3, 2, 8)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 4, Phòng 2(3D), Mã phim 9
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 4, 2, 9)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 5, Phòng 2(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 5, 2, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 5, Phòng 3(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 5, 3, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 5, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 5, 4, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 4, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 4, 4, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 3, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 3, 4, 1)
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 2, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 3, 4, 1)

--Insert account mẫu
INSERT INTO LOAIKH VALUES ('Normal', 0.0)
INSERT INTO LOAIKH VALUES ('VIP', 10/100.0)

INSERT INTO LOAITKNV VALUES (N'QuanLyPhongChieu');
INSERT INTO LOAITKNV VALUES (N'QuanLyPhim');
INSERT INTO LOAITKNV VALUES (N'QuanLyLichChieu');
INSERT INTO LOAITKNV VALUES (N'QuanLyKhachHang');
INSERT INTO LOAITKNV VALUES (N'QuanLyNhanVien');
INSERT INTO LOAITKNV VALUES (N'QuanLyHeThong');

INSERT INTO NHANVIEN VALUES (N'Phước', 'phuoc1@gmail.com', 12345678, 'ACTIVE')

INSERT INTO PHANQUYENNV VALUES ('1', '1')
INSERT INTO PHANQUYENNV VALUES ('2', '1')
INSERT INTO PHANQUYENNV VALUES ('3', '1')

----Truy xuất dữ liệu từ các bảng
--SELECT* FROM PHANQUYENNV
--SELECT* FROM LOAITKNV
--SELECT* FROM LOAIKH
--SELECT* FROM KHACHHANG
--SELECT* FROM NHANVIEN
--SELECT* FROM THELOAIP
--SELECT* FROM TL_P
--SELECT* FROM GIOIHANTUOI
--SELECT* FROM PHIM
--SELECT* FROM LOAIGHE
--SELECT* FROM GHECUAPC
--SELECT* FROM XUATCHIEU
--SELECT* FROM LOAIPC
--SELECT* FROM LICHCHIEU
--SELECT* FROM PHONGCHIEU
--SELECT* FROM BINHLUAN
--SELECT* FROM YEUTHICH
--SELECT* FROM VEPHIM
--SELECT* FROM VE_GHE
--SELECT* FROM HOADON
--SELECT* FROM CHITIETHD