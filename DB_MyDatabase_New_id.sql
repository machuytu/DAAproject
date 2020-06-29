USE master
go
DROP DATABASE IF EXISTS [MyDatabase];
go
CREATE DATABASE [MyDatabase]
Go
use [MyDatabase]
Go

CREATE TABLE [dbo].[dangkyhocphan](
	[iddkhp] [int] IDENTITY(1,1) PRIMARY KEY,
	[namhoc] [varchar](10) NOT NULL,
	[hocky] [int] NOT NULL,
	[thoigianbd] [smalldatetime] UNIQUE NOT NULL,
	[thoigiankt] [smalldatetime] UNIQUE NOT NULL,
	UNIQUE ([namhoc],[hocky]),
);
GO

CREATE TABLE [dbo].[khoa](
	[idkhoa] [int] IDENTITY(1,1) PRIMARY KEY,
	[tenkhoa] [nvarchar](50) NOT NULL,
	[makhoa] [varchar](10) UNIQUE NULL,
	[idgv] [int] NULL,
);
GO

CREATE TABLE [dbo].[giangvien](
	[idgv] [int] IDENTITY(1,1) PRIMARY KEY,
	[hoten] [nvarchar](50) NOT NULL,
	[magv] [varchar](10) UNIQUE NULL,
	[idkhoa] [int] NULL,
	[gioitinh] [nvarchar](5) NOT NULL,
	[diachi] [nvarchar](50) NOT NULL,
	[quequan] [nvarchar](50) NOT NULL,
	[sdt] [varchar](20) NOT NULL,
	[capbac] [nvarchar](20) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[ngaysinh] [date] NOT NULL,
	[ngayvaolam] [date] NOT NULL,
	[hinhanh] [nvarchar](max) NULL,
	FOREIGN KEY ([idkhoa]) REFERENCES [dbo].[khoa]([idkhoa]) ON DELETE SET NULL,
);
GO

CREATE TABLE [dbo].[mon](
	[idmon] [int] IDENTITY(1,1) PRIMARY KEY,
	[tenmon] [nvarchar](30) NOT NULL,
	[mamon] [varchar](10) UNIQUE NULL,
	[idmontruoc] [int] NULL,
	[hsqt] [float] DEFAULT 0,
	[hsth] [float] DEFAULT 0,
	[hsgk] [float] DEFAULT 0,
	[hsck] [float] DEFAULT 1,
	[sotc] [int] DEFAULT 2,
	[tclt] [int] NULL,
	[tcth] [int] NULL,
);
GO

CREATE TABLE [dbo].[lopcn](
	[idlopcn] [int] IDENTITY(1,1) PRIMARY KEY,
	[malopcn] [varchar](10) NULL, --mã khoa + niên khóa
	[idgv] [int] NULL,
	[idkhoa] [int] NULL,
	[nienkhoa] [int] NOT NULL,
	FOREIGN KEY ([idgv]) REFERENCES [dbo].[giangvien]([idgv]) ON DELETE SET NULL,
	FOREIGN KEY ([idkhoa]) REFERENCES [dbo].[khoa]([idkhoa]) ON DELETE SET NULL,
);
GO

CREATE TABLE [dbo].[sinhvien](
	[idsv] [int] IDENTITY(1,1) PRIMARY KEY,
	[hoten] [nvarchar](50) NOT NULL,
	[masv] [varchar](10) UNIQUE NULL,
	[gioitinh] [nvarchar](5) NOT NULL,
	[ngaysinh] [date] NOT NULL,
	[idlopcn] [int] NULL,
	[diachi] [nvarchar](50) NOT NULL,
	[quequan] [nvarchar](50) NOT NULL,
	[sdt] [varchar](20) NOT NULL,
	[bachoc] [nvarchar](30) NULL,
	[hinhanh] [nvarchar](max) NULL,
	FOREIGN KEY ([idlopcn]) REFERENCES [dbo].[lopcn]([idlopcn]) ON DELETE SET NULL,
);
GO

CREATE TABLE [dbo].[taikhoan](
	[idtk] [int] IDENTITY(1,1) PRIMARY KEY,
	[matk] [varchar](10) UNIQUE NULL,
	[password] [nvarchar](50) NOT NULL,
	[nhom] [nvarchar](30) NULL DEFAULT N'Quản trị viên',
	[idsv] [int] NULL,
	[idgv] [int] NULL,
	FOREIGN KEY ([idsv]) REFERENCES [dbo].[sinhvien]([idsv]) ON DELETE CASCADE,
	FOREIGN KEY ([idgv]) REFERENCES [dbo].[giangvien]([idgv]) ON DELETE CASCADE,
);
GO

CREATE TABLE [dbo].[thongbao](
	[idtb] [int] IDENTITY(1,1) PRIMARY KEY,
	[tieude] [nvarchar](100) NULL,
	[noidung] [nvarchar](max) NULL,
	[tag] [nvarchar](30) NOT NULL,
	[idtk] [int] NULL,
	[file] [nvarchar](max) NULL,
	[thoigiandang] [smalldatetime] DEFAULT SYSDATETIME(),
	[thoigiancapnhat] [smalldatetime] NULL,
	FOREIGN KEY ([idtk]) REFERENCES [dbo].[taikhoan]([idtk]) ON DELETE SET NULL,
);
GO

CREATE TABLE [dbo].[lop](
	[idlop] [int] IDENTITY(1,1) PRIMARY KEY,
	[malop] [varchar](20) UNIQUE NULL, -- mã môn + .k + iddkhp + thứ + tiết bd + tiết kt + . mã khoa
	[idgv] [int] NULL,
	[idmon] [int] NULL,
	[idkhoa] [int] NULL,
	[iddkhp] [int] NOT NULL,
	[thu] [int] NOT NULL,
 	[tietbd] [int] NOT NULL,
	[tietkt] [int] NULL,
	FOREIGN KEY ([iddkhp]) REFERENCES [dbo].[dangkyhocphan]([iddkhp]),
	FOREIGN KEY ([idkhoa]) REFERENCES [dbo].[khoa]([idkhoa]) ON DELETE SET NULL,
	FOREIGN KEY ([idmon]) REFERENCES [dbo].[mon]([idmon]) ON DELETE CASCADE,
	FOREIGN KEY ([idgv]) REFERENCES [dbo].[giangvien]([idgv]) ON DELETE SET NULL,
);
GO

CREATE TABLE [dbo].[hoc](
	[idhoc] [int] IDENTITY(1,1) PRIMARY KEY,
	[idlop] [int] NULL,
	[idsv] [int] NULL,
	[diemqt] [float] NULL,
	[diemth] [float] NULL,
	[diemgk] [float] NULL,
	[diemck] [float] NULL,
	[diemtb] [float] NULL,
	UNIQUE ([idsv],[idlop]),
	FOREIGN KEY ([idsv]) REFERENCES [dbo].[sinhvien]([idsv]),
	FOREIGN KEY ([idlop]) REFERENCES [dbo].[lop]([idlop]) ON DELETE CASCADE,
);
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[khoa] 
ADD FOREIGN KEY ([idgv]) REFERENCES [giangvien]([idgv]) ON DELETE SET NULL;
GO
ALTER TABLE [dbo].[mon] 
ADD FOREIGN KEY ([idmontruoc]) REFERENCES [mon]([idmon]);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([hsqt] BETWEEN 0 AND 0.5);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([hsth] BETWEEN 0 AND 0.5);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([hsgk] BETWEEN 0 AND 0.5);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([hsck] BETWEEN 0 AND 1);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([sotc] BETWEEN 2 AND 5);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([tclt] BETWEEN 1 AND 5);
GO
ALTER TABLE [dbo].[mon] 
ADD CHECK ([tcth] IN(0,1));
GO
ALTER TABLE [dbo].[hoc]  
ADD CHECK ([diemqt] BETWEEN 0 AND 10);
GO
ALTER TABLE [dbo].[hoc]  
ADD CHECK ([diemth] BETWEEN 0 AND 10);
GO
ALTER TABLE [dbo].[hoc] 
ADD CHECK ([diemgk] BETWEEN 0 AND 10);
GO
ALTER TABLE [dbo].[hoc]  
ADD CHECK ([diemck] BETWEEN 0 AND 10);
GO
ALTER TABLE [dbo].[hoc]  
ADD CHECK ([diemtb] BETWEEN 0 AND 10);
GO
ALTER TABLE [dbo].[lop] 
ADD CHECK ([thu] BETWEEN 2 AND 7);
GO
ALTER TABLE [dbo].[dangkyhocphan] 
ADD CHECK ([hocky] IN (1,2));
GO
ALTER TABLE [dbo].[dangkyhocphan] 
ADD CHECK ([thoigiankt] > [thoigianbd]);
GO
ALTER TABLE [dbo].[thongbao] 
ADD CHECK ([thoigiancapnhat] >= [thoigiandang]);
GO
ALTER TABLE [dbo].[lop] 
ADD CHECK ([tietbd] BETWEEN 1 AND 10);
GO
ALTER TABLE [dbo].[lop] 
ADD CHECK ([tietkt] BETWEEN 1 AND 10);
GO
ALTER TABLE [dbo].[giangvien] 
ADD CHECK ([ngayvaolam] > [ngaysinh]);
GO
ALTER TABLE [dbo].[giangvien] 
ADD CHECK ([gioitinh] IN (N'Nam',N'Nữ'));
GO
ALTER TABLE [dbo].[sinhvien] 
ADD CHECK ([gioitinh] IN (N'Nam',N'Nữ'));
GO
ALTER TABLE [dbo].[taikhoan]
ADD CHECK ([nhom] IN (N'Giảng viên',N'Sinh viên',N'Quản trị viên'));
GO
ALTER TABLE [dbo].[thongbao] 
ADD CHECK ([tag] in (N'THÔNG BÁO NGHỈ, BÙ', N'THÔNG BÁO CHUNG', N'THÔNG BÁO VỀ PHÒNG HỌC'));
GO

CREATE OR ALTER TRIGGER trg_insertthongbao ON thongbao
FOR INSERT
AS 
BEGIN
	DECLARE @id int;
	SELECT @id = idtb FROM inserted;
	BEGIN
		UPDATE thongbao SET thoigiandang = SYSDATETIME() WHERE idtb = @id;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_updatethongbao ON thongbao
FOR UPDATE
AS 
BEGIN
	DECLARE @id int;
	SELECT @id = idtb FROM inserted;
	BEGIN
		UPDATE thongbao SET thoigiancapnhat = SYSDATETIME() WHERE idtb = @id;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_checkthongbao ON thongbao
FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @idsv int;
	SELECT @idsv = idsv FROM inserted JOIN taikhoan ON inserted.idtk = taikhoan.idtk;
	IF (@idsv IS NOT NULL)
	BEGIN
		RAISERROR(N'Sinh viên không có quyền đăng thông báo',10,12);
		ROLLBACK TRAN;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_insertSV ON sinhvien
FOR INSERT
AS 
DECLARE
	@id int, @masv varchar(10), @idlopcn varchar(10), @nienkhoa varchar(4);
BEGIN
	SELECT @id = idsv, @idlopcn = idlopcn FROM inserted;
	SELECT @nienkhoa = CONVERT(varchar,nienkhoa) FROM lopcn WHERE idlopcn = @idlopcn;
	SET @masv = 'SV' + RIGHT(@nienkhoa,2) + RIGHT('0000' + CONVERT(varchar(4),@id), 4);
	UPDATE sinhvien SET masv = @masv WHERE idsv = @id;
	INSERT INTO taikhoan (matk,[password],idsv,nhom) VALUES (@masv,'1',@id,N'Sinh viên');
END;
GO

CREATE OR ALTER TRIGGER trg_insertGV ON giangvien
FOR INSERT
AS 
BEGIN
	DECLARE @id int,@magv varchar(10);
	SELECT @id = idgv FROM inserted;
	SET @magv = 'GV' + RIGHT('0000' + CONVERT(varchar(4),@id), 4);
	UPDATE giangvien SET magv = @magv WHERE idgv = @id;
	INSERT INTO taikhoan (matk,[password],idgv,nhom) VALUES (@magv,'1',@id,N'Giảng viên');
END;
GO

CREATE OR ALTER TRIGGER trg_gv_khoa ON giangvien
FOR UPDATE
AS 
BEGIN
	DECLARE @id int, @idkhoa int, @idKhoaCuaGV int;
	SELECT @id = idgv,@idKhoaCuaGV = idkhoa FROM inserted
	SELECT @idkhoa = khoa.idkhoa FROM khoa WHERE idgv = @id;
	IF (@idkhoa <> @idKhoaCuaGV AND @idkhoa IS NOT NULL)
	BEGIN
		RAISERROR(N'Trưởng khoa phải là giảng viên thuộc khoa',10,12);
		UPDATE khoa SET idgv = NULL WHERE idkhoa = @idkhoa;
	END;
END;
GO


CREATE OR ALTER TRIGGER trg_checkTrgKhoa ON khoa
FOR INSERT, UPDATE
AS 
BEGIN
	IF EXISTS (
		SELECT * FROM inserted JOIN giangvien ON inserted.idgv = giangvien.idgv
		WHERE inserted.idkhoa <> giangvien.idkhoa)
	BEGIN
		RAISERROR(N'Trưởng khoa phải là giảng viên thuộc khoa',10,12);
		ROLLBACK TRAN;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_insertLopCN ON lopcn
FOR INSERT
AS 
begin
	DECLARE @id int, @makhoa varchar(6), @idgv int, @idkhoa int, @nienkhoa int;
	SELECT @id = idlopcn, @idkhoa = idkhoa, @idgv = idgv, @nienkhoa = nienkhoa FROM inserted;
	SELECT @makhoa = makhoa FROM khoa WHERE idkhoa = @idkhoa;
	UPDATE lopcn SET malopcn = @makhoa + CONVERT(varchar,@nienkhoa) WHERE idlopcn = @id;
end;
GO

CREATE OR ALTER TRIGGER trg_updateLopCN ON lopcn
FOR UPDATE
AS 
begin
	DECLARE @id int,@makhoa varchar(6),@idgv int,@idkhoa int,@nienkhoa int;
	SELECT @id = idlopcn, @idkhoa = idkhoa, @idgv = idgv, @nienkhoa = nienkhoa FROM inserted;
	SELECT @makhoa = makhoa FROM khoa WHERE idkhoa = @idkhoa;
	UPDATE lopcn SET malopcn = @makhoa + CONVERT(varchar,@nienkhoa) WHERE idlopcn = @id;
end;
GO

CREATE OR ALTER TRIGGER trg_checkLopCN ON lopcn
FOR INSERT, UPDATE
AS 
BEGIN
	IF EXISTS ( 
		SELECT * FROM inserted JOIN giangvien ON inserted.idgv = giangvien.idgv
		WHERE inserted.idkhoa <> giangvien.idkhoa)
	BEGIN
		RAISERROR(N'Giảng viên không thuộc khoa',10,15);
		ROLLBACK TRAN;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_changeLopCN ON sinhvien
FOR UPDATE
AS
begin
	DECLARE @id int, @nienkhoainsert int, @nienkhoadelete int;
	SELECT @id = idsv, @nienkhoainsert = nienkhoa  FROM inserted JOIN lopcn ON inserted.idlopcn = lopcn.idlopcn;
	SELECT @nienkhoadelete = nienkhoa  FROM deleted JOIN lopcn ON deleted.idlopcn = lopcn.idlopcn WHERE idsv = @id;
	if (@nienkhoainsert <> @nienkhoadelete)
	begin
		RAISERROR (N'niên khóa không phù hợp',10,2);
		ROLLBACK TRAN;
	end;
end;
GO

CREATE OR ALTER TRIGGER trg_checkMon ON mon
FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @id int, @hsqt float, @hsth float, @hsgk float, @hsck float, @sotc int;

	SELECT @id = idmon, @hsqt = hsqt, @hsth = hsth, @hsgk = hsgk, @hsck = hsck, @sotc = sotc 
	FROM inserted;

	IF (@hsqt + @hsth + @hsgk + @hsck <> 1)
	BEGIN
		RAISERROR(N'Tổng hệ số điểm phải bằng 1',10,14);
		ROLLBACK TRAN;
	END;
	ELSE IF ((@hsck < @hsgk) OR (@hsck < @hsth) OR (@hsck < @hsqt))
	BEGIN
		RAISERROR(N'Hệ số cuối kỳ không thể bé hơn các hệ số khác',10,14);
		ROLLBACK TRAN;
	END;
	ELSE
	BEGIN
		IF (@hsth = 0)
		BEGIN
			UPDATE mon SET tcth = 0, tclt = @sotc WHERE idmon = @id; 
		END;
		ELSE
		BEGIN
			UPDATE mon SET tcth = 1, tclt = @sotc - 1 WHERE idmon = @id; 
		END;
	END;
END;
GO

CREATE OR ALTER TRIGGER trg_deleteMonTrc ON mon
INSTEAD OF DELETE
AS 
BEGIN
	DECLARE @id int;
	SELECT @id = idmon FROM deleted;
	UPDATE mon SET idmontruoc = NULL WHERE idmontruoc = @id;
	DELETE FROM mon WHERE idmon = @id;
END;
GO

CREATE OR ALTER TRIGGER trg_insertLop ON lop
FOR INSERT
AS
BEGIN
	DECLARE @id int, @malop varchar(30),@mamon varchar(10), @makhoa varchar(10), @iddkhp int,@thu int,@tietbd int, @sotc int;
	Select @id = inserted.idlop, @iddkhp = iddkhp, @thu = thu, @tietbd = tietbd, @malop = malop, @mamon = mon.mamon, @sotc = sotc, @makhoa = makhoa
	From (inserted join mon ON inserted.idmon = mon.idmon) JOIN khoa on khoa.idkhoa = inserted.idkhoa;

	UPDATE lop SET malop = @mamon + '.K' + CONVERT(varchar,@iddkhp) + CONVERT(varchar,@thu) 
	+ CONVERT(varchar,@tietbd) + RIGHT(CONVERT(varchar,@tietbd+@sotc-1),1) + '.' + @makhoa, tietkt = @tietbd+@sotc-1
	Where idlop = @id;
END;
GO

CREATE OR ALTER TRIGGER trg_checkLop ON lop
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idgv int,@iddkhp int,@idlop int,@idmon int, @idkhoa int,@thu int,@tietbd int,@tietkt int;

	Select @idgv = idgv, @iddkhp = iddkhp, @thu = thu, @tietbd = tietbd, @tietkt = tietkt, @idlop = idlop, @idkhoa = idkhoa, @idmon = inserted.idmon 
	From inserted join mon ON inserted.idmon = mon.idmon;
	--test

	if (@tietbd <= 5 and @tietkt > 5)
	begin
		raiserror(N'Lịch không hợp lý',10,4);
		rollback transaction;
	end;
	else if exists (select * from lop 
		where idlop <> @idlop and idgv = @idgv and thu = @thu and iddkhp = @iddkhp
		and not (@tietbd > tietkt or @tietkt < tietbd))
	begin
		raiserror(N'Giảng viên trùng lịch',10,4);
		rollback transaction;
	end;
END;
GO

CREATE OR ALTER TRIGGER trg_UpdateHoc ON hoc
FOR UPDATE
AS 
DECLARE @id int, @idlop int, @hsqt float, @hsth float, @hsgk float, @hsck float,
		@diemqt float, @diemth float, @diemgk float, @diemck float, @diemtb float;
BEGIN
	SELECT @id = idhoc, @idlop = idlop, @diemqt = diemqt, @diemth = diemth, @diemgk = diemgk, @diemck = diemck
	FROM inserted;

	SELECT @hsqt = hsqt, @hsth = hsth, @hsgk = hsgk, @hsck = hsck 
	FROM mon JOIN lop ON mon.idmon = lop.idmon WHERE idlop = @idlop;

	IF (@hsqt = 0 AND @diemqt IS NOT NULL)
	BEGIN
		RAISERROR (N'Không có điểm quá trình',1,1000);
		SET @diemqt = NULL;
	END;

	IF (@hsth = 0 AND @diemth IS NOT NULL)
	BEGIN
		RAISERROR (N'Không có điểm thực hành',1,1000);
		SET @diemth = NULL;
	END;

	IF (@hsgk = 0 AND @diemgk IS NOT NULL)
	BEGIN
		RAISERROR (N'Không có điểm giữa kỳ',1,1000);
		SET @diemgk = NULL;
	END;

	SET @diemtb = NULL;
	IF (@diemck IS NOT NULL)
	BEGIN
		IF ((@hsqt <> 0 AND @diemqt IS NULL) OR (@hsth <> 0 AND @diemth IS NULL) OR (@hsgk <> 0 AND @diemgk IS NULL))
		BEGIN
			RAISERROR (N'Chưa nhập đủ điểm',1,100);
			SET @diemck = NULL;
		END;
		ELSE
		BEGIN
			SET @diemtb = @diemck * @hsck + ISNULL(@diemgk,0) * @hsgk + ISNULL(@diemth,0) * @hsth + ISNULL(@diemqt,0) * @hsqt;
		END;
	END;
	
	UPDATE hoc SET diemqt = @diemqt, diemth = @diemth, diemgk = @diemgk, diemck = @diemck, diemtb = @diemtb WHERE idhoc = @id;
END;
GO

CREATE OR ALTER TRIGGER trg_deleteHoc ON hoc
FOR DELETE
AS
DECLARE @diemqt float, @diemth float, @diemgk float, @diemck float;
BEGIN
	SELECT @diemqt = diemqt, @diemth = diemth, @diemgk = diemgk, @diemck = diemck FROM deleted;

	IF ((@diemqt IS NOT NULL) OR (@diemth IS NOT NULL) OR (@diemgk IS NOT NULL) OR (@diemck IS NOT NULL))
	BEGIN
		RAISERROR (N'Không thể hủy đăng ký lớp',10,1000);
		ROLLBACK TRANSACTION;
	END;
END;
GO

/*
CREATE OR ALTER TRIGGER trg_insertHoc ON hoc
FOR INSERT
AS 
BEGIN
	DECLARE @idmon int, @idmontruoc int, @idsv int, @idlop int,@malop varchar(30),@err nvarchar(100) , @thu int, @tietbd int, @tietkt int, @iddkhp int;

	SELECT @idsv = idsv, @idlop = idlop FROM inserted;

	SELECT @idmon = idmon, @malop = malop, @thu = thu, @tietbd = tietbd, @tietkt = tietkt, @iddkhp = iddkhp
	FROM lop WHERE idlop = @idlop;

	SELECT @idmontruoc = idmontruoc FROM mon WHERE idmon = @idmon;

	IF EXISTS (select * from hoc join lop on hoc.idlop = lop.idlop
			where idsv = @idsv and hoc.idlop <> @idlop and iddkhp = @iddkhp and thu = @thu 
			and not (tietbd > @tietkt or tietkt < @tietbd))
	BEGIN
		set @err =N'Lớp ' + @malop + N': Trùng lịch/';
		RAISERROR (@err,16,1);
		ROLLBACK TRANSACTION;
	END;
	ELSE IF EXISTS (select * from hoc join lop on hoc.idlop = lop.idlop
			where idsv = @idsv and hoc.idlop <> @idlop and idmon = @idmon 
			and (diemtb is null or diemtb >= 5)) 
	BEGIN
		set @err =N'Lớp ' + @malop + N': Môn đã học /';
		RAISERROR(@err,16,2)
		ROLLBACK TRANSACTION;
	END;
	ELSE IF NOT EXISTS (select * from hoc join lop on hoc.idlop = lop.idlop
			where idsv = @idsv and idmon = @idmontruoc)	AND ( @idmontruoc is not null )
	BEGIN
		set @err =N'Lớp ' + @malop + N': Chưa học môn học trước /';
		RAISERROR (@err,16,3);
		ROLLBACK TRANSACTION;
	END;
END;
GO

drop TRIGGER trg_insertHoc;
go
*/

CREATE OR ALTER TRIGGER trg_dkhp ON dangkyhocphan
FOR INSERT, UPDATE
AS 
BEGIN
	DECLARE @iddkhp int,  @thoigianbd smalldatetime, @thoigiankt smalldatetime;
	SELECT @iddkhp = iddkhp, @thoigianbd = thoigianbd, @thoigiankt = thoigiankt FROM inserted;

	IF EXISTS (SELECT * FROM dangkyhocphan 
				WHERE iddkhp <> @iddkhp AND NOT( thoigiankt < @thoigianbd OR thoigianbd > @thoigiankt ))
	BEGIN
		RAISERROR(N'Trùng lịch học phần',10,1000);
		ROLLBACK TRAN;
	END;
END;
GO

insert into taikhoan (matk,[password]) values ('admin','1');
go

insert into dangkyhocphan (namhoc,hocky,thoigianbd,thoigiankt) values ('2019-2020',1,'20200105','20200115');
go

insert into dangkyhocphan (namhoc,hocky,thoigianbd,thoigiankt) values ('2020-2021',1,'20200605','20200915');
go

select * from dangkyhocphan;
go

delete from dangkyhocphan where iddkhp NOT IN(1,2); 

insert into khoa (makhoa,tenkhoa) values('HTTT', N'Hệ thống thông tin');
go
insert into khoa (makhoa,tenkhoa) values('KHMT', N'Khoa học máy tính');
go
insert into khoa (makhoa,tenkhoa) values('MMT', N'Mạng máy tính');
go

insert into giangvien (hoten,idkhoa,gioitinh,ngaysinh,ngayvaolam,diachi,quequan,sdt,capbac,email) 
values (N'abc',1,N'Nam','19800101','20000101',N'abc',N'abc','090',N'giáo sư','gv0001@gm.com');
go
insert into giangvien (hoten,idkhoa,gioitinh,ngaysinh,ngayvaolam,diachi,quequan,sdt,capbac,email) 
values (N'xyz',2,N'Nữ','19800101','20000101',N'xyz',N'xyz','090',N'tiến sĩ','gv0002@gm.com');
go
insert into giangvien (hoten,idkhoa,gioitinh,ngaysinh,ngayvaolam,diachi,quequan,sdt,capbac,email) 
values (N'xyz',3,N'Nữ','19800101','20000101',N'xyz',N'xyz','090',N'thạc sĩ','gv0003@gm.com');
go

update khoa set idgv = '1' where makhoa = 'HTTT';
GO

insert into lopcn (idkhoa,idgv,nienkhoa) values (1,1,2017);
go

insert into lopcn (idkhoa,idgv,nienkhoa) values (2,2,2017);
go

insert into lopcn (idkhoa,idgv,nienkhoa) values (2,2,2018);
go

insert into sinhvien (hoten,gioitinh,ngaysinh,idlopcn,diachi,quequan,sdt,bachoc) 
values (N'NVA',N'Nữ','20000101',1,N'abc',N'abc','090',N'xyz');
go
insert into sinhvien (hoten,gioitinh,ngaysinh,idlopcn,diachi,quequan,sdt,bachoc) 
values (N'NVB',N'Nam','20000101',1,N'abc',N'abc','090',N'xyz');
go
insert into sinhvien (hoten,gioitinh,ngaysinh,idlopcn,diachi,quequan,sdt,bachoc) 
values (N'NVC',N'Nữ','20000101',1,N'abc',N'abc','090',N'xyz');
go

insert into mon (mamon,tenmon,sotc,hsck,hsgk,hsth,hsqt) 
values ('MA003',N'Giải tích',5,0.5,0.3,0,0.2);
go
insert into mon (mamon,tenmon,sotc,hsck,hsgk,hsth,hsqt) 
values ('IT001',N'Nhập môn lập trình',4,0.5,0.3,0,0.2);
go
insert into mon (mamon,idmontruoc,tenmon,sotc,hsck,hsgk,hsth,hsqt) 
values ('IT002',2,N'Lập trình hướng đối tượng',4,0.4,0.2,0.2,0.2);
go

insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (1,1,1,1,2,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (3,1,1,1,3,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (2,1,1,1,5,6);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (1,1,1,2,2,6);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (2,1,1,2,2,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (2,1,1,2,4,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (2,1,1,2,3,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (2,1,1,2,5,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (1,2,1,2,2,1);
go
insert into lop (idmon,idgv,idkhoa,iddkhp,thu,tietbd) 
values (3,2,1,2,3,1);
go

insert into hoc (idsv,idlop) values (1,1);
go
insert into hoc (idsv,idlop) values (2,5);
go
insert into hoc (idsv,idlop) values (2,6);
go

update hoc set diemqt = 10, diemgk = 6 ,diemck = 5 where idhoc = 1;
go

insert into thongbao (idtk,tag,tieude,noidung)
values (1,N'THÔNG BÁO CHUNG',N'Test thông báo ...',N'abc...xyz');
go
insert into thongbao (idtk,tag,tieude,noidung)
values (1,N'THÔNG BÁO CHUNG',N'thông báo 1 ...',N'abc...xyz');
go
insert into thongbao (idtk,tag,tieude,noidung)
values (2,N'THÔNG BÁO CHUNG',N'thông báo 2 ...',N'abc...xyz');
go
insert into thongbao (idtk,tag,tieude,noidung)
values (2,N'THÔNG BÁO CHUNG',N'thông báo 3 ...',N'abc...xyz');
go
insert into thongbao (idtk,tag,tieude,noidung,thoigiandang)
values (2,N'THÔNG BÁO CHUNG',N'thông báo 4 ...',N'abc...xyz',null);
go
UPDATE thongbao SET noidung = N'CẬP NHẬT' WHERE idtb = 1002;
GO

select * from taikhoan; 
go
select * from dangkyhocphan; 
go
select * from khoa; 
go
select * from giangvien; 
go
select * from lop;
go
select * from lopcn; 
go
select * from sinhvien; 
go
select * from mon; 
go
select * from hoc; 
go
select * from thongbao; 
go
delete from hoc;
go