# Hướng dẫn chạy .NET Core MVC Project và khởi tạo cơ sở dữ liệu trong SQL Server Management Studio (SSMS)

## Tạo cơ sở dữ liệu mới trong SQL Server Management Studio (SSMS)

1. Mở SQL Server Management Studio:

* Khởi chạy SSMS: Từ Start Menu hoặc Desktop.

2. Kết nối với Máy chủ:

Kết nối với máy chủ của bạn: Nhập tên máy chủ và phương thức xác thực của bạn, sau đó nhấp vào "Kết nối".

3. Tạo cơ sở dữ liệu mới:

* Nhấp chuột phải vào Cơ sở dữ liệu: Trong Object Explorer, nhấp chuột phải vào nút "Cơ sở dữ liệu".
* Chọn Cơ sở dữ liệu mới: Nhập tên cơ sở dữ liệu và nhấp vào "OK."

4. Thực thi khởi tạo cơ sở dữ liệu

* Nhấp chuột phải vào tên cơ sở dữ liệu đã tạo ở bước trước
* Chọn Truy vấn mới
* Sao chép và Dán nội dung init.sql rồi nhấn Thực thi


5. Cập nhật ConnectionString trong appsinstall.json

Mẹo: bạn có thể thực hiện truy vấn này trong SSMS để nhận chuỗi kết nối:

```
select
    'data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
from sys.server_principals
where name = suser_name()
```

Then you can copy result of the query to DefaultConnection section in appsetting.

## Database schema

Bên trong thư mục mã nguồn, truy cập vào thư mục docs: databaseschema.png


