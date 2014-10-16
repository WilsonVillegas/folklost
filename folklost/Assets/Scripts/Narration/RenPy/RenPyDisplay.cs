using UnityEngine;
using System.Collections;
using RenPy.Dialog;
using RenPy.Parser;
using RenPy.Script;

namespace RenPy
{
	public abstract class RenPyDisplay : MonoBehaviour
	{
		public RenPyScriptAsset renpyScript;
		
		private RenPyDialogState m_state;
		public RenPyDialogState State {
			get { return m_state; }
		}
		
		private AudioSource m_music;
		public AudioSource Music {
			get { return m_music; }
		}
		
		private AudioSource m_sound;
		public AudioSource Sound {
			get { return m_sound; }
		}
		
		private GameObject m_coroutines;
		private RenPyCoroutine m_corMusic;
		private RenPyCoroutine m_corSound;
		
		private bool open = false;
		public bool Open {
			get { return open; }
		}
		
		public void Awake() {
			if(renpyScript == null) {
				Debug.LogWarning("RenPy script is null!");
			}
			m_state = RenPyParser.Parse(ref renpyScript);

			GameObject parent = CreateChildGameObject(this.gameObject.transform.parent.gameObject, name + " Coroutines");
			
			GameObject go = CreateChildGameObject(parent, "Music");
			m_music = go.AddComponent<AudioSource>();
			m_music.loop = true;
			
			go = CreateChildGameObject(parent, "Sounds");
			m_sound = go.AddComponent<AudioSource>();
			m_sound.loop = false;
			
			m_coroutines = CreateChildGameObject(parent, "Coroutines");
			go = CreateChildGameObject(m_coroutines, "Music Coroutine");
			m_corMusic = go.AddComponent<RenPyCoroutine>();
			go = CreateChildGameObject(m_coroutines, "Sound Coroutine");
			m_corSound = go.AddComponent<RenPyCoroutine>();
		}
		
		public void Start() {
			m_state.Reset();
			open = false;
		}
		
		public void Update() {
			if(open && m_state.CurrentLine != null) {
				RenPyUpdate(m_state.CurrentLine.Type);
			} else {
				StopDialog();
			}
		}
		
		protected abstract void RenPyUpdate(RenPyLineType type);
		
		public void OnGUI() {
			if(open && m_state.CurrentLine != null) {
				RenPyOnGUI(m_state.CurrentLine.Type);
			} else {
				StopDialog();
			}
		}
		
		protected abstract void RenPyOnGUI(RenPyLineType type);
		
		public void StartDialog() {
			m_state.Reset();
			m_state.CurrentLine.Execute(this);
			open = true;
		}
		
		public void StopDialog() {
			open = false;
		}
		
		private GameObject CreateChildGameObject(GameObject parent, string name) {
			GameObject go = new GameObject();
			go.name = name;
			go.transform.parent = parent.transform;
			return go;
		}
		
		public Coroutine BeginCoroutine(IEnumerator routine) {
			return StartCoroutine(routine);
		}
		
		public Coroutine BeginCoroutine(string category, IEnumerator routine) {
			switch(category) {
				case "music":
					return m_corMusic.BeginCoroutine(routine);
				case "sound":
					return m_corSound.BeginCoroutine(routine);
			}
			return null;
		}
	}
}
