using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using mDitaEditor.Utils;
using NAudio.Lame;
using NAudio.Wave;

namespace mDitaEditor.Dita.Forms
{
    public partial class RecordAudioForm : Form
    {
        public string Musica
        {
            get; private set;
        }
        int seconds = 0;
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);
        
        Timer t = new Timer();
        Timer play = new Timer();
        AudioFileReader audioFileReader;
        IWavePlayer waveOutDevice;
        public bool recording = false;
        public RecordAudioForm()
        {
            InitializeComponent();
            t.Interval = 1000;
            t.Tick += T_Tick;
            Musica = "";
            play.Interval = 1000;
            play.Tick += T2_Tick;
           

        }
        /// <summary>
        /// Otkucaj timer-a koji ispisuje koliko dugo korisnik snima Audio materijal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            seconds += 1;
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            string answer = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds);
            label1.Text = "Recording " + answer;
        }
        /// <summary>
        /// Otkucaj timer-a koji prikazuje koliko je još ostalo
        /// u puštanju snimljenog klipa do kraja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T2_Tick(object sender, EventArgs e)
        {
            if (progressPlay.Value != progressPlay.Maximum)
            {
                progressPlay.Value += 1;
            }
            else
            {
                play.Stop();
            }
        }
        private void RecordAudio_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Metoda koja konvertuje Wav u Mp3
        /// </summary>
        /// <param name="wavFile"></param>
        private static void NAudioWavToMp3(string wavFile)
        {
            using (WaveFileReader reader = new WaveFileReader(wavFile))
            {
                using (var writer = new LameMP3FileWriter(wavFile.Replace(".wav", ".mp3"), reader.WaveFormat, 128))
                {
                    reader.CopyTo(writer);
                    writer.Close();
                }
            }
        }
     
        /// <summary>
        /// Dugme za puštanje pesme. Ako nije snimljena pesma traži da je otvori
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (Musica == "")
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "MP3|*.mp3";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Musica = open.FileName;
                }
            }
            if (Musica != "")
            {
                if (waveOutDevice != null)
                {
                    waveOutDevice.Stop();
                    waveOutDevice.Dispose();
                }
                if (audioFileReader != null)
                {
                    try
                    {
                        audioFileReader.Close();
                        audioFileReader.Dispose();
                    }
                    catch { }
                }
                waveOutDevice = new WaveOut();
                audioFileReader = new AudioFileReader(Musica);

                progressPlay.Value = 0;
                progressPlay.Maximum = seconds;
                play.Start();
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }
        }
        /// <summary>
        /// Dugme za stopiranje snimanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (recording == true)
            {
                
                t.Stop();
                label1.Visible = false;
                mciSendString("pause capture", null, 0, 0);
                Musica = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\record.wav";
                mciSendString("save capture \"" + Musica + "\"", null, 0, 0);
                mciSendString("close capture", null, 0, 0);
                if (File.Exists(Musica.Replace("wav", "mp3")))
                {
                    FileManager.DeleteFile(Musica.Replace("wav", "mp3"));
                }
                NAudioWavToMp3(Musica);
                File.Delete(Musica);
                Musica = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\record.mp3";
                recording = false;
                btnPlay.Enabled = true;
                btnTakeAudio.Enabled = true;
            }
            else
            {
                if (waveOutDevice != null)
                {
                    waveOutDevice.Stop();
                }
            }
        }

        private void brnRecord_Click(object sender, EventArgs e)
        {
            recording = true;
            btnPlay.Enabled = false;
            btnTakeAudio.Enabled = false;
            progressPlay.Value = 0;
            if (waveOutDevice != null)
            {
                try
                {
                    audioFileReader.Close();
                    waveOutDevice.Stop();
                    waveOutDevice.Dispose();
                }
                catch { }
            }
            if (audioFileReader != null)
            {
                try
                {
                    audioFileReader.Close();
                    audioFileReader.Dispose();
                }
                catch { }
            }
            mciSendString("open new Type waveaudio Alias capture", null, 0, 0);
            mciSendString("set capture alignment 2 bitspersample 16 samplespersec 44100 channels 1 bytespersec 8820", null, 0, 0);
            mciSendString("set capture channels 1", null, 0, 0);
            t.Start();
            seconds = 0;
            label1.Visible = true;
            label1.Text = "Recording";
            mciSendString("record capture", null, 0, 0);
        }

        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            btnPlay.Image = Properties.Resources.play2_hover;
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {

            btnPlay.Image = Properties.Resources.play2;
        }

        private void btnStop_MouseEnter(object sender, EventArgs e)
        {
            btnStop.Image = Properties.Resources.stop_hover;
        }

        private void btnStop_MouseLeave(object sender, EventArgs e)
        {

            btnStop.Image = Properties.Resources.stop;
        }

        private void brnRecord_MouseEnter(object sender, EventArgs e)
        {
            brnRecord.Image = Properties.Resources.record_hover;
        }

        private void brnRecord_MouseLeave(object sender, EventArgs e)
        {
            brnRecord.Image = Properties.Resources.record;
        }

        private void btnTakeAudio_Click(object sender, EventArgs e)
        {
            if (Musica != "")
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void RecordAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
            }
            if (audioFileReader != null)
            {
                try
                {
                    audioFileReader.Close();
                }
                catch { }
                try
                {
                    audioFileReader.Dispose();
                }
                catch { }
            }
        }
    }
}
