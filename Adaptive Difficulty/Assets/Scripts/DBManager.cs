using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class DBManager
{
    private IDbConnection dbcon;
    private string connection;

    public DBManager()
    {
        connection = "URI=file:" +  "C:/UserS/Simurgh/Documents/GitHub/Dynamic-Difficulty/Adaptive Difficulty/" + "My_Database.db";
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        CreateTests();
        CreateWaves();
        CreatePlayers();
        CreateEnemies();
        CreateHits();
    }

    private void CreateTests()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS TESTS (" +
          "TEST_ID INTEGER PRIMARY KEY NOT NULL, " +
          "DESC TEXT NOT NULL, " +
          "DATE TEXT NOT NULL "+
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateWaves()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS WAVES (" +
          "WAVE_ID INTEGER NOT NULL, " +
          "TEST_ID INTEGER NOT NULL, " +
          "ENEMY_COUNT INTEGER NOT NULL, " +
          "HITS INTEGER NOT NULL, " +
          "DURATION INTEGER NOT NULL, " +
          "SHOTS_FIRED INTEGER NOT NULL, " +
          "PRIMARY KEY (WAVE_ID, TEST_ID) " +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreatePlayers()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS PLAYERS (" +
          "WAVE_ID INTEGER NOT NULL, " +
          "TEST_ID INTEGER NOT NULL, " +
          "TARGET_MODE TEXT NOT NULL, " +
          "REACTION_SPEED REAL NOT NULL," +
          "ACCURACY REAL NOT NULL, " +
          "MOVEMENT_SPEED REAL NOT NULL, " +
          "FIRE_RATE REAL NOT NULL, " +
          "SHOT_SPEED REAL NOT NULL, " +
          "PRIMARY KEY (WAVE_ID, TEST_ID) " +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateEnemies()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS ENEMIES( " +
          "ENEMY_ID INTEGER NOT NULL, " +
          "WAVE_ID INTEGER NOT NULL, " +
          "TEST_ID INTEGER NOT NULL, " +
          "HEALTH INTEGER NOT NULL, " +
          "FIRE_RATE REAL NOT NULL, " +
          "SHOT_SPEED REAL NOT NULL, " +
          "SHOTS_FIRED INTEGER NOT NULL, " +
          "LIFETIME INTEGER NOT NULL, " +
          "HITS INTEGER NOT NULL, " +
          "PRIMARY KEY (ENEMY_ID, WAVE_ID, TEST_ID) " +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateHits()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS HITS("+
          "ENEMY_ID INT NOT NULL, " +
          "WAVE_ID INT NOT NULL, " +
          "TEST_ID INT NOT NULL, " +
          "SHOTS_ON_SCREEN INT NOT NULL" +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    public void InsertPlayer(Player p, int wave, int test)
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          " INSERT INTO PLAYERS (WAVE_ID, TEST_ID, TARGET_MODE, REACTION_SPEED, ACCURACY, MOVEMENT_SPEED, FIRE_RATE, SHOT_SPEED)" +
          " VALUES (" + wave + ", " + test + ", \"" + p.Targetting + "\", " + p.ReactionSpeed + ", " + p.Accuracy + ", " + p.Speed + ", " + p.FireRate + ", " + p.ShotSpeed + " );";
        reader = dbcmd.ExecuteReader();
    }

    public void InsertEnemy(Shooter s, int wave, int test)
    {
        try
        {
            IDbCommand dbcmd = dbcon.CreateCommand();
            IDataReader reader;

            dbcmd.CommandText =
              " INSERT INTO ENEMIES (ENEMY_ID, WAVE_ID, TEST_ID, HEALTH , FIRE_RATE, SHOT_SPEED, SHOTS_FIRED, LIFETIME, HITS)" +
              " VALUES (" + s.Id + ", " + wave + ", " + test + ", " + s.MaxHealth + ", " + s.FireRate + ", " + s.ShotSpeed + ", " + s.ShotsFired + ", " + s.LifeTime + ", " + s.Hits + " );";
            reader = dbcmd.ExecuteReader();
            Debug.Log("SUCCESS: Enemy " + s.Id + ", Wave " + wave + ", Test " + test);
        }
        catch (SqliteException e)
        {
            Debug.Log("FAILED: Enemy " + s.Id + ", Wave " + wave + ", Test " + test);
        }
    }

    public void InsertHit(int enemy, int wave, int test, int shots)
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          " INSERT INTO HITS (ENEMY_ID, WAVE_ID, TEST_ID, SHOTS_ON_SCREEN)" +
          " VALUES (" + enemy + ", " + wave + ", " + test + ", " + shots + " );";
        reader = dbcmd.ExecuteReader();
    }

    public void InsertWave(int wave, int test, int enemies, int hits, int duration, int shots)
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          " INSERT INTO WAVES (WAVE_ID, TEST_ID, ENEMY_COUNT, HITS, DURATION, SHOTS_FIRED)" +
          " VALUES (" + wave + ", " + test + ", " + enemies + ", " + hits +"," + duration + ", " + shots + " );";
        reader = dbcmd.ExecuteReader();
    }

    public void InsertTest(int test, string desc)
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          " INSERT INTO TESTS (TEST_ID, DESC, DATE)" +
          " VALUES (" + test + ", \"" + desc + "\", \"" + DateTime.Now + "\" );";
        reader = dbcmd.ExecuteReader();
    }

    public int TestCount()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "SELECT COUNT(*) FROM TESTS;";
        reader = dbcmd.ExecuteReader();
        return int.Parse(reader[0].ToString());
    }
}