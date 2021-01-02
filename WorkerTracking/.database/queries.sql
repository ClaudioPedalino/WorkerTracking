SELECT is_admin, * FROM users
-- update users set is_admin = true where user_name = 'clau@gmail.com' 
SELECT * FROM status
SELECT * FROM roles
SELECT * FROM teams
SELECT * FROM workers 
SELECT * FROM workers_by_teams
SELECT * FROM logs order by TIMESTAMP

SELECT * FROM workers order by last_modification_time desc


SELECT * 
FROM workers_by_teams

/*-- Listar tablas --------------------*/
    SELECT table_catalog, table_name
    FROM information_schema.tables
    WHERE table_schema = 'public'

/*-- Obtener info importante de una tabla --------------------*/
    SELECT table_name, column_name, column_default, is_nullable, data_type,character_maximum_length
    FROM information_schema.columns
    WHERE table_schema = 'public'
    AND table_name = 'workers'

/*-- Randomizar los valores de los satusId entre 100 y 104 ----------------*/
    UPDATE workers
    SET STATUS_ID = floor(random() * (104-100+1) + 100)::int;

/* Ver total workers by rol */
    SELECT DISTINCT r.role_id, r.NAME as role_name, count(w) as total_workers
    FROM roles r
    LEFT JOIN workers w  ON r.role_id = w.role_id
    GROUP BY r.role_id
    ORDER BY r.role_id
 
/* Ver total workers by status */
    SELECT DISTINCT s.STATUS_ID, s.NAME as status_name, count(w) as total_workers
    FROM status s
    LEFT JOIN workers w  ON s.STATUS_ID = w.STATUS_ID
    GROUP BY s.STATUS_ID
    ORDER BY s.STATUS_ID

/* Ver total workers by team */
    SELECT DISTINCT t.NAME as team_name, count(w) as total_workers
    FROM teams t
    LEFT JOIN workers_by_teams wt ON t.team_id = wt.team_id
    LEFT JOIN workers w ON w.worker_id = wt.worker_id
    GROUP BY t.NAME


