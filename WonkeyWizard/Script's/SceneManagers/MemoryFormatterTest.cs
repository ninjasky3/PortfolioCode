using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class MemoryFormatterTest : MonoBehaviour {
	//You must include these namespaces
	//to use BinaryFormatter
		//High score entry
		public class ScoreEntry
		{
			
			//Players name
			
			public string name;
			
			//Score
			
			public int score;
			
		}
		
		
		//High score table
		
		public List<ScoreEntry> highScores = new List<ScoreEntry>();
		
		
		void SaveScores()
			
		{
			
			//Get a binary formatter
			
			var b = new BinaryFormatter();
			
			//Create an in memory stream
			
			var m = new MemoryStream();
			
			//Save the scores
			
			b.Serialize(m, highScores);
			
			//Add it to player prefs
			
			PlayerPrefs.SetString("HighScores", Convert.ToBase64String(m.GetBuffer()));
			
		}
		
		
		void
			Start()
				
		{
			
			//Get the data
			
			
			string data = PlayerPrefs.GetString("HighScores");
			//If not blank then load it
			
			if(!string.IsNullOrEmpty(data))
			{
				//Binary formatter for loading back
				BinaryFormatter b = new BinaryFormatter();
				//Create a memory stream with the data
				MemoryStream m = new MemoryStream(Convert.FromBase64String(data));
				//Load back the scores
				highScores = (List<ScoreEntry>)b.Deserialize(m);
			}
		}
	}
