using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Management.Instrumentation;
using System.Collections.Specialized;
using System.Media;
using System.Net.NetworkInformation;
using System.Threading;
using System.Reflection;
using System.Speech.Synthesis;
using Microsoft.Win32;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {




        bool play = true;
        int playy = 0;
        

        #region formstuff
        public Form1()
        {
            //hiding form
            InitializeComponent();

















            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            

            while(true)
            {
                bool test = PingHost("google.com");
                if (!test)
                {
                    outtest();
                    play = !play;
                    ++playy;
                }
                else
                {

                    if (playy < 4)
                    {
                        --playy;
                    }
                }
                Thread.Sleep(1300);
            }
            
        }

        public void startup()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue("InternetWarn") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                rkApp.SetValue("InternetWarn", Application.ExecutablePath.ToString());
            }
            else
            {
                // The value exists, the application is set to run at startup
                
            }
        }



        #endregion

        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        
        public void outtest(){
            Console.WriteLine(play);
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream s = a.GetManifestResourceStream("WindowsFormsApplication1.sound.wav");
            SoundPlayer player = new SoundPlayer(s);
            bool lay;
            lay = false;
            if(playy < 4){
                lay = true;
            }
            if (play && lay)
            {
                player.Play();
            }
            else
            {
                if (lay)
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();

                    synth.Speak("INTERNET DOWN");
                }
                
            }
            
        }
        
        

    }
}
