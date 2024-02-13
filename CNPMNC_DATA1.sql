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

CREATE TABLE NHANVIEN(
	MaNV INT IDENTITY(1,1) PRIMARY KEY,
	HoTenNV NVARCHAR(100) NOT NULL,
	Email VARCHAR(35) NOT NULL,
	MatKhauNV VARCHAR(30) NOT NULL,
	TrangThaiTKNV NVARCHAR(25) NOT NULL
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

CREATE TABLE BINHLUAN(
	MaBL INT IDENTITY(1,1) PRIMARY KEY,
	MaPhim INT REFERENCES PHIM(MaPhim),
	TenTK VARCHAR(25),
	GhiChu NVARCHAR(MAX) NOT NULL,
	TrangThai NVARCHAR(30) NOT NULL,
	NgayTao DATETIME NOT NULL
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

INSERT INTO PHIM VALUES
(N'BÍ MẬT CỦA ĐỊA NGỤC', N'Trên hành trình khám phá một hang động mới, 
một nhóm thám tử đối diện với những bí ẩn kinh hoàng của địa ngục: 
những thế lực siêu nhiên và những bí mật về quá khứ đen tối.',
GETDATE(),108,0,0,'bimatcuadianguc.png','https://www.youtube.com/watch?v=X5GDdXm6H28',50000,4)

INSERT INTO PHIM VALUES
(N'THẢM HỌA TẠI BIỆT THỰ AMITYVILLE', N'Bí ẩn về ngôi biệt thự 
Amityville tiếp tục khi một gia đình mới chuyển đến và phải đối mặt 
với những hiện tượng siêu nhiên đáng sợ.',
GETDATE(),109,0,0,'thamhoataibietthuamityville.png','https://www.youtube.com/watch?v=rdoAoBwWUVc',50000,3)

INSERT INTO PHIM VALUES
(N'NGÀY TẾT QUỶ DỮ', N'Trong một thị trấn nhỏ, vào đêm giao thừa, 
những sự kiện kỳ quái và tai hại bắt đầu diễn ra, làm sáng tỏ những bí 
mật đen tối của cộng đồng.',
GETDATE(),102,0,0,'ngaytetquydu.png','https://www.youtube.com/watch?v=byTx5l8N49o',50000,3)

INSERT INTO PHIM VALUES
(N'CUỘC CHIẾN CHỐNG THẦN BÍ', N'Một nhóm các chiến binh 
huyền thoại hợp lực để chống lại sự tàn phá của một thế lực tà ác từ 
thế giới khác, trước khi nó hủy diệt mọi sinh vật trên trái đất.',
GETDATE(),101,0,0,'cuocchienchongthanbi.png','https://www.youtube.com/watch?v=JVcN6ZrlYIc',50000,4)

INSERT INTO PHIM VALUES
(N'VÒNG XOÁY TỬ THẦN', N'Khi một người đàn ông bí ẩn xuất hiện và 
đề xuất một trò chơi nguy hiểm, mọi người trong một thị trấn nhỏ 
phải đối mặt với những quyết định đầy tính mạng.',
GETDATE(),102,0,0,'vongxoaytuthan.png','https://www.youtube.com/watch?v=Sy2kP4qQX9Q',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC SỐNG SỐ 9', N'Trong một tương lai không xa, con người 
sống trong một thế giới hoàn toàn số hóa. Nhưng khi một số người bắt 
đầu mất tính toàn vẹn, một cuộc phiêu lưu để tìm hiểu sự thật bắt đầu.',
GETDATE(),103,0,0,'cuocsongso9.png','https://www.youtube.com/watch?v=Jp89C72Ywvk',50000,4)

INSERT INTO PHIM VALUES
(N'THÁM TỬ HOÀNG GIA: VÙNG ĐẤT BÍ ẨN', N'Thám tử Sherlock Holmes và 
cộng sự Dr. Watson bắt đầu một cuộc điều tra kỳ bí vào một vùng đất xa 
xôi, nơi bí mật đen tối đang chờ đợi.',
GETDATE(),104,0,0,'thamtuhoangia_vungdatbiann.png','https://www.youtube.com/watch?v=IwbbB2hyXdE',50000,4)

INSERT INTO PHIM VALUES
(N'NỖI SỢ KINH HOÀNG', N'Khi một phóng viên điều tra bí 
ẩn về một khu rừng huyền bí, cô phát hiện ra sự tồn tại của một thế lực 
tà ác cổ xưa, và cuộc sống của cô đang bị đe dọa.',
GETDATE(),105,0,0,'noiso_kinhhoang.png','https://www.youtube.com/watch?v=hF5tYgdCp8Y',50000,4)

INSERT INTO PHIM VALUES
(N'TÀN TÍCH BÍ ẨN', N'Khi một nhóm khảo cổ học phát hiện 
ra một nghĩa trang cổ đại, họ không ngờ rằng họ đã mở khóa một lời 
nguyền đen tối từ thời xa xưa.',
GETDATE(),106,0,0,'tantichbiann.png','https://www.youtube.com/watch?v=3dz-r7Z7Eoo',50000,4)

INSERT INTO PHIM VALUES
(N'NGÀY PHỤ NỮ THẦN BÍ', N'Khi một phụ nữ bí ẩn xuất hiện trong 
một thị trấn nhỏ, mọi người bắt đầu nhận ra rằng cô có một quyền 
năng siêu nhiên đặc biệt, nhưng cũng mang theo một mối đe dọa đáng sợ.',
GETDATE(),107,0,0,'ngayphunuthanbi.png','https://www.youtube.com/watch?v=rCcbG6f5dE4',50000,4)

INSERT INTO PHIM VALUES
(N'ÁNH SÁNG ĐEN TỐI', N'Khi một loạt các vụ án mạng kỳ lạ xảy ra, 
một nhóm thám tử phải đối mặt với một kẻ thù siêu nhiên vô cùng nguy hiểm.',
GETDATE(),108,0,0,'anhsangdento.png','https://www.youtube.com/watch?v=hZvQsh3u9Fg',50000,4)

INSERT INTO PHIM VALUES
(N'CỔNG TRUY CẬP HUYỀN BÍ', N'Khi một cỗ máy thời gian được phát 
hiện, một nhóm nhà khoa học vô tình mở ra một cánh cửa tới những thế giới 
song song, nhưng cũng mở cánh cửa cho một thế lực tà ác khủng khiếp.',
GETDATE(),109,0,0,'congtruycaphuyenbi.png','https://www.youtube.com/watch?v=UWk5YdTrCpM',50000,4)

INSERT INTO PHIM VALUES
(N'THỊ TRẤN ÁM ẢNH', N'Khi một nhóm người dân phát hiện ra rằng họ bị 
mắc kẹt trong một vòng lặp thời gian đen tối, họ phải tìm cách để thoát 
ra trước khi bị mất hết mọi hi vọng.',
GETDATE(),110,0,0,'thitranam_athanh.png','https://www.youtube.com/watch?v=J_3Fm6zUhfo',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC CHIẾN ẨN DANH', N'Khi một loạt các vụ án mạng bí ẩn 
xảy ra, một thám tử tài ba phải đối mặt với một kẻ thù tinh ranh và đầy bí ẩn.',
GETDATE(),111,0,0,'cuocchien_andanh.png','https://www.youtube.com/watch?v=jrUAXxTy_Rw',50000,4)

INSERT INTO PHIM VALUES
(N'BIỆT ĐỘI SỨ GIẢ', N'Sau khi một người ngoài hành tinh tà ác tấn công 
trái đất, một nhóm các anh hùng không tưởng được tập hợp để chống lại họ và cứu thế giới.',
GETDATE(),112,0,0,'bietdoisugia.png','https://www.youtube.com/watch?v=wF_gfu5N-y0',50000,4)

INSERT INTO PHIM VALUES
(N'BÓNG ĐÊM KHIẾN ÁM ẢNH', N'Khi một loạt các vụ án mạng kinh hoàng xảy ra trong một
khu phố yên bình, một nhóm thám tử phải đối mặt với một thế lực tà ác đang rình rập trong bóng tối.',
GETDATE(),113,0,0,'bongdemkienamanh.png','https://www.youtube.com/watch?v=7D_xiLvJf3o',50000,4)

INSERT INTO PHIM VALUES
(N'THÀNH PHỐ ÁM ẢNH', N'Khi một thành phố bị bao phủ bởi bóng tối và sự tuyệt vọng, 
một nhóm anh hùng không lấy gì làm ngại ngần đứng lên chống lại thế lực tà ác.',
GETDATE(),114,0,0,'thanhphoamanh.png','https://www.youtube.com/watch?v=MYrFYh2iXI0',50000,4)

INSERT INTO PHIM VALUES
(N'TÀN TÍCH CỦA QUỶ', N'Khi một nhóm khảo cổ học khám phá một ngôi mộ cổ, họ không 
ngờ rằng họ đã mở ra một lời nguyền cổ xưa đầy nguy hiểm và đẫm máu.',
GETDATE(),115,0,0,'tantichcuaquy.png','https://www.youtube.com/watch?v=dZIcX0nrTsw',50000,4)

INSERT INTO PHIM VALUES
(N'HUYỀN BÍ SÂN CHƠI', N'Khi một nhóm trẻ em khám phá một công viên bỏ hoang, 
họ phát hiện ra rằng nơi đây đang ẩn chứa những bí mật tăm tối và nguy hiểm.',
GETDATE(),116,0,0,'huyenbisancoi.png','https://www.youtube.com/watch?v=2Tl1tkTkB-Y',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC CHIẾN TRONG BÓNG TỐI', N'Khi một thế lực tà ác từ thế giới ngầm bắt đầu 
tấn công thế giới loài người, một nhóm anh hùng phải đứng lên chống lại họ để bảo vệ sự tồn vong của loài người.',
GETDATE(),117,0,0,'cuocchientrongbongtoi.png','https://www.youtube.com/watch?v=vH6B1CnWLZM',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC SỐNG TRONG HUYỀN BÍ', N'Khi một nhóm người sống trong một thế 
giới huyền bí nơi mọi điều kỳ diệu đều có thể xảy ra, họ phải đối mặt với những nguy hiểm đáng sợ để bảo vệ sự tồn vong của họ.',
GETDATE(),118,0,0,'cuocsongtronghuyenbi.png','https://www.youtube.com/watch?v=CMi58fhp0YI',50000,4)

INSERT INTO PHIM VALUES
(N'THÀNH PHỐ ĐEN TỐI', N'Khi một thế lực tà ác từ thế giới ngầm trỗi 
dậy và bắt đầu lan tràn ra thế giới loài người, một nhóm anh hùng phải đứng lên 
chống lại họ để bảo vệ sự tồn vong của loài người.',
GETDATE(),119,0,0,'thanhphodento.png','https://www.youtube.com/watch?v=7PPS-TREtQg',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC SỐNG TRONG BÓNG TỐI', N'Khi một nhóm người sống trong 
một thế giới bị che phủ bởi bóng tối, họ phải tìm cách để thoát khỏi sự hiểm nguy 
và bảo vệ sự tồn vong của mình.',
GETDATE(),120,0,0,'cuocsongtrongbongtoi.png','https://www.youtube.com/watch?v=Z1bXEKTmFy8',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC CHIẾN TRONG BÓNG ĐÊM', N'Khi một thế lực tà ác từ thế 
giới ngầm bắt đầu tấn công thế giới loài người vào ban đêm, một nhóm 
anh hùng phải đứng lên chống lại họ để bảo vệ sự tồn vong của loài người.',
GETDATE(),121,0,0,'cuocchientrongbongdem.png','https://www.youtube.com/watch?v=hbdIWw9kcwQ',50000,4)

INSERT INTO PHIM VALUES
(N'ĐƯỜNG HUYỀN BÍ', N'Khi một nhóm nhà thám hiểm bước vào một khu 
rừng bí ẩn, họ không ngờ rằng họ sẽ phải đối mặt với những nguy hiểm 
đáng sợ từ những thế lực siêu nhiên.',
GETDATE(),122,0,0,'duonghuyenbi.png','https://www.youtube.com/watch?v=pLs2dWeqPmM',50000,4)

INSERT INTO PHIM VALUES
(N'CUỘC CHIẾN ẨN DANH II', N'Khi một loạt các vụ án mạng bí ẩn xảy ra, 
một thám tử tài ba phải đối mặt với một kẻ thù tinh ranh và 
đầy bí ẩn một lần nữa.',
GETDATE(),123,0,0,'cuocchien_andanh2.png','https://www.youtube.com/watch?v=3l_Lo3Y5TVQ',50000,4)

INSERT INTO PHIM VALUES
(N'BÓNG TỐI KÝ ỨC', N'Khi một người phụ nữ mất trí nhớ bắt đầu 
tìm hiểu về quá khứ của mình, cô phát hiện ra rằng có những 
bí mật đen tối mà ai đó đang cố gắng che giấu.',
GETDATE(),124,0,0,'bongtoikyuc.png','https://www.youtube.com/watch?v=PkBiZzT8pI0',50000,4)

INSERT INTO PHIM VALUES
(N'KHO BÁU TRONG RỪNG SÂU', N'Khi một nhóm trẻ em phát hiện ra một 
kho báu bí ẩn ở một khu rừng sâu, họ không ngờ rằng họ sẽ phải đối mặt 
với những nguy hiểm đáng sợ từ những thế lực siêu nhiên.',
GETDATE(),125,0,0,'khobautrongrungsau.png','https://www.youtube.com/watch?v=OzNAP6egLaQ',50000,4)

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
--Thêm LICHCHIEU vào ngày 20-10-2023, 'ENABLE', số vé là 0, Ca chiếu 1, Phòng 4(3D), Mã phim 1
INSERT INTO LICHCHIEU VALUES(GETDATE(), 'ENABLE', 0, 1, 4, 1)

--Insert account mẫu

INSERT INTO LOAIKH VALUES ('Normal', 0.0)
INSERT INTO LOAIKH VALUES ('VIP', 10/100.0)
INSERT INTO NHANVIEN VALUES (N'Phước', 'phuoc1@gmail.com', 12345678, 'ACTIVE')

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
SELECT* FROM LICHCHIEU
SELECT* FROM PHONGCHIEU
SELECT* FROM BINHLUAN
SELECT* FROM YEUTHICH
SELECT* FROM VEPHIM
SELECT* FROM VE_GHE
SELECT* FROM HOADON
SELECT* FROM CHITIETHD
