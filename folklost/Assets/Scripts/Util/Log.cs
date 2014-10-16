#if UNITY_STANDALONE
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class Log : MonoBehaviour {
	
	private static string fileName;
	private static List<string> strings = new List<string>();
	
	public void Start() {
		StartNewLog();
	}
	
	public void OnDestroy() {
		WriteLog();
	}
	
	public static bool HasNotStarted() {
		return fileName == null;
	}

	public static void StartNewLog() {
		strings.Clear();
		fileName = "Logs/" + DateTime.UtcNow.ToString("yyyy-MM-ddTHHmmssZ") + ".log";
		
		if(!Directory.Exists("Logs")) {
			Directory.CreateDirectory("Logs");
		}
		
		if(File.Exists(fileName)) {
			return;
		} else {
			using(StreamWriter stream = File.CreateText(fileName)) {
				stream.WriteLine(fileName);
				stream.WriteLine("====================");
			}
		}
	}
	
	public static void Write(string str) {
		if(HasNotStarted()) {
			Debug.LogError("Cannot write log; log has not been started");
			return;
		}
		
		float seconds = Time.realtimeSinceStartup;
		string output = seconds.ToString("0.000") + "s: " + str;
		strings.Add(output);
	}

	public static void WriteLog() {
		if(HasNotStarted()) {
			Debug.LogError("Cannot write log; log has not been started");
			return;
		}
		
		using(StreamWriter stream = new StreamWriter(fileName, true)) {
			foreach(string str in strings) {
				stream.WriteLine(str);
			}
		}
		
		Debug.Log("Log saved\n" + fileName );
		fileName = null;
	}
}
#else
using UnityEngine;
public class Log : MonoBehaviour {
	
	public static bool HasNotStarted() 
	{
	return true;
	}
	public static void StartNewLog() {}
	
	public static void Write(string str) {}
	public static void WriteLog() {}
}
#endif