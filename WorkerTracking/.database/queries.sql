SELECT * FROM status
SELECT * FROM roles
SELECT * FROM teams
 
SELECT * FROM workers_by_teams
SELECT * FROM logs order by TIMESTAMP


/*-- Listar tablas --------------------*/
    SELECT table_catalog, table_name
    FROM information_schema.tables
    WHERE table_schema = 'public'

/*-- Obtener info importante de una tabla --------------------*/
    SELECT table_name, column_name, column_default, is_nullable, data_type,character_maximum_length
    FROM information_schema.columns
    WHERE table_schema = 'public'
    AND table_name   = 'workers'


/*-- Randomizar los valores de los satusId entre 100 y 104 ----------------*/
    UPDATE workers
    SET STATUS_ID = floor(random() * (104-100+1) + 100)::int;

    SELECT count(worker_id) FROM workers_by_teams
