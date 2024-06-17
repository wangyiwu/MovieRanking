# 기존 .NET Core MVC 프로젝트 실행 및 SQL Server Management Studio (SSMS)에서 데이터베이스 초기화 가이드

## SQL Server Management Studio (SSMS)에서 새 데이터베이스 생성

1. SQL Server Management Studio 열기:

* SSMS 실행: 시작 메뉴 또는 데스크톱에서 SSMS를 실행합니다.

2. 서버에 연결:

서버에 연결: 서버 이름과 인증 방법을 입력한 후 "연결"을 클릭합니다.

3. 새 데이터베이스 생성:

* Databases에서 오른쪽 클릭: 개체 탐색기에서 "Databases" 노드를 마우스 오른쪽 버튼으로 클릭합니다.
* 새 데이터베이스 선택: 데이터베이스 이름을 입력하고 "확인"을 클릭합니다.

4. 데이터베이스 초기화 실행

* 이전 단계에서 생성한 데이터베이스 이름을 마우스 오른쪽 버튼으로 클릭합니다.
* 새 쿼리 선택
* init.sql 내용을 복사하여 붙여넣고 실행 버튼을 누릅니다.


5. appsetting.json에서 ConnectionString 업데이트

팁: SSMS에서 이 쿼리를 실행하여 연결 문자열을 얻을 수 있습니다:

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


