using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
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
          "TEST_IDINTEGER PRIMARY KEY NOT NULL, " +
          "DESC TEXT NOT NULL, " +
          "DATE TEXT NOT NULL "
          + ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateWaves()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS WAVES (" +
          "WAVE_ID INTEGER PRIMARY KEY NOT NULL, " +
          "TEST_ID INTEGER NOT NULL, " +
          "PLAYER_ID INTEGER NOT NULL, " +
          "ENEMY1_ID INTEGER, " +
          "ENEMY2_ID INTEGER, " +
          "ENEMY3_ID INTEGER, " +
          "ENEMY4_ID INTEGER, " +
          "ENEMY5_ID INTEGER, " +
          "ENEMY6_ID INTEGER, " +
          "DURATION INTEGER NOT NULL, " +
          "SHOTS_FIRED INTEGER NOT NULL "
          + ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreatePlayers()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS PLAYERS (" + 
          "TARGET_MODE TEXT NOT NULL, " +
          "REACTION_SPEED REAL NOT NULL," +
          "ACCURACY REAL NOT NULL, " +
          "MOVEMENT_SPEED REAL NOT NULL, " +
          "FIRE_RATE REAL NOT NULL, " +
          "SHOT_SPEED REAL NOT NULL " +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateEnemies()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS ENEMIES( " +
          "ENEMY_ID INTEGER PRIMARY KEY NOT NULL, " +
          "HEALTH INTEGER NOT NULL, " +
          "FIRE_RATE REAL NOT NULL, " +
          "SHOT_SPEED REAL NOT NULL, " +
          "SHOTS_FIRED INTEGER NOT NULL, " +
          "LIFETIME INTEGER NOT NULL " +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    private void CreateHits()
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          "CREATE TABLE IF NOT EXISTS HITS("+
          "ENEMY_ID INT NOT NULL, "+
          "WAVE_ID INT NOT NULL, "+
          "SHOTS_ON_SCREEN INT NOT NULL" +
          ")";
        reader = dbcmd.ExecuteReader();
    }

    public void InsertPlayer(string target, double react, double acc, double move, double fireRate, double shotSpeed)
    {
        IDbCommand dbcmd = dbcon.CreateCommand();
        IDataReader reader;

        dbcmd.CommandText =
          " INSERT INTO PLAYERS (TARGET_MODE, REACTION_SPEED, ACCURACY, MOVEMENT_SPEED, FIRE_RATE, SHOT_SPEED)" +
          " VALUES (\"" + target + "\", "+react + ", " + acc +", " + move+", "+ fireRate+", "+ shotSpeed +" );";
        reader = dbcmd.ExecuteReader();
        Debug.Log("ME");
    }
}
