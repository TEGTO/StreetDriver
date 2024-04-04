using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MusicNS
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] audioClips;
        [SerializeField]
        private float changeClipRate = 1f;
        [SerializeField]
        private float updateRate = 1f;

        private AudioClip currentClip;
        private CancellationTokenSource tokenSource;
        private Coroutine changeSongCoroutine;

        private void OnEnable()
        {
            tokenSource = new CancellationTokenSource();
            CheckIfSongEnded();
        }
        private void OnDisable()
        {
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }
        private async void CheckIfSongEnded()
        {
            while (!tokenSource.IsCancellationRequested)
            {
                if (!audioSource.isPlaying)
                    SetSong();
                await Task.Delay((int)updateRate * 1000);
            }
        }
        private void SetSong()
        {
            if (audioClips.Length > 0 && changeSongCoroutine == null)
            {
                AudioClip newAudioClip = audioClips.Length > 1 ? audioClips.Where(x => x != currentClip).ToArray()[Random.Range(0, audioClips.Length)]
                : audioClips.First();
                changeSongCoroutine = StartCoroutine(ChangeSong(newAudioClip));
            }
        }
        private IEnumerator ChangeSong(AudioClip audioClip)
        {
            audioSource.volume = 0;
            audioSource.clip = audioClip;
            audioSource.Play();
            while (audioSource.volume < 1)
            {
                audioSource.volume += Time.deltaTime / changeClipRate;
                yield return null;
            }
            changeSongCoroutine = null;
        }
    }
}
