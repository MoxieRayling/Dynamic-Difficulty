--select count(wave_id) from waves where TEST_ID = 76 GROUP by ENEMY_COUNT
select count(e.wave_id) from ENEMIES e INNER JOIN WAVES w on w.WAVE_ID = e.WAVE_ID AND w.TEST_ID = e.TEST_ID where w.TEST_ID = 78 group by ENEMY_COUNT