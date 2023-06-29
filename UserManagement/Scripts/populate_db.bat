@ECHO OFF
ECHO "Populating DB"
TYPE CreateMySqlDB.sql.txt | docker exec -i aka-foods-mysql mysql -uakafood_main -p4K4-F@@d-pWd
ECHO "Showing tables"
ECHO use akafood_db; show tables; | docker exec -i aka-foods-mysql mysql -uakafood_main -p4K4-F@@d-pWd
TIMEOUT 10