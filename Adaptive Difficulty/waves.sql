SELECT WAVE_ID, Hits,ENEMY_COUNT as eCount,
max([1]) AS FireRate1,max([2]) AS ShotSpeed1,max([3]) AS Health1,
max([4]) AS FireRate2,max([5]) AS ShotSpeed2,max([6]) AS Health2,
max([7]) AS FireRate3,max([8]) AS ShotSpeed3,max([9]) AS Health3,
max([10]) AS FireRate4,max([11]) AS ShotSpeed4,max([12]) AS Health4,
max([13]) AS FireRate5,max([14]) AS ShotSpeed5,max([15]) AS Health5,
max([16]) AS FireRate6,max([17]) AS ShotSpeed6,max([18]) AS Health6
FROM
(
       SELECT WAVE_ID, Hits, ENEMY_COUNT,
	   CASE WHEN EnemyWaveID = 1 THEN FIRE_RATE ELSE NULL END AS [1],
       CASE WHEN EnemyWaveID = 1 THEN SHOT_SPEED ELSE NULL END AS [2],
       CASE WHEN EnemyWaveID = 1 THEN HEALTH ELSE NULL END AS [3],
       CASE WHEN EnemyWaveID = 2 THEN FIRE_RATE ELSE NULL END AS [4],
       CASE WHEN EnemyWaveID = 2 THEN SHOT_SPEED ELSE NULL END AS [5],
       CASE WHEN EnemyWaveID = 2 THEN HEALTH ELSE NULL END AS [6],
       CASE WHEN EnemyWaveID = 3 THEN FIRE_RATE ELSE NULL END AS [7],  
       CASE WHEN EnemyWaveID = 3 THEN SHOT_SPEED ELSE NULL END AS [8],  
       CASE WHEN EnemyWaveID = 3 THEN HEALTH ELSE NULL END AS [9],  
       CASE WHEN EnemyWaveID = 4 THEN FIRE_RATE ELSE NULL END AS [10],  
       CASE WHEN EnemyWaveID = 4 THEN SHOT_SPEED ELSE NULL END AS [11],  
       CASE WHEN EnemyWaveID = 4 THEN HEALTH ELSE NULL END AS [12],  
       CASE WHEN EnemyWaveID = 5 THEN FIRE_RATE ELSE NULL END AS [13],  
       CASE WHEN EnemyWaveID = 5 THEN SHOT_SPEED ELSE NULL END AS [14],  
       CASE WHEN EnemyWaveID = 5 THEN HEALTH ELSE NULL END AS [15],  
       CASE WHEN EnemyWaveID = 6 THEN FIRE_RATE ELSE NULL END AS [16],  
       CASE WHEN EnemyWaveID = 6 THEN SHOT_SPEED ELSE NULL END AS [17],  
       CASE WHEN EnemyWaveID = 6 THEN HEALTH ELSE NULL END AS [18]
	   FROM
              (
                     SELECT w.WAVE_ID,w.HITS as Hits, ENEMY_COUNT,row_number() over (partition by w.WAVE_ID order by ENEMY_ID) AS EnemyWaveID,e.SHOT_SPEED, e.FIRE_RATE, e.HEALTH FROM WAVES w
                     INNER JOIN ENEMIES e ON e.WAVE_ID = w.WAVE_ID AND e.TEST_ID = w.TEST_ID
					 WHERE e.TEST_ID = 72
              ) x
) y
GROUP BY WAVE_ID