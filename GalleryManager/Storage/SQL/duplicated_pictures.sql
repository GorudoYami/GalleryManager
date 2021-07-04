WITH cte AS (
    SELECT
        `hash`,
        COUNT(*) `count`
    FROM `pictures`
    GROUP BY 
        `hash`
    HAVING 
        COUNT(*) > 1
)
SELECT 
    *
FROM `pictures`
    INNER JOIN `cte` ON 
        `cte`.`hash` = `pictures`.`hash`
ORDER BY 
    `pictures`.`hash`;