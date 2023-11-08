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

CREATE TABLE KHACHHANG(
	MaKH INT IDENTITY(1,1) PRIMARY KEY,
	TenTKKH VARCHAR(25) NOT NULL,
	MatKhauKH VARCHAR(30) NOT NULL,
	EmailKH VARCHAR(35) NOT NULL,
	DiemThuongKH SMALLINT NOT NULL,
	TrangThaiTKKH NVARCHAR(25) NOT NULL,
	MaLoaiKH INT REFERENCES LOAIKH(MaLoaiKH)
)

--CREATE TABLE LAOINV(
--	MaLoaiNV INT IDENTITY(1,1) PRIMARY KEY,
--	TenLoaiNV NVARCHAR(100) NOT NULL
--)

CREATE TABLE NHANVIEN(
	MaNV INT IDENTITY(1,1) PRIMARY KEY,
	HoTenNV NVARCHAR(100) NOT NULL,
	Email VARCHAR(35) NOT NULL,
	MatKhauNV VARCHAR(30) NOT NULL,
	TrangThaiTKNV NVARCHAR(25) NOT NULL
	--MaLoaiNV INT REFERENCES LAOINV(MaLoaiNV)
)

CREATE TABLE THELOAIP(
	MaTL INT IDENTITY(1,1) PRIMARY KEY,
	TenTL NVARCHAR(100) NOT NULL,
	MoTaTL NVARCHAR(255) NOT NULL
)

CREATE TABLE GIOIHANTUOI(
	MaGHT INT IDENTITY(1,1) PRIMARY KEY,
	TenGHT CHAR(10) NOT NULL,
	MoTaGHT NVARCHAR(255) NOT NULL
)

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

CREATE TABLE TL_P(
	MaTLP INT IDENTITY(1,1) PRIMARY KEY,
	MaPhim INT REFERENCES PHIM(MaPhim),
	MaTL INT REFERENCES THELOAIP(MaTL)
)

CREATE TABLE LOAIPC(
	MaLPC INT IDENTITY(1,1) PRIMARY KEY,
	TenLPC NVARCHAR(30) NOT NULL,
	MoTaLPC NVARCHAR(100) NOT NULL
)

CREATE TABLE LOAIGHE(
	MaGhe INT IDENTITY(1,1) PRIMARY KEY,
	TenLG CHAR(10) NOT NULL,
	GiaLGhe FLOAT NOT NULL
)

CREATE TABLE PHONGCHIEU(
	MaPC INT IDENTITY(1,1) PRIMARY KEY,
	TenPC NVARCHAR(30) NOT NULL,
	SLGheThuong SMALLINT CHECK (SLGheThuong>=1) NOT NULL,
	SLGheVIP SMALLINT CHECK (SLGheVIP>=1) NOT NULL,
	MaLPC INT REFERENCES LOAIPC(MaLPC)
)

--chưa làm
CREATE TABLE GHECUAPC(
	MaPC INT REFERENCES PHONGCHIEU(MaPC),
	MaGhe INT REFERENCES LOAIGHE(MaGhe),
	TrangThaiGhePC NVARCHAR(50) NOT NULL,
	TenGhePC CHAR(10) NOT NULL,
	PRIMARY KEY (MaPC, MaGhe)
)

CREATE TABLE XUATCHIEU(
	MaXC INT IDENTITY(1,1) PRIMARY KEY,
	CaXC CHAR(10),
	GioXC TIME NOT NULL
)

CREATE TABLE LICHCHIEU(
	MaLC INT IDENTITY(1,1) PRIMARY KEY,
	NgayLC DATE NOT NULL,
	TrangThaiLC NVARCHAR(50) NOT NULL,
	SLVeDat INT CHECK (SLVeDat>=0) NOT NULL,
	MaXC INT REFERENCES XUATCHIEU(MaXC), --sai here, HPhước đã sửa
	MaPC INT REFERENCES PHONGCHIEU(MaPC),
	MaPhim INT REFERENCES PHIM(MaPhim)
)

--CREATE TABLE LICHCHIEU_PHONG (
--	MALC_P INT IDENTITY(1, 1) PRIMARY KEY,
--	MaLC INT REFERENCES LICHCHIEU(MaLC)
--	MaPC INT REFERENCES PHONGCHIEU(MaPC),
--)

CREATE TABLE BINHLUAN(
	MaPhim INT REFERENCES PHIM(MaPhim),
	MaKH INT REFERENCES KHACHHANG(MaKH),
	TrangThai NVARCHAR(30) NOT NULL,
	NgayTao DATE NOT NULL,
	PRIMARY KEY (MaPhim, MaKH)
)

CREATE TABLE YEUTHICH(
	MaPhim INT REFERENCES PHIM(MaPhim),
	MaKH INT REFERENCES KHACHHANG(MaKH),
	PRIMARY KEY (MaPhim, MaKH)
)

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

CREATE TABLE VE_GHE(
	MaVG INT IDENTITY (1,1) PRIMARY KEY,
	MaVe INT REFERENCES VEPHIM(MaVe),
	TenGheVG CHAR(10) NOT NULL,
	TrangThaiVG NVARCHAR (MAX) NOT NULL
)

CREATE TABLE HOADON(
	MaHD INT IDENTITY(1,1) PRIMARY KEY,
	NgayTao DATETIME NOT NULL,
	TongGiaHD FLOAT NOT NULL,
	TongGiaSauGiam FLOAT NOT NULL,
	TiLeGG FLOAT NOT NULL,
	MaKH INT REFERENCES KHACHHANG(MaKH),
	MaNV INT REFERENCES NHANVIEN(MaNV)
)

CREATE TABLE CHITIETHD(
	MaVe INT REFERENCES VEPHIM(MaVe),
	MaHD INT REFERENCES HOADON(MaHD),
	SoLuongVe SMALLINT CHECK (SoLuongVe>=1) NOT NULL,
	ThanhTienVe FLOAT NOT NULL,
	PRIMARY KEY (MaVe, MaHD)
)

INSERT INTO GIOIHANTUOI values ('P', N'Mọi lứa tuổi')
INSERT INTO GIOIHANTUOI values ('P13', N'Trên 13 tuổi')
INSERT INTO GIOIHANTUOI values ('P16', N'Trên 16 tuổi')
INSERT INTO GIOIHANTUOI values ('P18', N'Trên 18 tuổi')

INSERT INTO PHIM VALUES
(N'ÁN MẠNG Ở VENICE', 
N'Dựa trên tiểu thuyết Halloween Party của nhà văn Agatha Christie, hành trình phá án của thám tử Hercule Poirot tiếp tục được đưa lên màn ảnh rộng.',
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
(N'ARGYLLE SIÊU ĐIỆP VIÊN', N'Argylle là ai? Duy nhất 1 cách có thể tìm ra câu trả lời. ARGYLLE SIÊU ĐIỆP VIÊN | Dự Kiến Khởi Chiếu - Mùng 1 Tết 10.02.2024',
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

-- Lấy danh sách tất cả các bảng
SELECT table_name
FROM information_schema.tables
WHERE table_type = 'BASE TABLE'

--Dữ liệu thử, xóa khi chạy chính
update phim
set LuotThich = 11234
where MaPhim = 2

--Thêm XUẤT CHIẾU
INSERT INTO XUATCHIEU VALUES ('CA 1', '7:00:00')
INSERT INTO XUATCHIEU VALUES ('CA 2', '10:30:00')
INSERT INTO XUATCHIEU VALUES ('CA 3', '14:00:00')
INSERT INTO XUATCHIEU VALUES ('CA 4', '17:30:00')
INSERT INTO XUATCHIEU VALUES ('CA 5', '21:00:00') -- Ca chiếu lúc 9h là ca đêm cuối cùng, chiếu tới 0:30 hôm sau

--Thêm LOẠI PHÒNG CHIẾU là 2D, mô tả là Phòng 2D
INSERT INTO LOAIPC VALUES ('2D', N'Phòng chiếu 2D')
--Thêm LOẠI PHÒNG CHIẾU là 3D, mô tả là Phòng 3D
INSERT INTO LOAIPC VALUES ('3D', N'Phòng chiếu 3D')

--Thêm PHÒNG CHIẾU 1, 50 ghế thường, 10 ghế vip, loại phòng 2D
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 1', 70, 10, 1) 
--Thêm PHÒNG CHIẾU 2, 50 ghế thường, 10 ghế vip, loại phòng 3D
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 2', 70, 20, 2) 
--Thêm PHÒNG CHIẾU 2, 50 ghế thường, 10 ghế vip, loại phòng 2D
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 3', 50, 10, 1) 
--Thêm PHÒNG CHIẾU 2, 50 ghế thường, 10 ghế vip, loại phòng 3D
INSERT INTO PHONGCHIEU VALUES (N'PHÒNG CHIẾU 4', 50, 10, 2) 


--Truy xuất lịch chiếu theo mã phim
SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = 1 AND LOAIPC.TenLPC = '2D' AND XUATCHIEU.MaXC = LICHCHIEU.MaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '2023-10-20' ORDER BY GioXC ASC

SELECT DISTINCT XUATCHIEU.GioXC FROM LICHCHIEU, PHONGCHIEU, LOAIPC, XUATCHIEU WHERE MaPhim = 1 AND LOAIPC.TenLPC = '3D' AND XUATCHIEU.MaXC = LICHCHIEU.MaXC AND LICHCHIEU.MaPC = PHONGCHIEU.MaPC AND PHONGCHIEU.MaLPC = LOAIPC.MaLPC AND LICHCHIEU.NgayLC = '2023-10-20' ORDER BY GioXC ASC

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
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 1, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 1, 4, 1)

--Insert account mẫu

INSERT INTO LOAIKH VALUES ('Normal', 0.0)
INSERT INTO LOAIKH VALUES ('VIP', 10/100.0)

INSERT INTO KHACHHANG VALUES ('demo_khachhang', '123456', 'dfzdfg@gmail.com', 0, 'ACTIVE', 1);
INSERT INTO KHACHHANG VALUES ('demo_khachhang1', '123456', 'dfzdfg@gmail.com', 0, 'ACTIVE', 2);
INSERT INTO LAOINV VALUES (N'QL Sản phẩm')
INSERT INTO NHANVIEN VALUES (N'Phước', 'phuoc@gmail.com', '12345678', 'Actived', 1)

INSERT INTO VEPHIM VALUES (GETDATE(), N'CHƯA THANH TOÁN', N'CHƯA HẾT HẠN', 6, 6*50000, 1, 1);
INSERT INTO VEPHIM VALUES (GETDATE(), N'CHƯA THANH TOÁN', N'CHƯA HẾT HẠN', 1, 1*50000, 2, 1);

INSERT INTO VE_GHE VALUES (1, 'C1', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (1, 'C2', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (1, 'C3', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (1, 'C4', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (1, 'C5', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (1, 'C6', N'CHƯA THANH TOÁN')
INSERT INTO VE_GHE VALUES (2, 'C6', N'CHƯA THANH TOÁN')


--Truy xuất dữ liệu từ các bảng
SELECT* FROM LOAIKH
SELECT* FROM KHACHHANG
SELECT* FROM NHANVIEN
SELECT* FROM THELOAIP
SELECT* FROM TL_P
SELECT* FROM GIOIHANTUOI
SELECT* FROM PHIM
SELECT* FROM LOAIGHE
SELECT* FROM GHECUAPC
SELECT* FROM XUATCHIEU
SELECT* FROM LOAIPC
SELECT* FROM LICHCHIEU ORDER BY MaPhim asc, MaXC asc, MaPC asc
SELECT* FROM PHONGCHIEU
SELECT* FROM BINHLUAN
SELECT* FROM YEUTHICH
SELECT* FROM VEPHIM
SELECT* FROM VE_GHE
SELECT* FROM HOADON
SELECT* FROM CHITIETHD

SELECT LICHCHIEU.*, PHONGCHIEU.SLGheThuong, PHONGCHIEU.SLGheVIP, XUATCHIEU.GioXC, LOAIPC.TenLPC FROM LICHCHIEU, XUATCHIEU, PHIM, PHONGCHIEU, LOAIPC WHERE LOAIPC.TenLPC = '3D' AND LOAIPC.MaLPC = PHONGCHIEU.MaLPC AND PHONGCHIEU.MaPC = LICHCHIEU.MaPC AND PHIM.MaPhim = LICHCHIEU.MaPhim AND LICHCHIEU.MaXC = XUATCHIEU.MaXC AND LICHCHIEU.NgayLC = '2023-10-23' AND LICHCHIEU.MaPhim = 1 AND XUATCHIEU.GioXC = '7:00:00'

SELECT VE_GHE.* FROM VEPHIM, VE_GHE WHERE VEPHIM.MaLC = 1 AND VEPHIM.MaVe = VE_GHE.MaVe

SELECT*
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE='BASE TABLE'

--Dữ liệu thử, xóa khi chạy chính
update phim
set LuotThich = 11234
where MaPhim = 2

sELECT * FROM VE_GHE vg, GHECUAPC gpc WHERE vg.TenGheVG=gpc.TenGhePC AND vg.MaVe = 123;

INSERT INTO LICHCHIEU VALUES ('2-2-2023', 'Active', 0, 1, 1 ,1)

--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 1, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES('20231027', 'ENABLE', 0, 1, 4, 1)

INSERT INTO LAOINV VALUES (N'NV Kỹ thuật')

INSERT INTO NHANVIEN VALUES (N'ABC', 'phuoc@gmail.com', '12345678', 'Active', '1')

INSERT INTO HOADON VALUES (GETDATE(), 50000, 50000, 0, 1, 1);
INSERT INTO HOADON VALUES (GETDATE(), 60000, 60000, 0, 1, 1);
INSERT INTO HOADON VALUES (GETDATE(), 55000, 55000, 0, 1, 1);
INSERT INTO HOADON VALUES (GETDATE(), 500000, 500000, 0, 1, 1);

INSERT INTO CHITIETHD VALUES (1, 5, 1, 120000);
INSERT INTO CHITIETHD VALUES (2, 5, 1, 120000);
INSERT INTO CHITIETHD VALUES (1, 6, 3, 120000);
INSERT INTO CHITIETHD VALUES (1, 7, 2, 50000);
INSERT INTO CHITIETHD VALUES (2, 7, 1, 50000);

SELECT kh.TenTKKH, COUNT(*) as SLHD FROM HOADON hd, KHACHHANG kh WHERE hd.MaKH = kh.MaKH GROUP BY kh.TenTKKH

select cthd.* from HOADON hd, CHITIETHD cthd, VEPHIM vp where hd.MaHD=5 and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe

SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'

DELETE FROM VE_GHE WHERE MaVe = 4 DELETE FROM VEPHIM WHERE MaVe = 4

SELECT COUNT(*) FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH = kh.MaKH AND kh.TenTKKH = 'phuoc' AND TrangThaiThanhToan = N'CHƯA THANH TOÁN'

SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='{tentk}' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'

DECLARE @chietkhau FLOAT, @makh INT
SELECT @chietkhau=lkh.ChietKhau FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH AND kh.TenTKKH = 'phuoc'
SELECT @makh = MaKH FROM KHACHHANG WHERE TenTKKH = 'phuoc'
INSERT INTO HOADON VALUES (GETDATE(), 1000, 1000-(1000*@chietkhau), @chietkhau, @makh, 1)

DECLARE @mahd INT
SELECT @mahd=MAX(MaHD) FROM HOADON
INSERT INTO CHITIETHD VALUES (2, @mahd, 1, 1)

SELECT COUNT(vg.MaVG) FROM VEPHIM vp, VE_GHE vg WHERE vp.MaVe=vg.MaVe AND vp.MaKH = 1 AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'

SELECT vp.MaVe FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='phuoc'

UPDATE VEPHIM SET TrangThaiThanhToan = N'ĐÃ THANH TOÁN' WHERE MaVe = 1

SELECT p.TenPhim, p.HinhAnh, vp.GiaVe, lc.NgayLC, vp.NgayDat, vp.MaVe FROM VEPHIM vp, LICHCHIEU lc, PHIM p, KHACHHANG kh WHERE vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim AND kh.MaKH=vp.MaKH AND kh.TenTKKH='phuoc' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'

SELECT * FROM VE_GHE WHERE MaVe = 1


select vp.NgayDat, vp.GiaVe, lc.NgayLC, p.TenPhim from HOADON hd, CHITIETHD cthd, VEPHIM vp, LICHCHIEU lc, PHIM p where hd.MaHD=1 and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe AND vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim

select vg.TenGheVG from VEPHIM vp, HOADON hd, VE_GHE vg, CHITIETHD cthd where hd.MaHD=1 and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe and vp.MaVe=vg.MaVe

select cthd.* from HOADON hd, CHITIETHD cthd where hd.MaHD=1 and hd.MaHD=cthd.MaHD

SELECT vp.MaVe FROM VEPHIM vp, KHACHHANG kh WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='phuoc' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN'

SELECT MaVe FROM VE_GHE WHERE MaVe = 1

select vp.NgayDat, vp.GiaVe, lc.NgayLC, p.TenPhim, vp.MaVe from HOADON hd, CHITIETHD cthd, VEPHIM vp, LICHCHIEU lc, PHIM p where hd.MaHD=1 and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe AND vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim

select cthd.* from VEPHIM vp, HOADON hd, VE_GHE vg, CHITIETHD cthd where hd.MaHD=1 and vg.MaVe=1 and hd.MaHD=cthd.MaHD and vp.MaVe=cthd.MaVe and vp.MaVe=vg.MaVe

--Truy xuất dữ liệu từ các bảng
SELECT* FROM LOAIKH
SELECT* FROM KHACHHANG
SELECT* FROM LAOINV
SELECT* FROM NHANVIEN
SELECT* FROM THELOAIP
SELECT* FROM TL_P
SELECT* FROM GIOIHANTUOI
SELECT* FROM PHIM
SELECT* FROM LOAIGHE
SELECT* FROM GHECUAPC
SELECT* FROM XUATCHIEU
SELECT* FROM LOAIPC
SELECT* FROM LICHCHIEU ORDER BY MaPhim asc, MaXC asc, MaPC asc
SELECT* FROM PHONGCHIEU
SELECT* FROM BINHLUAN
SELECT* FROM YEUTHICH
SELECT* FROM VEPHIM
SELECT* FROM VE_GHE
SELECT* FROM HOADON
SELECT* FROM CHITIETHD
=======
SELECT * FROM PHIM WHERE NgayCongChieu <= GETDATE()

SELECT * FROM PHIM WHERE NgayCongChieu > GETDATE()

SELECT * FROM PHIM WHERE LuotMua >= 10

SELECT vp.MaVe, p.MaPhim FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p WHERE vp.MaKH=kh.MaKH AND kh.TenTKKH='phuoc' AND vp.TrangThaiThanhToan = N'CHƯA THANH TOÁN' AND vp.MaLC=lc.MaLC AND lc.MaPhim=p.MaPhim

UPDATE PHIM SET LuotMua=LuotMua+1 WHERE MaPhim = 5

SELECT kh.TenTKKH, kh.EmailKH, kh.MatKhauKH, lkh.TenLKH FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH

SELECT CONVERT(date, vp.NgayDat) FROM VEPHIM vp, KHACHHANG kh WHERE kh.MaKH=vp.MaKH AND kh.TenTKKH='phuoc' GROUP BY CONVERT(date, vp.NgayDat) ORDER BY CONVERT(date, vp.NgayDat) DESC

SELECT vp.MaVe, p.TenPhim, pc.TenPC, STRING_AGG(vg.TenGheVG, ''), CONVERT(date, vp.NgayDat)
FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p, PHONGCHIEU pc, VE_GHE vg
WHERE kh.TenTKKH='phuoc' 
AND vp.MaKH=kh.MaKH 
AND vp.MaLC=lc.MaLC 
AND lc.MaPhim=p.MaPhim
AND pc.MaPC=lc.MaPC
AND vp.MaVe=vg.MaVe
GROUP BY vp.MaVe, p.TenPhim, pc.TenPC, vp.NgayDat 
ORDER BY vp.NgayDat DESC

UPDATE KHACHHANG SET EmailKH=asd WHERE TenTKKH=asd UPDATE KHACHHANG SET MatKhauKH=asd WHERE TenTKKH=asd



