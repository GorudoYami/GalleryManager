WITH cte AS (
    SELECT
        `hash`,
        COUNT(*) `count`
    FROM `videos`
    GROUP BY 
        `hash`
    HAVING 
        COUNT(*) > 1
)
SELECT 
    *
FROM `videos`
    INNER JOIN cte ON 
        cte.`hash` = `videos`.`hash`
ORDER BY 
    `videos`.`hash`;