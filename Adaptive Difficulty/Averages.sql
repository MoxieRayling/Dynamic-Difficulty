select 
CAST(e.SHOT_SPEED * 100 as INT) as SSBounds,
HEALTH,
FIRE_RATE,
count(ENEMY_ID),
avg(e.HITS) as hits
from ENEMIES e 
inner JOIN WAVES w on w.WAVE_ID = e.WAVE_ID 
AND w.TEST_ID = e.TEST_ID
WHERE ((e.TEST_ID = 85) OR (e.TEST_ID = 72))  AND ENEMY_COUNT = 6
GROUP by HEALTH
 