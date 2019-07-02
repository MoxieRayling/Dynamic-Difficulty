SELECT *, 
count(ENEMIES.ENEMY_ID) as enemyCount,
avg(ENEMIES.SHOT_SPEED) as aShotsSpeed,
avg(ENEMIES.FIRE_RATE) as aFireRate,
avg(ENEMIES.HEALTH) as aHealth, 
avg(ENEMIES.SHOTS_FIRED) as aShots, 
avg(ENEMIES.LIFETIME) as aLifetime, 
ENEMIES.HITS/avg(ENEMIES.LIFETIME) as HitRate, 
avg(ENEMIES.SHOT_SPEED*HEALTH/ENEMIES.FIRE_RATE) as probability
FROM ENEMIES
INNER JOIN WAVES on WAVES.WAVE_ID = ENEMIES.WAVE_ID AND WAVES.TEST_ID = ENEMIES.TEST_ID
WHERE WAVES.TEST_ID = 71
GROUP BY ENEMIES.HITS
HAVING enemyCount > 10
;