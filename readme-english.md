# Guide to Run .NET Core MVC Project and Initialize a Database in SQL Server Management Studio (SSMS)

## Creating a New Database in SQL Server Management Studio (SSMS)

1. Open SQL Server Management Studio:

* Launch SSMS: From the Start Menu or Desktop.

2. Connect to a Server:

Connect to Your Server: Enter your server name and authentication method, then click "Connect."

3. Create a New Database:

* Right-click on Databases: In Object Explorer, right-click on the "Databases" node.
* Select New Database: Enter the database name and click "OK."

4. Execute initialize Database

* Right-click on database name which created in previous step
* Select New Query
* Copy and Paste init.sql content and press Execute


5. Update ConnectionString in appsetting.json

Tip: you can execute this query in SSMS to get connection string:

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

