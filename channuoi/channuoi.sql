Create database quanlychannuoi;

-- tạo bảng tài khoản cho người quản lý hệ thống
create table taikhoan(
id int primary key IDENTITY(1,1),
gmail nvarchar(50) not null,
mk nvarchar(50) not null,
role int not null,
ten nvarchar(50) not null
);
select*from cosokhaonghiem;
-- tạo bảng cơ sở sản xuất
create table cososanxuat(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
quymo int,
diachi nvarchar(50));
drop table cosokhaonghiem;
use quanlychannuoi;
select*from cosokhaonghiem;
-- tạo bảng cơ sở khảo nghiệm
create table cosokhaonghiem(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
diachi nvarchar(50));
 
-- tạo bảng sản phẩm
create table sanpham(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
idsanxuat int foreign key REFERENCES cososanxuat(id),
idkhaonghiem int foreign key REFERENCES cosokhaonghiem(id)
);
drop table sanpham;
 select*from sanpham;


-- tạo bảng chi cục thú y
create table chicucthuy(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoidaidien nvarchar(50),
email nvarchar(50),
phone int,
diachi nvarchar(50));
select*from chicucthuy;
ALTER TABLE chicucthuy
RENAME COLUMN nguoidaidien TO nguoilienhe;
ALTER TABLE chicucthuy
ALTER COLUMN diachi NVARCHAR(255);

EXEC sp_rename 'chicucthuy.nguoidaidien', 'nguoilienhe', 'COLUMN';

-- tạo bảng cơ sở giết mổ
create table cosogietmo(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
diachi nvarchar(50),
idchicuc int foreign key references chicucthuy(id));

-- tạo bảng khu giam giữ
create table khugiamgiu(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
diachi nvarchar(50),
idchicuc int foreign key references chicucthuy(id));

-- tạo bảng đại lý bán thuốc
create table dailybanthuoc(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
diachi nvarchar(50),
idchicuc int foreign key references chicucthuy(id));

--tạo bảng tổ chức chứng nhận
create table tochucchungnhan(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoilienhe nvarchar(50),
email nvarchar(50),
phone int,
);

--tạo bảng cơ sở chế biến sản phẩm chăn nuôi
create table cosochebiensanphamchannuoi(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
nguoidaidien nvarchar(50),
email nvarchar(50),
phone int,
congsuat int);
EXEC sp_rename 'cosochebiensanphamchannuoi.nguoidaidien', 'nguoilienhe', 'COLUMN';
-- tạo bảng giấy chứng nhận
create table chungnhansanxuat(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
idchebien int foreign key REFERENCES cosochebiensanphamchannuoi(id),
idchungnhan int foreign key REFERENCES tochucchungnhan(id)
);

-- tạo bảng vùng chăn nuôi
create table vungchannuoi(
id int primary key IDENTITY(1,1),
diadiem nvarchar(50),
trangthai bit
);

-- tạo bảng nhân lực
create table nhanluc(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
email nvarchar(50),
idhochannuoi int foreign key REFERENCES vungchannuoi(id)
);

-- tạo bảng hộ chăn nuôi nhỏ lẻ
create table hochannuoi(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
email nvarchar(50),
phone int,
dieukienchannuoi nvarchar(50),
idvungchannuoi int foreign key REFERENCES vungchannuoi(id));

-- tạo bảng chứng nhận chăn nuôi
create table chungnhanchannuoi(
id int primary key IDENTITY(1,1),
ten nvarchar(50),
idhochannuoi int foreign key REFERENCES hochannuoi(id),
idchungnhan int foreign key REFERENCES tochucchungnhan(id)
);

